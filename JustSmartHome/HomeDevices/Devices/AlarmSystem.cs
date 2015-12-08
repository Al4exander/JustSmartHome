namespace SmartHome
{
    public class AlarmSystem : Device, ISwitchable
    {
        public string Password { get; set; }

        public AlarmSystem(bool status, string password)
            : base(status)
        {
            Password = password;
        }

        public override string ToString()
        {
            if (Status)
            {
                return "Power: on, password: " + Password;
            }
            else
            {
                return "Power: off";
            }
        }
    }
}
