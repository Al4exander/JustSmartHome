namespace SmartHome
{
    public class TVSet : Device, ISwitchable, IAddSubable
    {
        private int channel;
        private int volume;

        public int Channel
        {
            get 
            { 
                return channel;
            }
            set 
            {
                if (value > 0 && value <= 10)
                {
                    channel = value; 
                }
            }
        }

        public int Volume
        {
            get
            {
                return volume;
            }
            set
            {
                if (value >= 0 && value <=10)
                {
                    volume = value;
                }
            }
        }

        public TVSet(bool status, int channel, int volume)
            : base(status)
        {
            Channel = channel;
            Volume = volume;
        }

        public void AddOne()
        {
            if(this.Status == true)
            {
                if(this.channel == 10)
                {
                    Channel = 1;
                }
                else 
                {
                    ++Channel;
                }
            }
            else
            {
                return;
            }
        }

        public void SubOne()
        {
            if(this.Status == true)
            {
                if(this.channel == 1)
                {
                    Channel = 10;
                }
                else
                {
                    --Channel; 
                }
            }
            else
            {
                return;
            }      
        }

        public void AnotherChannel(int anotherChannel)
        {
            if (anotherChannel > 0)
            {
                Channel = anotherChannel;
            }
        }

        public void MoreVolume()
        {
            if(this.Status == true)
            {
                ++Volume;
            }
            else
            {
                return;
            }
        }

        public void LessVolume()
        {
            if (volume != 0)
            {
                --Volume;
            }
        }

        //public void OffOrOnVolume()
        //{
        //    if (volume == 0 && temp != 0)
        //    {
        //        volume = temp;
        //        temp = 0;
        //    }
        //    else if (volume != 0)
        //    {
        //        temp = volume;
        //        volume = 0;
        //    }
        //}

        public override string ToString()
        {
            if (Status)
            {
                return "Power: on, " + "channel: " + channel + ", volume: " + volume;  
            }
            else
            {
                return "Power: off";
            }
        }
    }
}
