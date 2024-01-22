using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.database
{
    public class OCPP_Manager_Table_Setting
    {
        private const string NAME_DB = @"..\Database_OCPPSetting.db";
        private const string NAME_TABLE = "OCPP_Setting_Table";
        private SQLiteConnection connection;

        public OCPP_Manager_Table_Setting()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(NAME_DB))
            {
                connection = new SQLiteConnection($"Data Source={NAME_DB};Version=3;");
                connection.Open();
                CreateNewDatabase();
            }
            else
            {
                connection = new SQLiteConnection($"Data Source={NAME_DB};Version=3;");
                connection.Open();
                // 기존 데이터베이스에 대한 추가 작업이 필요한 경우 여기에 작성합니다.
            }
        }

        private void CreateNewDatabase()
        {
            CreateTables();
            InsertInitialData();
        }

        private void CreateTables()
        {
            var createTableQuery = @"
            CREATE TABLE IF NOT EXISTS " + NAME_TABLE + @" (
                SettingKey TEXT PRIMARY KEY,
                SettingValue TEXT NOT NULL,
                AccessType TEXT NOT NULL
            );";

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        static String[][] SETTING_DATA = new String[][]
      {
            ////////////////////////////////////////////0
            new string[]{"AllowOfflineTxForUnknownId", "true", "RW" },
            new string[]{"AuthorizationCacheEnabled", "false", "RW" },
            new string[] { "AuthorizeRemoteTxRequests", "false", "RW" },
            new string[] { "BlinkRepeat", "0", "RW" },
            new string[] { "ClockAlignedDataInterval", "0", "RW" },

            new string[] { "ConnectionTimeOut", "90", "RW" },
            new string[] { "ConnectorPhaseRotation", "Unknown", "RW" },/*CSL*/
            new string[] { "ConnectorPhaseRotationMaxLength", "0", "R" },
            new string[] { "GetConfigurationMaxKeys", "0", "R" },
            new string[] { "HeartbeatInterval", "60", "RW" },
            ////////////////////////////////////////////10
            new string[] { "LightIntensity", "0", "RW" },
            new string[] { "LocalAuthorizeOffline", "true", "RW" },
            new string[] { "LocalPreAuthorize", "false", "RW" },
            new string[] { "MaxEnergyOnInvalidId", "0", "RW" },
            new string[] { "MeterValuesAlignedData", "0", "RW" },/*CSL*/

            new string[] { "MeterValuesAlignedDataMaxLength", "50", "R" },
            new string[] { "MeterValuesSampledData", "Energy.Active.Import.Register", "RW" },/*CSL*/
            new string[] { "MeterValuesSampledDataMaxLength", "50", "R" },
            new string[] { "MeterValueSampleInterval", "300", "RW" },
            new string[] { "MinimumStatusDuration", "10", "RW" },
            ////////////////////////////////////////////20
            new string[] { "NumberOfConnectors", "1", "R" },
            new string[] { "ResetRetries", "0", "RW" },
//            {"StopTransactionOnEVSideDisconnect", "false" , "RW"},//기본상태, 아래는 급속을 위한 설정
            new string[] { "StopTransactionOnEVSideDisconnect", "false", "R" }, //커넥터가 분리되면 트랜잭션을 정지한다. 트랜잭션 != 충전
            new string[] { "StopTransactionOnInvalidId", "false", "RW" },
            new string[] { "StopTxnAlignedData", "0", "RW" },/*CSL*/

            new string[] { "StopTxnAlignedDataMaxLength", "5", "R" },
            new string[] { "StopTxnSampledData", "0", "RW" },/*CSL*/
            new string[] { "StopTxnSampledDataMaxLength", "50", "R" },
            new string[] { "SupportedFeatureProfiles", "Core", "R" },/*CSL*/
            new string[] { "SupportedFeatureProfilesMaxLength", "5", "R" },
            ////////////////////////////////////////////30
            new string[] { "TransactionMessageAttempts", "0", "RW" },
            new string[] { "TransactionMessageRetryInterval", "10", "RW" },
            new string[] { "UnlockConnectorOnEVSideDisconnect", "0", "RW" },
            new string[] { "WebSocketPingInterval", "20", "RW" },
            //9.2. Local Auth List Management Profile
            new string[] { "LocalAuthListEnabled", "false", "RW" }
      };
        private void InsertInitialData()
        {


            foreach (var setting in SETTING_DATA)
            {
                var insertQuery = @"
                INSERT INTO " + NAME_TABLE + @" (SettingKey, SettingValue, AccessType)
                VALUES (@SettingKey, @SettingValue, @AccessType);";

                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@SettingKey", setting[0]);
                    command.Parameters.AddWithValue("@SettingValue", setting[1]);
                    command.Parameters.AddWithValue("@AccessType", setting[2]);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }


        public DataTable selectDT()
        {
            string query = "SELECT * FROM OCPP_Setting_Table";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
        public DataTable selectData(string field)
        {
            string query = $"SELECT * FROM OCPP_Setting_Table where SettingKey ='{field}'";
            string result = "";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }              
        public int updateData(string field, string newValue)
        {
            string query = $"UPDATE OCPP_Setting_Table SET SettingValue = '{newValue}' WHERE SettingKey = '{field}'";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@newValue", newValue);
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows;
            }
        }

    }
}
