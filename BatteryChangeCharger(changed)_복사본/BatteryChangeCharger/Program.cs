using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BatteryChangeCharger
{
    static class Program
    {
        static void exceptionDump(object sender, System.Threading.ThreadExceptionEventArgs args)
        {
            //Exception e = args.Exception;
            //Console.WriteLine("errMsg: " + e.Message);
            //Console.WriteLine("errPos: " + e.TargetSite);

            //덤프 파일 경로 설정(MinidumpHelp.cs 에서도 수정)
            //MinidumpHelp.Minidump.install_self_mini_dump(Application.StartupPath);

            MinidumpHelp.Minidump.install_self_mini_dump();
        }

        //이벤트 클래스(처리되지 않은 예외)
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //Exception e = args.Exception;
            //Console.WriteLine("errMsg: " + e.Message);
            //Console.WriteLine("errPos: " + e.TargetSite);

            //덤프 파일 경로 설정(MinidumpHelp.cs 에서도 수정)
            //MinidumpHelp.Minidump.install_self_mini_dump(Application.StartupPath);

            MinidumpHelp.Minidump.install_self_mini_dump();
        }


        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //이벤트 추가
            //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(exceptionDump);

            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
