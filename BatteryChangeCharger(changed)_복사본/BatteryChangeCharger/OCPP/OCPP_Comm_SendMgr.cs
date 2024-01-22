using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.packet;
using EL_DC_Charger.ocpp.ver16.packet.common_action;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using EL_DC_Charger.ocpp.ver16.packet.csms2cp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParkingControlCharger.baseClass;
using BatteryChangeCharger.OCPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Emit;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BatteryChangeCharger.BatteryChange_Charger.Controller;
using BatteryChangeCharger.Manager;
using BatteryChangeCharger.Applications;
using EL_DC_Charger.ocpp.ver16.database;
using System.Data;

namespace EL_DC_Charger.ocpp.ver16.comm
{
    public class OCPP_Comm_SendMgr
    {

        protected EL_OCPP_Packet_Wrapper mPacket_SendPacket_Call_CP = null;
        public List<EL_OCPP_Packet_Wrapper> list_packet = new List<EL_OCPP_Packet_Wrapper>();

        ChargePointStatus before_Status;

        public void sendOCPP_CP_Req_BootNotification()
        {
            Req_BootNotification bootNotification = new Req_BootNotification();
            bootNotification.setRequiredValue("EL-BSS", "BSS_01");
            setSendPacket_Call_CP(
                bootNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1],
                JsonConvert.SerializeObject(bootNotification, MyApplication.mJsonSerializerSettings));
        }
        public void sendOCPP_CP_Req_StatusNotification(int ChannelIdx, ChargePointErrorCode errorCode, ChargePointStatus status)
        {

            if (before_Status != status)
                before_Status = status;
            else
                return;

            Req_StatusNotification statusNotification = new Req_StatusNotification();
            statusNotification.setRequiredValue(ChannelIdx, errorCode, status);

            setSendPacket_Call_CP(
                statusNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1],
                JsonConvert.SerializeObject(statusNotification, MyApplication.mJsonSerializerSettings));
        }
        public void sendOCPP_CP_Req_HearthBeat()
        {
            Req_Heartbeat req_Heartbeat = new Req_Heartbeat();

            setSendPacket_Call_CP(
                req_Heartbeat.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1],
                JsonConvert.SerializeObject(req_Heartbeat, MyApplication.mJsonSerializerSettings));
            MyApplication.getInstance().HeartBeatLastSendTime = DateTime.Now;
        }

        private void setSendPacket_Call_CP(String actionName, String payloadString)
        {


            JArray call_Packet = new JArray();
            call_Packet.Add(2);
            call_Packet.Add(Guid.NewGuid().ToString());
            call_Packet.Add(actionName);
            if (payloadString != null && payloadString.Length > 0)
            {
                //try
                //{
                call_Packet.Add(JObject.Parse(payloadString));

                //}
                //catch (JSONException e)
                //{
                //    e.printStackTrace();
                //}
            }
            //try
            //{
            mPacket_SendPacket_Call_CP =
                    new EL_OCPP_Packet_Wrapper(call_Packet[2].ToString(), call_Packet[1].ToString(), call_Packet);

            list_packet.Add(mPacket_SendPacket_Call_CP);
            MyApplication.getInstance().oCPP_Comm_Manager.SendMessageAsync(mPacket_SendPacket_Call_CP.mPacket.ToString());
        }

        public void ReceivedPacket(string _packet)
        {
            Logger.d("☆Receive☆ OCPP CSMS->CP Call => " + _packet.ToString());
            String callResult_message = "";
            JArray callResult_Packet = new JArray();


            try
            {
                JArray jsonArray = JArray.Parse(_packet);
                string _uid = jsonArray[1].ToString();

                callResult_Packet.Add(3);
                callResult_Packet.Add(jsonArray[1].ToString());

                switch ((int)jsonArray[0])
                {

                    //받은거
                    case 2:

                        if (jsonArray[2].ToString().Equals(EOCPP_Action_CSMS_Call.GetConfiguration.ToString()))
                        {
                            Req_GetConfiguration data = JsonConvert.DeserializeObject<Req_GetConfiguration>(((JObject)jsonArray[3]).ToString());
                            Conf_GetConfiguration data_Result = new Conf_GetConfiguration();
                            data_Result.configurationKey = new List<KeyValue>();

                            if (data.key != null && data.key.Count == 1 && data.key[0].Equals("SupportedFeatureProfiles"))
                            {
                                //                    String[] profiles = new String[]{"Core","FirmwareManagement","LocalAuthListManagement","Reservation","SmartCharging","RemoteTrigger"};
                                String[] profiles = new String[] { "Core" };
                                for (int i = 0; i < profiles.Length; i++)
                                {
                                    KeyValue key = new KeyValue();
                                    key.key = "SupportedFeatureProfiles";
                                    key.Readonly = true;
                                    key.value = profiles[i];

                                    data_Result.configurationKey.Add(key);
                                }
                            }
                            else if (data.key != null && data.key.Count > 0)
                            {
                                MyApplication.getInstance().oCPP_Manager_Table_Setting.selectData(data.key.ToString());
                            }
                            else
                            {
                                DataTable dt = MyApplication.getInstance().oCPP_Manager_Table_Setting.selectDT();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    KeyValue key = new KeyValue();
                                    key.key = dt.Rows[i][0].ToString();
                                    key.value = dt.Rows[i][1].ToString();

                                    if (dt.Rows[i][2].ToString().Equals("RW"))
                                        key.Readonly = true;
                                    else
                                        key.Readonly = false;

                                    data_Result.configurationKey.Add(key);
                                }
                            }

                            callResult_message = JsonConvert.SerializeObject(data_Result, MyApplication.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_GetConfiguration.class);
                            callResult_message = callResult_message.Replace("Readonly", "readonly");
                        }
                        else if (jsonArray[2].ToString().Equals(EOCPP_Action_CSMS_Call.ChangeConfiguration.ToString()))
                        {
                            String replaceText = ((JObject)jsonArray[3]).ToString().Replace("Readonly", "readonly");
                            Req_ChangeConfiguration data = JsonConvert.DeserializeObject<Req_ChangeConfiguration>(replaceText);
                            Conf_ChangeConfiguration data_Result = new Conf_ChangeConfiguration();

                            DataTable dt = MyApplication.getInstance().oCPP_Manager_Table_Setting.selectData(data.key);

                            if (dt.Rows.Count < 1)
                            {
                                data_Result.status = ConfigurationStatus.NotSupported;
                            }
                            else if (dt.Rows[0][2].ToString().ToUpper().Equals("RW") || dt.Rows[0][2].ToString().ToUpper().Equals("W"))
                            {
                                if (MyApplication.getInstance().oCPP_Manager_Table_Setting.updateData(data.key, data.value) > 0)
                                {
                                    data_Result.status = ConfigurationStatus.Accepted;
                                }
                            }

                            else
                            {
                                data_Result.status = ConfigurationStatus.Rejected;
                            }



                            callResult_message = JsonConvert.SerializeObject(data_Result, MyApplication.mJsonSerializerSettings);
                        }


                        if (callResult_message.Length > 0)
                        {
                            JObject obj_Payload = JObject.Parse(callResult_message);
                            callResult_Packet.Add(obj_Payload);
                        }
                        String callResult_Packet_String = callResult_Packet.ToString();
                        MyApplication.getInstance().oCPP_Comm_Manager.SendMessageAsync(callResult_Packet_String);
                        break;
                    //응답                    
                    case 3:
                        int foundIndex = -1;
                        for (int i = 0; i < list_packet.Count; i++)
                        {
                            Console.WriteLine(list_packet[i].mPacket[1]);
                            Console.WriteLine(_uid);
                            if (list_packet[i].mPacket.Count > 2 && list_packet[i].mPacket[1].ToString().Equals(_uid))
                            {
                                foundIndex = i;
                                break;
                            }
                        }
                        if (list_packet[foundIndex].mPacket[2].ToString().Equals(EOCPP_Action_CP_Call.BootNotification.ToString()))
                        {
                            Conf_BootNotification data = JsonConvert.DeserializeObject<Conf_BootNotification>(((JObject)jsonArray[2]).ToString());
                            MyApplication.getInstance().conf_BootNotification = data;

                            if (data.status == RegistrationStatus.Accepted)
                                MyApplication.getInstance().HeartBeatInterval = (int)data.interval;

                        }
                        else if (list_packet[foundIndex].mPacket[2].ToString().Equals(EOCPP_Action_CP_Call.Heartbeat.ToString()))
                        {
                            Conf_Heartbeat data = JsonConvert.DeserializeObject<Conf_Heartbeat>(((JObject)jsonArray[2]).ToString());
                        }




                        if (foundIndex != -1)
                        {
                            list_packet.RemoveAt(foundIndex);
                        }
                        break;

                    //에러
                    case 4:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + " 패킷 json 변환 실패");
            }
        }
    }
}
