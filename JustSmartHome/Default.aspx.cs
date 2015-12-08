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
                panelForDevices.Controls.Add(new DeviceControl(key, devicesDictionary));
            }
        }

        protected void AddDeviceButtonClick(object sender, EventArgs e)
        {
            Device newDevice;
            switch(((ImageButton)sender).ID)
            {
                default:
                    newDevice = new Lamp(false,  Brightness.low);
                    break;
                case "AddTV":
                    newDevice = new TVSet(false,  1, 1);
                    break;
                case "AddAlarm":
                    newDevice = new AlarmSystem(false, "0000");
                    break;
                case "AddConditioner":
                    newDevice = new Conditioner(false, 15);
                    break;
                case "AddMicrovawe":
                    newDevice = new Microwave(false, 0, Mode.standart);
                    break;
            }
            int id = (int)Session["NextId"];
            devicesDictionary.Add(id, newDevice); 
            panelForDevices.Controls.Add(new DeviceControl(id, devicesDictionary));
            id++;
            Session["NextId"] = id;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["Devices"] = devicesDictionary;
        }
    }
}