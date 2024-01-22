using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTVPlayer.Database
{
    abstract public class Manager_SQLite
    {
        protected string mFileName = "";
        protected string mTableName = "";
        protected bool bIsCreate = false;
        public Manager_SQLite(string dbFile, string tableName)
        {
            mFileName = dbFile + ".db";
            mTableName = tableName;
            if (!File.Exists(mFileName))
            {
                SQLiteConnection.CreateFile(mFileName);
                bIsCreate = true;
            }

            open();

            setManager_Table();
        }

       

        abstract public void setManager_Table();

        public List<Manager_Table> List_Manager_Table
        {
            get { return mList_Manager_Table; }
        }

        protected List<Manager_Table> mList_Manager_Table = new List<Manager_Table>();

        protected void initTable()
        {
            foreach (Manager_Table table in mList_Manager_Table)
            {
                table.dropTable();
            }
        }

        public SQLiteConnection getConnection()
        {
            return mConn;
        }

        protected SQLiteConnection mConn = null;

        public bool isOpen()
        {
            if (mConn == null)
                return false;
            return true;
        }

        protected void open()
        {
            try
            {
                mConn = new SQLiteConnection("Data Source=" + mFileName + ";Version=3");
                mConn.Open();
            }catch(Exception e)
            {
                mConn = null;
                Debug.Print(e.Message);
            }
        }

        
    }
}
