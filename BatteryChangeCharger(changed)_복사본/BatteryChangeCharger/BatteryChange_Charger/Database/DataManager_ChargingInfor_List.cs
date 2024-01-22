using BatteryChangeCharger.Applications;
using BatteryChangeCharger.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.Database
{
    public class CINDEX_CHARGINGINFOR_LIST
    {
        public const int _id = 0;
        public const int dividecode = 1;
        public const int socketid = 2;
        public const int wasted_second = 3;
        public const int bmu_pack_in = 4;

        public const int bmu_pack_out = 5;
        public const int bmu_current = 6;
        public const int bmu_soc = 7;
        public const int bmu_soh = 8;
        public const int bmu_thmin = 9;

        public const int bmu_thmax = 10;
        public const int bmu_cc = 11;
        public const int bmu_cv = 12;
        public const int bmu_remain_capacity = 13;
        public const int bmu_cutoff_current = 14;

        public const int bmu_is_enable_charge = 15;
        public const int nfc_output_voltage = 16;
        public const int nfc_output_current = 17;
    }

    public class DataManager_ChargingInfor_List
    {
        protected static DataManager_ChargingInfor_List mDataManager = null;
        public static DataManager_ChargingInfor_List getInstance()
        {
            if(mDataManager == null)
            {
                mDataManager = new DataManager_ChargingInfor_List(1);
            }
            return mDataManager;
        }

        string mFilePath = @"..\..\DB_ChargingAndTemp_List.db";
        protected SQLiteConnection sql_con;
        protected SQLiteDataReader sql_reader;
        protected SQLiteCommand sql_cmd;
        protected SQLiteDataAdapter sql_adapter;
        public static DataSet DS = new DataSet();

        protected string mName_Tag = "";
        protected string mName_DB = "DB_ChargingAndTemp";
        protected string mName_Table = "Table_ChargingAndTemp";

        protected const string TYPE_TEXT = "TEXT";
        protected const string TYPE_INTEGER = "INTEGER";
        protected const string TYPE_REAL = "REAL";

        public string[] mArray_Column_Name = new string[] { "_id", "dividecode", "socketid", "wasted_second", 
            "bmu_pack_in", 
            "bmu_pack_out", "bmu_current", "bmu_soc", "bmu_soh", "bmu_thmin", 
            "bmu_thmax", "bmu_cc", "bmu_cv", "bmu_remain_capacity", "bmu_cutoff_current", 
            "bmu_is_enable_charge", "nfc_output_voltage", "nfc_output_current"
        };

        public string[] mArray_Column_DataType = new string[] {
             "_id", TYPE_TEXT, TYPE_INTEGER, TYPE_INTEGER,
            TYPE_REAL,
            TYPE_REAL, TYPE_REAL, TYPE_INTEGER, TYPE_INTEGER, TYPE_REAL,
            TYPE_REAL, TYPE_REAL, TYPE_REAL, TYPE_REAL, TYPE_REAL,
            TYPE_INTEGER, TYPE_REAL, TYPE_REAL
        };

        public string[] getColumnName() => mArray_Column_Name;
        public string getColumnName(int index) => mArray_Column_Name[index];
        /*   -------------------------------------------------------------------------------------------------------   */
        public DataManager_ChargingInfor_List(int channelIndex)
        {
            mApplication = MyApplication.getInstance();
            mChannelIndex = channelIndex;

            //mName_DB = dbName;

            //mName_Table = tableName + "_" + channelIndex;

            //mArray_Column_Name = array_columnName;
            //mArray_Column_DataType = array_columnType;

            if (mFilePath == null || mFilePath.Length < 5) return;
            generateDatabase();
        }

        public void closeConnection()
        {
            if (sql_adapter != null)
            {
                sql_adapter.Dispose();
                sql_adapter = null;
            }

            if (sql_reader != null)
            {
                sql_reader.Close();
                sql_reader = null;
            }

            if (sql_cmd != null)
            {
                sql_cmd.Cancel();
                sql_cmd = null;
            }

            if (sql_con != null)
            {
                sql_con.Close();
                sql_con = null;
            }
        }

        protected void generateDatabase()
        {
            bool isFirst = false;
            if (!File.Exists(mFilePath))
            {
                SQLiteConnection.CreateFile(mFilePath);
                isFirst = true;
            }

            if (isFirst) createTable();
        }

        public void regenerateDatabase()
        {
            dropTable();
            createTable();
        }

        protected void dropTable() => CommandTextAfterNonQuery("drop table '" + mName_Table + "'");


        protected void Connection()
        {
            if (sql_con == null)
            {
                sql_con = new SQLiteConnection("Data Source=" + mFilePath + ";Version=3;New=false;Compress=True;DSQLITE_THREADSAFE=2;BusyTimeout=0;LockingMode=Normal;");
                sql_con.Open();
            }

            // http://docwiki.embarcadero.com/RADStudio/Tokyo/en/Connect_to_SQLite_database_(FireDAC)
        }
        /*----------------------------------------------------------------------------------------------------------------------------------------------------------------
         * 
         ----------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public int removeCulumnById(string id)
        {
            if (id == null || id.Length < 1) return 0;
            string query = "DELETE FROM " + mName_Table + " WHERE " + mArray_Column_Name[0] + " = " + id;

            CommandTextAfterNonQuery(query);
            return Manager_Conversion.getInt(id);
        }


        protected void CommandTextAfterNonQuery(string str)
        {
            Connection();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = str;
            sql_cmd.ExecuteNonQuery();
        }
        protected void createTable()
        {
            string createSQL = "CREATE TABLE '" + mName_Table + "' (_id INTEGER PRIMARY KEY AUTOINCREMENT,";

            for (int i = 1; i < mArray_Column_Name.Length; i++)
            {
                createSQL += "'" + mArray_Column_Name[i] + "' " + " " + mArray_Column_DataType[i];
                if (i != mArray_Column_Name.Length - 1) createSQL += ", ";
            }

            CommandTextAfterNonQuery(createSQL + ");");
        }
        protected string ObjToString(object p)
        {
            string strRef = "";

            if (p == null) strRef = "";
            else strRef = p.ToString();
            return strRef;
        }

        protected MyApplication mApplication = null;
        protected int mChannelIndex = 0;

        public long getSum_Database_By_Date(int columnIndex_sum, int columnIndex_Date, string date)
        {
            Connection();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd = new SQLiteCommand("SELECT sum(" + mArray_Column_Name[columnIndex_sum] + ") FROM " + mName_Table + " WHERE " + mArray_Column_Name[columnIndex_Date] + " = " + date +
                " ORDER BY _id DESC LIMIT 1", sql_con);
            sql_reader = sql_cmd.ExecuteReader();

            while (sql_reader.Read())
                result = sql_reader[0];

            if (sql_reader != null)
                sql_reader.Close();
            int sum = 0;
            int.TryParse(ObjToString(result), out sum);
            return sum;
        }

        protected object result;
        public int getCount_Database_By_Date(int index_DateColumn, string date)
        {
            Connection();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd = new SQLiteCommand("SELECT " + mArray_Column_Name[0] + " FROM " + mName_Table + " WHERE " + index_DateColumn + " = " + date +
                " ORDER BY _id DESC LIMIT 1", sql_con);

            return Convert.ToInt32(sql_cmd.ExecuteScalar());
        }

        public void updateColumn(string id, int[] culumnIndex, string[] data)
        {

            int length = 0;
            if (data.Length > culumnIndex.Length) length = culumnIndex.Length;
            else length = data.Length;
            string query = "UPDATE 'main'.'" + mName_Table + "' SET ";

            for (int i = 0; i < length; i++)
            {
                if (mArray_Column_DataType[culumnIndex[i]] == TYPE_INTEGER) query += mArray_Column_Name[culumnIndex[i]] + " = " + data[i];
                else query += mArray_Column_Name[culumnIndex[i]] + " = '" + data[i] + "'";
                if (i != length - 1) { query += ", "; }
            }
            query += " where " + mArray_Column_DataType[0] + " = " + id + ";";
            CommandTextAfterNonQuery(query);
        }
        /*----------------------------------------------------------------------------------------------------------------------------------------------------------------
         * 
          ----------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public int removeColumnById(string id)
        {
            if (id == null || id.Length < 1) return 0;

            CommandTextAfterNonQuery("DELETE FROM " + mName_Table + " WHERE " + mArray_Column_Name[0] + " =? " + id);
            return 1;
        }

        /*----------------------------------------------------------------------------------------------------------------------------------------------------------------
         * 
          ----------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public string GetQueryMakeTable()
        {
            string data = "CREATE TABLE " + mName_Table + "("
                    + "_id INTEGER PRIMARY KEY AUTOINCREMENT,";

            data += GetQueryMakeTable(mArray_Column_Name, mArray_Column_DataType, mArray_Column_DataType.Length - 1);
            return data;
        }

        public static string GetQueryMakeTable(string[] culumn, string[] culumn_datatype, int length)
        {
            string returnString = "";
            for (int i = 0; i < length; i++)
                if (i == (length - 1)) returnString += culumn[i + 1] + " " + culumn_datatype[i + 1] + ");";
                else returnString += culumn[i + 1] + " " + culumn_datatype[i + 1] + ",";

            return returnString;
        }

        public int getCount_AllRow(string where)
        {
            Connection();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "select count(" + mArray_Column_Name[0] + ") from '" + mName_Table + "'";
            if (where.Length > 0)
                sql_cmd.CommandText = sql_cmd.CommandText + " " + where;
            return Convert.ToInt32(sql_cmd.ExecuteScalar());
        }

        public bool queryExistColumn(int selectIndex, string where)
        {
            Connection();
            sql_cmd = sql_con.CreateCommand();

            sql_cmd.CommandText = "select *  from '" + mName_Table + "' where " + mArray_Column_Name[selectIndex] + " = '" + where + "'";
            sql_adapter = new SQLiteDataAdapter(sql_cmd);
            DataTable dt = new DataTable();
            sql_adapter.Fill(dt);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }


        public List<string> queryColumn_Uniq(int selectIndex)
        {
            Connection();
            sql_cmd = sql_con.CreateCommand();

            sql_cmd.CommandText = "select distinct "+mArray_Column_Name[CINDEX_CHARGINGINFOR_LIST.dividecode]+" from '" + mName_Table + "'";
            sql_adapter = new SQLiteDataAdapter(sql_cmd);
            DataTable dt = new DataTable();
            sql_adapter.Fill(dt);
            List<string> list = new List<string>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                list.Add((string)dt.Rows[i][0]);

            return list;
        }

        public DataTable queryColumn(int[] selectIndex, int offset, int length, int index_orderby,string where, string asc)
        {
            Connection();
            sql_cmd = sql_con.CreateCommand();

            string selectQuery = "";
            if (selectIndex == null) selectQuery = "*";
            else
                for (int i = 0; i < selectIndex.Length; i++)
                {
                    selectQuery += mArray_Column_Name[selectIndex[i]];
                    if (i == selectIndex.Length - 1) selectQuery += " ";
                    else selectQuery += ", ";
                }

            if(length < 1)
            {
                sql_cmd.CommandText = "select " + selectQuery + " from '" + mName_Table + "' " + where + " ORDER BY "+ mArray_Column_Name[index_orderby] +" "+ asc;
            }
            else
            {
                if (offset > 0)
                    sql_cmd.CommandText = "select " + selectQuery + " from '" + mName_Table + "' " + where + " ORDER BY " + mArray_Column_Name[index_orderby] + " " + asc + " LIMIT " + length + " OFFSET " + offset;
                else
                    sql_cmd.CommandText = "select " + selectQuery + " from '" + mName_Table + "' " + where + " ORDER BY " + mArray_Column_Name[index_orderby] + " " + asc + " LIMIT " + length;
            }
            

            sql_adapter = new SQLiteDataAdapter(sql_cmd);
            DataTable dt = new DataTable();
            sql_adapter.Fill(dt);

            return dt;
        }

        public string[] queryColumn_By_id(string id)
        {
            Connection();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd = new SQLiteCommand("SELECT * FROM " + mName_Table + " WHERE _id = " + id + " ORDER BY _id ASC LIMIT 1", sql_con);
            sql_adapter = new SQLiteDataAdapter(sql_cmd);
            DataTable dt = new DataTable();
            sql_adapter.Fill(dt);
            List<string> list = new List<string>();

            for (int i = 0; i < dt.Rows.Count; i++)
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                    list.Add(ObjToString(dt.Rows[i][j]));

            string[] returnData = list.ToArray();
            return returnData;
        }

        public string[] queryColumn_Last()
        {
            Connection();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd = new SQLiteCommand("SELECT * FROM " + mName_Table + " ORDER BY _id DESC LIMIT 1", sql_con);
            sql_adapter = new SQLiteDataAdapter(sql_cmd);
            DataTable dt = new DataTable();
            sql_adapter.Fill(dt);
            List<string> list = new List<string>();

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                    list.Add(ObjToString(dt.Rows[i][j]));

            string[] returnData = list.ToArray();
            return returnData;
        }

        public string getSelect_String(int[] select)
        {
            string str = "";
            if (select == null) return "*";
            int lastIndex = select.Length - 1;
            if (lastIndex < 0) return "*";

            for (int i = 0; i < select.Length; i++)
                if (i == select.Length - 1) str += mArray_Column_Name[select[i]];
                else str += mArray_Column_Name[select[i]] + ", ";

            return str;
        }

        public string getWhere_String(int[] where, string[] data)
        {
            if (where == null || data == null) return "";

            string str = " where ";
            for (int i = 0; i < where.Length; i++)
            {
                if (mArray_Column_DataType[where[i]] == TYPE_INTEGER)
                    str += mArray_Column_Name[where[i]] + " = " + data[i] + " ";
                else
                    str += mArray_Column_Name[where[i]] + " = '" + data[i] + "' ";

                if (i < where.Length - 1) str += " and ";
            }
            return str;
        }

        /*----------------------------------------------------------------------------------------------------------------------------------------------------------------
         * 
          ----------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*
        *  공통으로 들어가는 시간을 제외한 데이터 입력
        */
        public void InsertColumn(string[] data)
        {
            Connection();
            string query = "INSERT INTO 'main'.'" + mName_Table + "' ";

            query += Make_INSERT_QueryL(data);

            CommandTextAfterNonQuery(query);
        }

        public string Make_INSERT_QueryL(string[] data)
        {
            string query = "(";
            int length = 0;
            length = data.Length;
            

            for (int i = 0; i < length; i++)
            {
                if (i == length - 1) query += "'" + mArray_Column_Name[1 + i] + "'";
                else query += "'" + mArray_Column_Name[1 + i] + "',";
            }
                

            query += ") VALUES (";
            for (int i = 0; i < length; i++)
            {
                if (mArray_Column_DataType[i + 1] == TYPE_INTEGER) query += data[i] + "";
                else query += "'" + data[i] + "'";
                if (i == length - 1) query += ");";
                else query += ",";
            }

            return query;
        }

        public string Make_INSERT_QueryL(int[] culumnIndex, string[] data)
        {
            string query = "(";
            int length = 0;
            if (culumnIndex.Length > data.Length) length = data.Length;
            else length = culumnIndex.Length;

            for (int i = 0; i < length; i++)
                if (i == length - 1) query += "'" + mArray_Column_Name[culumnIndex[i]] + "'";
                else query += "'" + mArray_Column_Name[culumnIndex[i]] + "',";

            query += ") VALUES (";
            for (int i = 0; i < length; i++)
            {
                if (mArray_Column_DataType[culumnIndex[i]] == TYPE_INTEGER) query += data[i] + "";
                else query += "'" + data[i] + "'";
                if (i == length - 1) query += ");";
                else query += ",";
            }

            return query;
        }
    }
}

