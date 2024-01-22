﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.datatype
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthorizationStatus
    {
        Accepted,
        Blocked,
        Expired,
        Invalid,
        ConcurrentTx,
        NeedCertifiy,
        UnsupportedOperator


    }
}
