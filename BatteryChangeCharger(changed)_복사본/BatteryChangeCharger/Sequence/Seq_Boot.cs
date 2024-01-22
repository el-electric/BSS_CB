using BatteryChangeCharger.Applications;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BatteryChangeCharger.Sequence
{
    public class Seq_Boot
    {
        private static int CurrentStep = 0;
        public static void MainCycle()
        {

            //switch (CsDefine.Cyc_Rail[CsDefine.CYC_BOOT])
            //{

            //    case CsDefine.CYC_WORK:
            //        CurrentStep = 0;
            //        MyApplication.getInstance().conf_BootNotification = new Conf_BootNotification();
            //        MyApplication.getInstance().oCPP_Comm_SendMgr.sendOCPP_CP_Req_BootNotification();
            //        NextStep();
            //        break;
            //    case CsDefine.CYC_WORK + 1:
            //        if (MyApplication.getInstance().conf_BootNotification.status == RegistrationStatus.Accepted)
            //        {

            //        }
            //        else if (MyApplication.getInstance().conf_BootNotification.status == RegistrationStatus.Rejected)
            //        {
            //            //재시도
            //            if (dt.AddSeconds((double)MyApplication.getInstance().conf_BootNotification.interval) <= DateTime.Now)
            //            {
            //                step = 0;
            //            }
            //        }
            //        break;
            //    case Main:

            //        MyApplication.getInstance().oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(0, ChargePointErrorCode.NoError, ChargePointStatus.Available);
            //        MyApplication.getInstance().oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Available);

            //        MyApplication.getInstance().oCPP_Comm_SendMgr.sendOCPP_CP_Req_HearthBeat();
            //        NextStep();
            //        break;
            //}
        }
        private static void NextStep()
        {
            CsDefine.Delayed[CsDefine.CYC_BOOT] = 0;
            CsDefine.Cyc_Rail[CsDefine.CYC_BOOT] = ++CurrentStep;
        }
    }
}
