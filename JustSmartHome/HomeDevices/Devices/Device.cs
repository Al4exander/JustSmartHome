namespace SmartHome
{
    public abstract class Device
    {
        public bool Status { get; set; }

        public Device(bool status)
        {
            Status = status;
        }

        public void OnIt()
        {
            Status = true;
        }

        public void ShutDown()
        {
            Status = false;
        }

    }
}
