using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryChangeCharger
{
    public class CsDefine
    {
        public static int step = 0;

        public const int CYC_WORK = 0;

        public const int CYC_BOOT = 0;
        public const int CYC_PREPARING = 1;
        public const int CYC_MAIN = 2;
        public const int CYC_AUTHRIZE = 3;
        public const int CYC_CHARGING = 4;

        public static DateTime[] dt_beforeDealy = new DateTime[10];
        public static DateTime[] dt_beforeCount = new DateTime[10];

        public static int[] Delayed = new int[10];
        public static int[] Counted = new int[10];
        public static int[] Cyc_Rail = new int[10];
    }
}
