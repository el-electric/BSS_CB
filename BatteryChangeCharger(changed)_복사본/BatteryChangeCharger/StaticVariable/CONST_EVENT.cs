namespace BatteryChangeCharger.StaticVariable
{
    public class CONST_EVENT
    {
        private const int DIVIDE_BUTTON = 1000;
        public const int EVENT_CLICK_BUTTON_CONFIRM = DIVIDE_BUTTON + 1;
        public const int EVENT_CLICK_BUTTON_CANCEL = DIVIDE_BUTTON + 2;
        public const int EVENT_CLICK_BUTTON_BACK = DIVIDE_BUTTON + 3;
        public const int EVENT_CLICK_BUTTON_START = DIVIDE_BUTTON + 4;




        private const int DIVIDE_CONTROLLER = 2000;
        public const int EVENT_PROCESS_COMPLETE = DIVIDE_CONTROLLER + 1;
        public const int EVENT_PROCESS_FAILED = DIVIDE_CONTROLLER + 2;



        private const int DIVIDE_SYSTEM = 3000;
        public const int EVENT_CERTIFICATION_COMPLETE = DIVIDE_SYSTEM + 1;
    }
}
