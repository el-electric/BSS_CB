using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCTVPlayer.Database
{
    abstract public class Manager_Table
    {
        //1) 연결지향형
        //연결지향형은 DataReader를 사용

        //2) 비연결지향형
        //비연결지향형은 DataAdapter를 사용

        protected const string TYPE_INTEGER = "INTEGER";
        protected const string TYPE_REAL = "REAL";
        protected const string TYPE_TEXT = "TEXT";
        protected const string TYPE_BLOB = "BLOB";

        protected string mTableName = "";
        protected Manager_SQLite mManager_SQLite = null;
        protected bool bIsMakeTable = false;
        public Manager_Table(Manager_SQLite manager_SQLite, string tableName, bool maketable)
        {
            mManager_SQLite = manager_SQLite;
            mTableName = tableName;
            bIsMakeTable = maketable;
            if (maketable)
            {
                makeTable();
            }
        }

        public DataSet selectRow(string[] select, string where, DataGridView gridView, int dataTableIndexArray)
        {
            DataSet ds = null;
            string selectString = "";
            if (select == null || select.Length < 1)    selectString = "*";
            else
            {
                for(int i = 0; i < select.Length; i++)
                {
                    if (selectString.Length > 0)
                        selectString += ", ";   
                    selectString += select[i];
                }
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ").Append(selectString);
            builder.Append(" FROM ").Append(mTableName);
            if (where != null && where.Length > 0)
                builder.Append(" Where ").Append(where);

            string query = builder.ToString();

            SQLiteCommand cmd = new SQLiteCommand(query);
            cmd.Connection = mManager_SQLite.getConnection();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            ds = new DataSet();
            try
            {
                da.Fill(ds);

                if(gridView != null)
                {
                    DataTable dt = ds.Tables[dataTableIndexArray];
                    gridView.DataSource = dt;
                }
            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            cmd.Dispose();

            return ds;
        }

        public int deleteRow(string where)
        {
            if (where.Length < 1)
                return 0;

            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM ").Append(mTableName).Append(" where ").Append(where);
            string query = builder.ToString();
            int resultCount = excuteQuery_NonResult(query);
            return resultCount;
        }
        public int insertRow(string[] name, string[] value)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO ").Append(mTableName).Append(" ");
            int count = 0;
            if (name.Length >= value.Length) count = value.Length;
            else count = name.Length;
            StringBuilder builder_name = new StringBuilder(); builder_name.Append("(");
            StringBuilder builder_value = new StringBuilder(); builder_value.Append("(");
            for (int i = 0; i < count; i++)
            {
                builder_name.Append(name[i]);
                builder_value.Append("'");
                builder_value.Append(value[i]);
                builder_value.Append("'");
                if (i < count - 1)
                {
                    builder_name.Append(",");
                    builder_value.Append(",");
                }
            }
            builder_name.Append(")");
            builder_value.Append(")");
            builder.Append(builder_name.ToString()).Append(" values ").Append(builder_value.ToString());
            string query = builder.ToString();
            int resultCount = excuteQuery_NonResult(query);
            return resultCount;
        }

        public int updateRow(string[] name, string[] value, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE ").Append(mTableName).Append(" SET ");
            int count = 0;
            if (name.Length >= value.Length) count = value.Length;
            else count = name.Length;
            
            for (int i = 0; i < count; i++)
            {
                builder.Append(name[i]);
                builder.Append(" = ");
                builder.Append("'").Append(value[i]).Append("'");
                if (i < count - 1)
                {
                    builder.Append(",");
                }
            }

            if (where != null && where.Length > 0)
                builder.Append(" where ").Append(where);

            string query = builder.ToString();
            int resultCount = excuteQuery_NonResult(query);
            return resultCount;
        }


        virtual public void dropTable()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DROP TABLE ").Append(mTableName);
            excuteQuery_NonResult(builder.ToString());
            makeTable();
        }

        //public int getCount_Row(string )
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("SELECT COUNT(").Append(getColumn()[][] _id) FROM ").Append(mTableName);
        //    string sql = builder.ToString();
        //    SQLiteCommand command = new SQLiteCommand(mManager_SQLite.getConnection());
        //    command.CommandText = sql;

        //    object obj = command.ExecuteScalar();

        //    int count = Int32.Parse("" + obj);
        //    return count;
        //}

        public int getCount_Row(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(_id) FROM ").Append(mTableName);

            if (where != null && where.Length > 0)
                builder.Append(" where ").Append(where);

            string sql = builder.ToString();
            SQLiteCommand command = new SQLiteCommand(mManager_SQLite.getConnection());
            command.CommandText = sql;

            object obj = command.ExecuteScalar();

            int count = Int32.Parse(""+obj);
            return count;
        }

        virtual protected void makeTable()
        {
            string[][] column = getColumn();
            StringBuilder builder = new StringBuilder();
            builder.Append("CREATE TABLE ").Append(mTableName).Append(" (_id INTEGER PRIMARY KEY, ");

            for (int i = 1; i < column.Length; i++)
            {
                builder.Append(column[i][0]).Append(" ").Append(column[i][1]);
                if (i == column.Length - 1)
                {
                    builder.Append(")");
                }
                else
                {
                    builder.Append(", ");
                }
            }

            string query = builder.ToString();
            excuteQuery_NonResult(query);
        }


        public int excuteQuery_NonResult(string query)
        {
            var cmd = new SQLiteCommand(mManager_SQLite.getConnection());
            cmd.CommandText = query;
            int count = cmd.ExecuteNonQuery();
            return count;
        }

        //string[][] 형식 : {컬럼명, 기본 데이터}
        abstract public string[][] getColumn();

    }
}
