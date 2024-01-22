using BatteryChangeCharger.Applications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    internal class WeatherAPITest
    {
        public void check_REH_API() // string[] args
        {
            Console.WriteLine(DateTime.Now.ToString("yyyyMMdd"));
            Console.WriteLine(DateTime.Now.ToString("HHmm"));

            int time = int.Parse(DateTime.Now.ToString("HH"));
            string base_date = DateTime.Now.ToString("yyyyMMdd");

            string base_time;
            // API에 요청할 base_time 설정 : 오픈API활용가이드 참조
            switch (time)
            {
                case 2:
                case 3:
                case 4:
                    base_time = "0200";
                    break;
                case 5:
                case 6:
                case 7:
                    base_time = "0500";
                    break;
                case 8:
                case 9:
                case 10:
                    base_time = "0800";
                    break;
                case 11:
                case 12:
                case 13:
                    base_time = "1100";
                    break;
                case 14:
                case 15:
                case 16:
                    base_time = "1400";
                    break;
                case 17:
                case 18:
                case 19:
                    base_time = "1700";
                    break;
                case 20:
                case 21:
                case 22:
                    base_time = "2000";
                    break;
                case 23:
                case 24:
                case 1:
                    base_time = "2300";
                    break;

                default:
                    base_time = "0500";
                    break;
            }

            // API 요청 URL 작성
            string url = "http://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/"; // URL(getUltraSrtNcst : 초단기실활조회, getUltraSrtFcst : 초단기예보조회, getVilageFcst : 동네예보조회, getFcstVersion : 예보버전조회
            url += "getVilageFcst";
            url += "?ServiceKey=" + "Reu%2B6o8FuGUyxkGZIFGD7gQbVmgDLy8X9cvzI%2Fg5QhU8f%2BfX2V%2FeVKuw%2FeUiDkwVNkoo815FXQrTJk9D9XPibA%3D%3D"; // Service Key
            //url += "&ServiceKey=-";
            url += "&numOfRows=10";             // 한페이지 결과 수(Default : 10)
            url += "&pageNo=1";                 // 페이지 번호(Default : 1)
            url += "&dataType=XML";             // 받을 자료형식(XML, JSON)
            //url += "&ftype=ODAM";
            url += "&base_date=" + base_date;   // 요청 날짜(yyMMdd)
            url += "&base_time=" + base_time;   // 요청 시간(HHmm)
            url += "&nx=54";                    // 요청 지역 x좌표
            url += "&ny=123";                   // 요청 지역 y좌료

            Console.WriteLine(url);

            // HttpWebRequest 이용한 응답 수신 방법
            var request = (HttpWebRequest)WebRequest.Create(url);   // 요청 URL을 기준으로 HTTP요청 인스턴스 생성
            request.Method = "GET"; // 방법은 GET(요청)으로 설정

            string results = string.Empty;
            HttpWebResponse response;   // HTTP응답 인스턴스 생성
            using (response = request.GetResponse() as HttpWebResponse) // 응답 요청
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());   // stream방식으로 결과 수시을 위한 인스턴스 생성
                results = reader.ReadToEnd();   // 수신된 결과 읽어오기
            }

            //Console.WriteLine(results);

            /* XML 응답자료 형식 : 참조
            < response >
                < header >
                    < resultCode > 00 </ resultCode >
                    < resultMsg > NORMAL_SERVICE </ resultMsg >
                </ header >
                < body >
                    < dataType > XML </ dataType >
                    < items >
                        < item >
                            < baseDate > 20200228 </ baseDate >
                            < baseTime > 0500 </ baseTime >
                            < category > POP </ category >
                            < fcstDate > 20200228 </ fcstDate >
                            < fcstTime > 0900 </ fcstTime >
                            < fcstValue > 30 </ fcstValue >
                            < nx > 1 </ nx >
                            < ny > 1 </ ny >
                        </ item >
                    < /items >
                < /body >
            < /response >
            */

            // 수신된 XML형식의 데이터를 컨트롤하기위해 XmlDocument 인스턴스를 생성
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(results);   // Stream결과를 XML형식으로 읽어오기
            XmlNodeList xmResponse = xml.GetElementsByTagName("response");  // <response></response> 기준으로 node 생성
            XmlNodeList xmlList = xml.GetElementsByTagName("item"); // <item></item> 기준으로 node 생성

            foreach (XmlNode node in xmResponse)    // xml의 <response> 값 읽어 들이기
            {
                if (node["header"]["resultMsg"].InnerText.Equals("NORMAL_SERVICE")) // 정상 응답일 경우
                {
                    foreach (XmlNode node1 in xmlList)  // <item> 값 읽어 들이기
                    {
                        if (node1["category"].InnerText.Equals("REH"))  // 습도(층전기에서 습도를 받을지 아니면 습도측정기를 설치할지)
                        {
                            /*Model.Charger_humnity = node1["fcstValue"].InnerText;*/
                            /*Model.Charger_humnity = node1["obsrValue"].InnerText;*/
                        }

                        if (node1["category"].InnerText.Equals("PTY"))  // 0 = 없음 , 1 = 비 , 2 = 비/눈 , 3 = 눈 , 4 = 소나기
                        {
                            MyApplication.getInstance().Morning_Weather_Info = Convert.ToInt32(node1["fcstValue"].InnerText);
                        }

                        if (node1["category"].InnerText.Equals("TMP"))  // 기온
                        {
                            MyApplication.getInstance().Morning_OneHour_Temp = Convert.ToInt32(node1["fcstValue"].InnerText);
                        }
                    }
                }
                else
                {
                    string apiErrorMsg = String.Empty;

                    // API 응답 에러 메세지 조사
                    /*apiErrorMsg = node["header"]["resultMsg"].InnerText switch
                    {
                        "APPLICATION_ERROR" => "어플리케이션 에러",
                        "DB_ERROR" => "데이터베이스 에러",
                        "NODATA_ERROR" => "데이터 없음",
                        "HTTP_ERROR" => "HTTP 에러",
                        "SERVICETIME_OUT" => "서비스 연결실패",
                        "INVALID_REQUEST_PARAMETER_ERROR" => "잘못된 요청 파라메터",
                        "NO_MANDATORY_REQUEST_PARAMETERS_ERROR" => "필수요청 파라메터가 없음",
                        "NO_OPENAPI_SERVICE_ERROR" => "해당 오픈 API서비스가 없거나 폐기됨",
                        "SERVICE_ACCESS_DENIED_ERROR" => "서비스 접근 거부",
                        "TEMPORARILY_DISABLE_THE_SERVICEKEY_ERROR" => "일시적으로 사용할 수 없는 서비스 키",
                        "LIMITED_NUMBER_OF_SERVICE_REQUESTS_EXCEEDS_ERROR" => "서비스 요청제한횟수 초과",
                        "SERVICE_KEY_IS_NOT_REGISTERED_ERROR" => "등록되지 않은 서비스 키",
                        "DEADLINE_HAS_EXPIRED_ERROR" => "기한 만료된 서비스 키",
                        "UNREGISTERED_IP_ERROR" => "등록되지 않은 IP",
                        "UNSIGNED_CALL_ERROR" => "서명되지 않은 호출",
                        "UNKNOWN_ERROR" => "기타에러",
                        _ => "해당하는 에러가 존재하지 않음",
                    };*/

                    // API 응답 에러 메세지 출력
                    Console.WriteLine(apiErrorMsg);
                }
            }
        }
    }
}
