using SmartHome.Enums;
namespace SmartHome
{
    public class Microwave : Device, ISwitchable
    {
        public Mode AMode { get; set; }
        private int time;
        
        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                if (value >= 0 && value < 5999)
                {
                    time = value;
                }
            }
        }

        public Microwave(bool status, int time, Mode amode)
            : base(status)
        {
            Time = time;
            AMode = amode;
        }

        public void AddFiveSec()
        {
                Time += 5;
        }

        public void SubFiveSec()
        {
            Time -= 5;
        }

        public void AddThirty()
        {
                Time += 30;
        }

        public void SubThirty()
        {
            Time -= 30;
        }

        public void StandartMode()
        {
            AMode = Mode.standart;
        }

        public void GrillMode()
        {
            AMode = Mode.grill;
        }

        public void DefrostMode()
        {
            AMode = Mode.defrost;
        }

        public void Start()
        {
            if (time == 0)
            {
                time = 30;
            }
        }

        public override string ToString()
        {
            string mode;

            if (AMode == Mode.defrost)
            {
                mode = "defrost";
            }
            else if (AMode == Mode.grill)
            {
                mode = "grill";
            }
            else
            {
                mode = "standart";
            }

            if (Status)
            {
                return "Power: on, mode: " + mode + ", time: " + Time;
            }
            else
            {
                return "Power: off";
            }
        }

    }
}
