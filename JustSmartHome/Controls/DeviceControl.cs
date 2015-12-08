using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome;
using SmartHome.Enums;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace JustSmartHome.Controls
{
    public class DeviceControl : Panel
    {
        private int id;
        private IDictionary<int, Device> devicesDictionary;
        private ImageButton lamp;
        private ImageButton onOff;
        private Button delete;
        private Image device;
        private Label info;
        private Image vol;
        private TextBox alarm;
        private ImageButton save;
        private ImageButton moreTemperature;
        private ImageButton lessTemperature;
        private DropDownList list;

        public DeviceControl (int id, IDictionary<int, Device> devicesDictionary)
	    {
            this.id = id;
            this.devicesDictionary = devicesDictionary;
            Initializer();
	    }

        protected void Initializer()
        {
            if(devicesDictionary[id] is Lamp)
            {
                AddLamp();
            }
            if(devicesDictionary[id] is TVSet)
            {
                AddTVSet();
            }
            if(devicesDictionary[id] is AlarmSystem)
            {
                AddAlarm();
            }
            if(devicesDictionary[id] is Conditioner)
            {
                AddConditioner();
            }
            if(devicesDictionary[id] is Microwave)
            {
                AddMicrowave();
            }
        }

        protected void AddLamp()
        {
            CssClass = "empty";
            lamp = new ImageButton();
            lamp.CssClass = "lamp";
            lamp.ImageUrl = "Images/lamp_start.png";
            lamp.Click += LampClick;
            lamp.ID = "lamp" + id.ToString();
            Controls.Add(lamp);
            Controls.Add(Span("<br />"));
            onOff = OnOffCreate(DeviceEnum.lamp);
            Controls.Add(onOff);
            delete = DeleteCreate();
            Controls.Add(delete);
            Controls.Add(Span("<br />"));
        }

        protected void AddTVSet()
        {
            string cClass = "bottom";
            CssClass = "empty";
            device = new Image();
            vol = new Image();

            ImageButton pChannel = new ImageButton();
            ImageButton nChannel = new ImageButton();
            ImageButton lessVolume = new ImageButton();
            ImageButton moreVolume = new ImageButton();

            delete = DeleteCreate();
            onOff = OnOffCreate(DeviceEnum.TVSet);

            pChannel.CssClass = cClass;
            pChannel.ImageUrl = "Images/previous_channel.png";
            pChannel.ID = "pChannel" + id.ToString();
            pChannel.Click += TVPChannelClick;
            nChannel.CssClass = cClass;
            nChannel.ImageUrl = "Images/next_channel.png";
            nChannel.ID = "nChannel" + id.ToString();
            nChannel.Click += TVNChannelClick;

            lessVolume.CssClass = cClass;
            lessVolume.ImageUrl = "Images/less_volume.png";
            lessVolume.ID = "lessVolume" + id.ToString();
            lessVolume.Click += TVLessVolumeClick;
            moreVolume.CssClass = cClass;
            moreVolume.ImageUrl = "Images/more_volume.png";
            moreVolume.ID = "moreVolume" + id.ToString();
            moreVolume.Click += TVMoreVolumeClick;

            if (((Device)devicesDictionary[id]).Status == true)
            {
                device.ImageUrl = "Images/Channel/channel_" + ((TVSet)devicesDictionary[id]).Channel + ".png";
                vol.ImageUrl = "Images/Volume/" + ((TVSet)devicesDictionary[id]).Volume + ".png";
            }
            else
            {
                vol.ImageUrl = "Images/Volume/null.png";
                device.ImageUrl = "Images/TVSet.png";
            }
            device.ID = "tv" + id.ToString();
            
            vol.CssClass = "vol";
            Controls.Add(Span("<center>"));
            Controls.Add(device);
            Controls.Add(Span("</center><br />"));
            Controls.Add(onOff);
            Controls.Add(Span("<div class='infoTV'>"));
            Controls.Add(pChannel);
            Controls.Add(nChannel);
            Controls.Add(Span("</div>"));
            Controls.Add(Span("<div class='infoTVVolume'"));
            Controls.Add(lessVolume);
            Controls.Add(moreVolume);
            Controls.Add(Span("</div>"));
            Controls.Add(vol);
            Controls.Add(delete);
        }

        protected void AddAlarm()
        {
            CssClass = "image";
            alarm = new TextBox();
            info = new Label();
            onOff = new ImageButton();
            delete = new Button();
            save = new ImageButton();

            info.CssClass = "infoAlarm";
            info.Text = "Пароль: " + ((AlarmSystem)devicesDictionary[id]).Password;
            save.ImageUrl = "Images/save.png";
            save.CssClass = "save";
            save.Click += AlarmSaveClick;
            onOff = OnOffCreate(DeviceEnum.alarm);
            delete = DeleteCreate();

            alarm.CssClass = "bggreen";
            alarm.Width = 100;
            alarm.Height = 25;
            Controls.Add(Span("<br />"));
            Controls.Add(Span("<div class='forText'>"));
            Controls.Add(alarm);
            Controls.Add(Span("</div>"));

            IList<Button> but = new List<Button>();
            Button b1 = new Button();
            Button b2 = new Button();
            Button b3 = new Button();
            Button b4 = new Button();
            Button b5 = new Button();
            Button b6 = new Button();
            Button b7 = new Button();
            Button b8 = new Button();
            Button b9 = new Button();
            but.Add(b1);
            but.Add(b2);
            but.Add(b3);
            but.Add(b4);
            but.Add(b5);
            but.Add(b6);
            but.Add(b7);
            but.Add(b8);
            but.Add(b9);
            Controls.Add(Span("<div class='forButtons'>"));
            foreach (Button b in but)
            {
                b.ID = "button" + id.ToString() + (but.IndexOf(b) + 1).ToString();
                b.Click += AlarmButtonsClick;
                b.Text = (but.IndexOf(b) + 1).ToString();
                Controls.Add(b);
                if (but.IndexOf(b) == 2 || but.IndexOf(b) == 5 || but.IndexOf(b) == 8)
                {
                    Controls.Add(Span("<br />"));
                }
            }
            Controls.Add(Span("</div>"));
            if (((Device)devicesDictionary[id]).Status == false)
            {
                save.Visible = false;
                alarm.Enabled = false;
                info.Visible = false;
            }
            Controls.Add(info);
            Controls.Add(Span("<br />"));
            Controls.Add(Span("<br />"));
            Controls.Add(Span("<br />"));
            Controls.Add(Span("<br />"));

            Controls.Add(onOff);
            Controls.Add(save);
            Controls.Add(delete);
        }

        protected void AddConditioner()
        {
            bool status = ((Device)devicesDictionary[id]).Status;
            CssClass = "conditioner";
            device = new Image();
            lessTemperature = new ImageButton();
            info = new Label();
            moreTemperature = new ImageButton();
            delete = DeleteCreate();
            onOff = OnOffCreate(DeviceEnum.conditioner);

            device.CssClass = "conditionerImage";
            device.ID = "device" + id.ToString();
            info.CssClass = "forConditioner";
            info.Text = (((Conditioner)devicesDictionary[id]).Degrees).ToString();
            lessTemperature.ImageUrl = "Images/less_temperature.png";
            lessTemperature.Click += ConditionerTempClick;
            lessTemperature.CssClass = "forConditioner";
            lessTemperature.ID = "lessTemperature" + id.ToString();
            moreTemperature.ImageUrl = "Images/more_temperature.png";
            moreTemperature.Click += ConditionerTempClick;
            moreTemperature.CssClass = "forConditioner";
            moreTemperature.ID = "moreTemperature" + id.ToString();

            if(status)
            {
                device.ImageUrl = "Images/conditioner_ison.png";
            }
            else
            {
                device.ImageUrl = "Images/conditioner_isoff.png";
                lessTemperature.Visible = false;
                info.Visible = false;
                moreTemperature.Visible = false;
            }

            Controls.Add(Span("<center>"));
            Controls.Add(device);
            Controls.Add(Span("</center>"));
            Controls.Add(Span("<br /><br />"));
            Controls.Add(onOff);
            Controls.Add(lessTemperature);
            Controls.Add(info);
            Controls.Add(moreTemperature);
            Controls.Add(delete);
        }

        protected void AddMicrowave()   
        {
            device = new Image();
            info = new Label();
            Button b1 = new Button();
            Button b2 = new Button();
            Button b3 = new Button();
            Button b4 = new Button();
            Button start = new Button();
            list = new DropDownList();
            onOff = OnOffCreate(DeviceEnum.microwave);
            delete = DeleteCreate();

            CssClass = "microwaveMain";

            info.CssClass = "label";
            info.ID = "info" + id.ToString();
            b1.Text = "+5с";
            b1.CssClass = "microwaveButtons";
            b1.Click += MicrowaveButtonsClick;
            b1.ID = "microwaveButton5" + id.ToString();

            b2.Text = "+30с";
            b2.CssClass = "microwaveButtons";
            b2.Click += MicrowaveButtonsClick;
            b2.ID = "microwaveButton30" + id.ToString();

            b3.Text = "-5с";
            b3.CssClass = "microwaveButtons";
            b3.Click += MicrowaveButtonsClick;
            b3.ID = "microwaveSubButton5" + id.ToString();

            b4.Text = "-30с";
            b4.CssClass = "microwaveButtons";
            b4.Click += MicrowaveButtonsClick;
            b4.ID = "microwaveSubButton30" + id.ToString();

            start.Text = "Start";
            start.CssClass = "microwaveStart";
            start.Click += MicrowaveStartClick;
            start.ID = "start" + id.ToString();

            list.ID = "dropList" + id.ToString();
            list.SelectedIndexChanged += MicrowaveDropList;
            list.Items.Add("Standart");
            list.Items.Add("Grill");
            list.Items.Add("Defrost");
            if(((Device)devicesDictionary[id]).Status == false)
            {
                list.Visible = false;
            }
            //Добавление в Controls нужных элементов управления
            Controls.Add(info);
            Controls.Add(Span("<br /><br />"));
            Controls.Add(b1);
            Controls.Add(Span("<br />"));
            Controls.Add(b2);
            Controls.Add(Span("<br />"));
            Controls.Add(b3);
            Controls.Add(Span("<br />"));
            Controls.Add(b4);
            Controls.Add(Span("<br />"));
            Controls.Add(Span("<div class='infoMicrowave'"));
            Controls.Add(start);
            Controls.Add(Span("</div>"));
            Controls.Add(Span("<br />"));
            Controls.Add(onOff);
            Controls.Add(list);
            Controls.Add(delete);
            
        }

        protected ImageButton OnOffCreate(DeviceEnum dev)
        {
            ImageButton onOff = new ImageButton();
            if(((Device)devicesDictionary[id]).Status == true)
            {
                onOff.ImageUrl = "Images/on.png";
            }
            else
            {
                onOff.ImageUrl = "Images/off.png";
            }
            onOff.ID = "onOff" + id.ToString();
            onOff.CssClass = "OnOff";
            switch(dev)
            {
                case DeviceEnum.lamp:
                    onOff.Click += LampOnOffClick;
                    break;
                case DeviceEnum.TVSet:
                    onOff.Click += TVOnOffClick;
                    break;
                case DeviceEnum.alarm:
                    onOff.Click += AlarmOnOffClick;
                    break;
                case DeviceEnum.conditioner:
                    onOff.Click += ConditionerOnOffClick;
                    break;
                case DeviceEnum.microwave:
                    onOff.Click += MicrowaveOnOffClick;
                    break;
            }
            return onOff;
        }

        protected Button DeleteCreate()
        {
            Button delete = new Button();
            delete.CssClass = "delete";
            delete.Click += DeleteClick;
            delete.ID = "delete" + id.ToString();
            return delete;
        }

        protected HtmlGenericControl Span(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
        }

        protected void MicrowaveButtonsClick(object sender, EventArgs e)
        {
            if(((Device)devicesDictionary[id]).Status == true)
            {
                if ((((Button)sender).ID).StartsWith("microwaveButton5"))
                {
                    ((Microwave)devicesDictionary[id]).AddFiveSec();
                }
                else if ((((Button)sender).ID).StartsWith("microwaveButton30"))
                {
                    ((Microwave)devicesDictionary[id]).AddThirty();
                }
                else if ((((Button)sender).ID).StartsWith("microwaveSubButton5"))
                {
                    ((Microwave)devicesDictionary[id]).SubFiveSec();
                }
                else
                {
                    ((Microwave)devicesDictionary[id]).SubThirty();
                }
                info.Text = (((Microwave)devicesDictionary[id]).Time).ToString();
            }
        }

        protected void MicrowaveStartClick(object sender, EventArgs e)
        {
            if(((Device)devicesDictionary[id]).Status == true)
            {
                ((Microwave)devicesDictionary[id]).Start();
                info.Text = (((Microwave)devicesDictionary[id]).Time).ToString();
                Controls.Add(Span("<div>" + (((Microwave)devicesDictionary[id]).AMode) + ": "));
                 Controls.Add(Span("<span id='timer_inp'>" + (((Microwave)devicesDictionary[id]).Time)));
                Controls.Add(Span("</span></div>"));
            }
        }

        protected void MicrowaveOnOffClick(object sender, EventArgs e)
        {
            if (((Device)devicesDictionary[id]).Status == true)
            {
                list.Visible = false;
                info.Visible = false;
                ((Microwave)devicesDictionary[id]).ShutDown();
                ((ImageButton)sender).ImageUrl = "Images/off.png";
            }
            else
            {
                info.Text = (((Microwave)devicesDictionary[id]).Time).ToString();
                list.Visible = true;
                info.Visible = true;
                ((Microwave)devicesDictionary[id]).OnIt();
                ((ImageButton)sender).ImageUrl = "Images/on.png";
            }
        }

        protected void MicrowaveDropList(object sender, EventArgs e)
        {
            ((Microwave)devicesDictionary[id]).AMode = (Mode)list.SelectedIndex;
        }

        protected void ConditionerTempClick(object sender ,EventArgs e)
        {
            if((((ImageButton)sender).ID).StartsWith("more"))
            {
                ((Conditioner)devicesDictionary[id]).AddOne();
                info.Text = (((Conditioner)devicesDictionary[id]).Degrees).ToString();
            }
            else
            {
                ((Conditioner)devicesDictionary[id]).SubOne();
                info.Text = (((Conditioner)devicesDictionary[id]).Degrees).ToString();
            }
        }

        protected void ConditionerOnOffClick(object sender, EventArgs e)
        {
            if (((Device)devicesDictionary[id]).Status == true)
            {
                lessTemperature.Visible = false;
                info.Visible = false;
                moreTemperature.Visible = false;
                device.ImageUrl = "Images/conditioner_isoff.png";
                ((Conditioner)devicesDictionary[id]).ShutDown();
                ((ImageButton)sender).ImageUrl = "Images/off.png";
            }
            else
            {
                lessTemperature.Visible = true;
                info.Visible = true;
                moreTemperature.Visible = true;
                device.ImageUrl = "Images/conditioner_ison.png";
                ((Conditioner)devicesDictionary[id]).OnIt();
                ((ImageButton)sender).ImageUrl = "Images/on.png";
            }
        }

        protected void AlarmOnOffClick(object sender, EventArgs e)
        {
            if (((Device)devicesDictionary[id]).Status == true)
            {
                alarm.Enabled = false;
                alarm.Text = "";
                save.Visible = false;
                info.Visible = false;
                ((AlarmSystem)devicesDictionary[id]).ShutDown();
                ((ImageButton)sender).ImageUrl = "Images/off.png";
            }
            else
            {
                alarm.Enabled = true;
                save.Visible = true;
                info.Visible = true;
                ((AlarmSystem)devicesDictionary[id]).OnIt();
                ((ImageButton)sender).ImageUrl = "Images/on.png";
            }
        }

        protected void AlarmButtonsClick(object sender, EventArgs e)
        {
            if(((Device)devicesDictionary[id]).Status)
            {
                string str = (((Button)sender).ID).Remove(0, 7);
                switch (str)
                {
                    default:
                        break;
                    case "1":
                        alarm.Text += "1";
                        break;
                    case "2":
                        alarm.Text += "2";
                        break;
                    case "3":
                        alarm.Text += "3";
                        break;
                    case "4":
                        alarm.Text += "4";
                        break;
                    case "5":
                        alarm.Text += "5";
                        break;
                    case "6":
                        alarm.Text += "6";
                        break;
                    case "7":
                        alarm.Text += "7";
                        break;
                    case "8":
                        alarm.Text += "8";
                        break;
                    case "9":
                        alarm.Text += "9";
                        break;
                }
            }
        }

        protected void AlarmSaveClick(object sender, EventArgs e)
        {
                if(alarm.Text != "" && alarm.Text.Length >= 3 && alarm.Text.Length <= 10)
                {
                    ((AlarmSystem)devicesDictionary[id]).Password = alarm.Text;
                    info.Text = "Пароль: " + alarm.Text;
                }
        }

        protected void TVOnOffClick(object sender, EventArgs e)
        {
            if (((Device)devicesDictionary[id]).Status == true)
            {
                device.ImageUrl = "Images/TVSet.png";
                ((TVSet)devicesDictionary[id]).ShutDown();
                ((ImageButton)sender).ImageUrl = "Images/off.png";
                vol.ImageUrl = "Images/Volume/null.png";
            }
            else
            {
                device.ImageUrl = "Images/Channel/channel_" + ((TVSet)devicesDictionary[id]).Channel + ".png";
                ((TVSet)devicesDictionary[id]).OnIt();
                ((ImageButton)sender).ImageUrl = "Images/on.png";
                vol.ImageUrl = "Images/Volume/" + ((TVSet)devicesDictionary[id]).Volume + ".png";
            }
        }

        protected void TVPChannelClick(object sender, EventArgs e)
        {
            if(((Device)devicesDictionary[id]).Status == true)
            {
                ((TVSet)devicesDictionary[id]).SubOne();
                device.ImageUrl = "Images/Channel/channel_" + ((TVSet)devicesDictionary[id]).Channel + ".png";
            }
        }

        protected void TVNChannelClick(object sender, EventArgs e)
        {
            if (((Device)devicesDictionary[id]).Status == true)
            {
                ((TVSet)devicesDictionary[id]).AddOne();
                device.ImageUrl = "Images/Channel/channel_" + ((TVSet)devicesDictionary[id]).Channel + ".png";
            }
        }

        protected void TVLessVolumeClick(object sender, EventArgs e)
        {
            if (((Device)devicesDictionary[id]).Status == true)
            {
                ((TVSet)devicesDictionary[id]).LessVolume();
                vol.ImageUrl = "Images/Volume/" + ((TVSet)devicesDictionary[id]).Volume + ".png";
            }
            else
            {
                vol.ImageUrl = "Images/Volume/null.png";
            }
        }

        protected void TVMoreVolumeClick(object sender, EventArgs e)
        {
            if (((Device)devicesDictionary[id]).Status == true)
            {
                ((TVSet)devicesDictionary[id]).MoreVolume();
                vol.ImageUrl = "Images/Volume/" + ((TVSet)devicesDictionary[id]).Volume + ".png";
            }
            else
            {
                vol.ImageUrl = "Images/Volume/null.png";
            }
        }

        protected void LampClick(object sender, EventArgs e)
        {
            if (((Device)devicesDictionary[id]).Status == false)
            {
                lamp.ImageUrl = "Images/lamp_start.png";
            }
            else
            {
                switch (((ImageButton)sender).ImageUrl)
                {
                    case "Images/lamp_start.png":
                        lamp.ImageUrl = "Images/lamp_low.png";
                        ((Lamp)devicesDictionary[id]).SetLowBright();
                        break;
                    case "Images/lamp_low.png":
                        lamp.ImageUrl = "Images/lamp_medium.png";
                        ((Lamp)devicesDictionary[id]).SetMiddleBright();
                        break;
                    case "Images/lamp_medium.png":
                        lamp.ImageUrl = "Images/lamp_high.png";
                        ((Lamp)devicesDictionary[id]).SetHighBright();
                        break;
                    case "Images/lamp_high.png":
                        lamp.ImageUrl = "Images/lamp_low.png";
                        ((Lamp)devicesDictionary[id]).SetLowBright();
                        break;
                }
            }
        }

        protected void LampOnOffClick(object sender, EventArgs e)
        {
            if(((Device)devicesDictionary[id]).Status == true)
            {
                lamp.ImageUrl = "Images/lamp_start.png";
                ((Lamp)devicesDictionary[id]).ShutDown();
                ((ImageButton)sender).ImageUrl = "Images/off.png";
            }
            else
            {
                switch(((Lamp)devicesDictionary[id]).Bright)
                {
                    case SmartHome.Enums.Brightness.low :
                        lamp.ImageUrl = "Images/lamp_low.png";
                        break;
                    case SmartHome.Enums.Brightness.middle:
                        lamp.ImageUrl = "Images/lamp_medium.png";
                        break;
                    case SmartHome.Enums.Brightness.high:
                        lamp.ImageUrl = "Images/lamp_high.png";
                        break;
                }
                ((Lamp)devicesDictionary[id]).OnIt();
                ((ImageButton)sender).ImageUrl = "Images/on.png";
            }
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            devicesDictionary.Remove(id);
            Parent.Controls.Remove(this);
        }
    }
}