using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NfcProgramming
{
    public enum PROTOCOL_18092
    {
        ATR_RES = 0x01,  // Attribute Request CMD2 Byte
        WUP_RES = 0x03,  // Wakeup Request CMD2 Byte
        PSL_RES = 0x05,  // Parameter Selection Request CMD2 Byte
        DEP_RES = 0x07,  // Data Exchange Protocol Request CMD2 Byte
        DSL_RES = 0x09,  // Deselect Request CMD2 Byte
        RLS_RES = 0x0B,  // Release Request CMD2 Byte
        /**/
        ATR_REQ = 0x00,  // Attribute Response CMD2 Byte
        WUP_REQ = 0x02,  // Wakeup Response CMD2 Byte
        PSL_REQ = 0x04,  // Parameter Selection Response CMD2 Byte
        /**/
        DEP_REQ = 0x06,  // Data Exchange Protocol Response CMD2 Byte
        DSL_REQ = 0x08,  // Deselect Response CMD2 Byte
        /**/
        RLS_REQ = 0x0A  // Release Response CMD2 Byte
    }

    public enum CONNECTION_STATUS
    { 
        RECEIVE_DIAL = 0x00,
        RECEIVE_CONNECTED = 0x01,
        RECEIVE_FINISHED = 0x02,
        RECEIVE_DISCONNECTED = 0x03
    }
    
    public enum LLC_TYPE
    { 
        SYMMETRY = 0x00,
        PARAMETER_EXCHANGE = 0x01,
        AGGREGATED_FRAME = 0x02,
        UNNUMBERED_INFORMATION = 0x03,
        CONNECT = 0x04,
        DISCONNECT = 0x05,
        CONNECTION_COMPLETE = 0x06,
        DISCONNECTED_MODE = 0x07,
        FRAME_REJECT = 0x08,
        SERVICE_NAME_LOOKUP = 0x09,
        INFORMATION = 0x0C,
        RECEIVE_READY = 0x0D,
        RECEIVE_NOT_READY = 0x0E
    }
}
