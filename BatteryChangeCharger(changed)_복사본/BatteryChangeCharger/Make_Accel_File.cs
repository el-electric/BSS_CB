using BatteryChangeCharger.Applications;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace BatteryChangeCharger
{
    public  class Make_Accel_File
    {
        static Excel.Application excelApp = null;
        static Excel.Workbook workBook = null;
        static Excel.Worksheet workSheet = null;

        public Nullable<DateTime> Test_Time = null;

        public void make_excel_file2()
        {
            if (Test_Time == null)
            {
                Test_Time = DateTime.Now;
            }
            else if (DateTime.Now >= Test_Time.Value.AddSeconds(10))
            {
                try
                {
                    /*if (!Directory.Exists(Application.StartupPath + "\\" + fileName + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0')))
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\" + fileName + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0'));
                    }*/
                    // string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  // 바탕화면 경로
                    string desktopPath = System.Windows.Forms.Application.StartupPath + "\\" +"Log_For_Test"+ "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0');
                    string path = Path.Combine(desktopPath, "Test_File_BSS.xlsx");                      // 엑셀 파일 저장 경로

                    excelApp = new Excel.Application();                             // 엑셀 어플리케이션 생성
                    workBook = excelApp.Workbooks.Add();                            // 워크북 추가
                    workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet; // 엑셀 첫번째 워크시트 가져오기

                    workSheet.Cells[1, 1] = "날짜";
                    workSheet.Cells[1, 2] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    workSheet.Cells[2, 2] = "상부온도";
                    workSheet.Cells[2, 3] = "하부온도";
                    workSheet.Cells[3, 1] = "충전기";
                    workSheet.Cells[3, 2] = MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.Charger_Up_Temp;
                    workSheet.Cells[3, 3] = MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.Charger_Down_Temp;
                    workSheet.Cells[6, 2] = "안착여부";
                    workSheet.Cells[6, 3] = "파워팩 전압";
                    workSheet.Cells[6, 5] = "파워팩 전류";
                    workSheet.Cells[6, 7] = "배터리 최고 온도";
                    workSheet.Cells[6, 9] = "배터리 최저 온도";
                    workSheet.Cells[6, 11] = "배터리 SOC";
                    workSheet.Cells[6, 13] = "배터리 SOH";
                    workSheet.Cells[7, 1] = "슬롯 1";
                    workSheet.Cells[7, 2] = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[0].mPacket_c1_Receive.BatterArrive;
                    workSheet.Cells[7, 3] = (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[0].mPacket_c1_Receive.PowerPackVoltage / 100);
                    workSheet.Cells[7, 5] = (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[0].mPacket_c1_Receive.PowerPackcurrent / 100);
                    workSheet.Cells[7, 7] = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[0].mPacket_c1_Receive.BatteryMaxTemper;
                    workSheet.Cells[7, 9] = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[0].mPacket_c1_Receive.BatteryMinTemper;
                    workSheet.Cells[7, 11] = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[0].mPacket_c1_Receive.SOC;
                    workSheet.Cells[7, 13] = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[0].mPacket_c1_Receive.SOH;

                    // 엑셀에 저장할 개 데이터


                    workSheet.Columns.AutoFit();                                    // 열 너비 자동 맞춤
                    workBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookDefault);    // 엑셀 파일 저장
                    workBook.Close(true);
                    excelApp.Quit();
                }
                finally
                {
                    ReleaseObject(workSheet);
                    ReleaseObject(workBook);
                    ReleaseObject(excelApp);
                }
            }
        }

        static void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);  // 액셀 객체 해제
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();   // 가비지 수집
            }
        }

    }
}
