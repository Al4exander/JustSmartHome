using SmartHome.Enums;
namespace SmartHome
{
    public class Conditioner : Device, ISwitchable, IAddSubable
    {
        private int degrees;

        public int Degrees 
        {
            get 
            { 
                return degrees;
            }
            set
            {
                if (value >= 15 && value <= 30)
                {
                    degrees = value;
                }
            } 
        }

        public Conditioner(bool status, int degrees)
            : base(status)
        {
            Degrees = degrees;
        }

        public void AddOne()
        {
            ++Degrees;
        }

        public void SubOne()
        {
            --Degrees;
        }
       

       public override string ToString()
        {
           string status;

           if (Status)
           {
               status = "on";
           }
           else
           {
               status = "off";
           }
            
           return "Power is: " + status + ", degrees: " + Degrees;
        }
    }
}
