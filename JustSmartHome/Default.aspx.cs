using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartHome;
using SmartHome.Enums;
using JustSmartHome.Controls;

namespace JustSmartHome
{
    public partial class MainForm : System.Web.UI.Page
    {
        private IDictionary<int, Device> devicesDictionary;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            }
            else
            {
                devicesDictionary = new SortedDictionary<int, Device>();
                devicesDictionary.Add(1, new Lamp(false, Brightness.low));
                devicesDictionary.Add(2, new TVSet(false,  1, 1));
                devicesDictionary.Add(3, new Conditioner(false, 15));
                devicesDictionary.Add(4, new Microwave(false, 0, Mode.standart));
                devicesDictionary.Add(5, new AlarmSystem(false, "000"));

                Session["Devices"] = devicesDictionary;
                Session["NextId"] = 6;
            }
        }

        protected void Page_Load()
        {
            if (IsPostBack)
            {
                AddLamp.Click += AddDeviceButtonClick;
                AddTV.Click += AddDeviceButtonClick;
                AddAlarm.Click += AddDeviceButtonClick;
                AddConditioner.Click += AddDeviceButtonClick;
                AddMicrovawe.Click += AddDeviceButtonClick;
            }
            InitialiseDevicesPanel();
        }

        protected void InitialiseDevicesPanel()
        {
            foreach (int key in devicesDictionary.Keys)
            {
                if(devicesDictionary[key] is Lamp)
                {
                    panelForDevices.Controls.Add(new LampControl(key, devicesDictionary));
                }
                else if (devicesDictionary[key] is AlarmSystem)
                {
                    panelForDevices.Controls.Add(new AlarmControl(key, devicesDictionary));
                }
                else if (devicesDictionary[key] is TVSet)
                {
                    panelForDevices.Controls.Add(new TVSetControl(key, devicesDictionary));
                }
                else if (devicesDictionary[key] is Microwave)
                {
                    panelForDevices.Controls.Add(new MicrowaveControl(key, devicesDictionary));
                }
                else if(devicesDictionary[key] is Conditioner)
                {
                    panelForDevices.Controls.Add(new ConditionerControl(key, devicesDictionary));
                }
            }
        }

        protected void AddDeviceButtonClick(object sender, EventArgs e)
        {
            Device newDevice;
            int id = (int)Session["NextId"];
            switch(((ImageButton)sender).ID)
            {
                default:
                    newDevice = new Lamp(false,  Brightness.low);
                    devicesDictionary.Add(id, newDevice); 
                    panelForDevices.Controls.Add(new LampControl(id, devicesDictionary));
                    break;
                case "AddTV":
                    newDevice = new TVSet(false,  1, 1);
                    devicesDictionary.Add(id, newDevice); 
                    panelForDevices.Controls.Add(new TVSetControl(id, devicesDictionary));
                    break;
                case "AddAlarm":
                    newDevice = new AlarmSystem(false, "0000");
                    devicesDictionary.Add(id, newDevice); 
                    panelForDevices.Controls.Add(new AlarmControl(id, devicesDictionary));
                    break;
                case "AddConditioner":
                    newDevice = new Conditioner(false, 15);
                    devicesDictionary.Add(id, newDevice); 
                    panelForDevices.Controls.Add(new ConditionerControl(id, devicesDictionary));
                    break;
                case "AddMicrovawe":
                    newDevice = new Microwave(false, 0, Mode.standart);
                    devicesDictionary.Add(id, newDevice); 
                    panelForDevices.Controls.Add(new MicrowaveControl(id, devicesDictionary));
                    break;
            }
            id++;
            Session["NextId"] = id;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["Devices"] = devicesDictionary;
        }
    }
}