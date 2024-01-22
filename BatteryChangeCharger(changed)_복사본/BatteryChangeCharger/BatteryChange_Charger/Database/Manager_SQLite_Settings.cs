using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTVPlayer.Database
{
    public class Manager_SQLite_Settings : Manager_SQLite
    {
        const string NAME_DB = "Database_SystemSetting";
        const string NAME_TABLE = "systemsetting";


        public Manager_SQLite_Settings() : base(NAME_DB, NAME_TABLE)
        {
        }

        public Manager_Table_Setting getTable_Setting(int indexArray)
        {
            return (Manager_Table_Setting)mList_Manager_Table[indexArray];
        }

        public override void setManager_Table()
        {
            Manager_Table_Setting manager = new Manager_Table_MainSetting(this, mTableName + "_" + 0, bIsCreate);
            if(!bIsCreate) manager.insertFirstSetting();

            mList_Manager_Table.Add(manager);

            //for (int i = 0; i < 5; i++)
            //{
            //    manager = new Manager_Table_CameraSetting(this, mTableName + "_CCTV_" + (i + 1), bIsCreate);
            //    if (!bIsCreate) manager.insertFirstSetting();
            //    mList_Manager_Table.Add(manager);
            //}
                
        }

        
        
    }
}
