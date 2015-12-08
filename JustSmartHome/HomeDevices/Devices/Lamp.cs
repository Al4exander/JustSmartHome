using SmartHome.Enums;
namespace SmartHome
{
    public class Lamp : Device, IBrightnable, ISwitchable
    {
        public Brightness Bright { get; set; }

        public Lamp(bool status, Brightness bright) : base (status)
        {
            Bright = bright;
        }

        public void SetLowBright()
        {
            Bright = Brightness.low;
        }

        public void SetMiddleBright()
        {
            Bright = Brightness.middle;
        }

        public void SetHighBright()
        {
            Bright = Brightness.high;
        }


        public override string ToString()
        {
            string bright;
            string status;

            if (Bright == Brightness.low)
            {
                bright = "low";
            }
            else if (Bright == Brightness.middle)
            {
                bright = "middle";
            }
            else
            {
                bright = "high";
            }


            if (Status)
            {
                status = "on";
            }
            else
            {
                status = "off";
            }

            return "Power: " + status + ", brightness: " + bright;
        }
    }
}
