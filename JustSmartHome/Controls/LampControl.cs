using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace JustSmartHome.Controls
{
    public class LampControl : Panel
    {
        private int id;
        private IDictionary<int, Device> devicesDictionary;
        private ImageButton lamp;
        private ImageButton onOff;
        private Button delete;

        public LampControl (int id, IDictionary<int, Device> devicesDictionary)
	    {
            this.id = id;
            this.devicesDictionary = devicesDictionary;
            Initializer();
	    }

        protected void Initializer()
        {
            CssClass = "empty";
            lamp = new ImageButton();
            lamp.CssClass = "lamp";
            lamp.ImageUrl = "Images/lamp_start.png";
            lamp.Click += LampClick;
            lamp.ID = "lamp" + id.ToString();
            Controls.Add(lamp);
            Controls.Add(Span("<br />"));
            onOff = OnOffCreate();
            Controls.Add(onOff);
            delete = DeleteCreate();
            Controls.Add(delete);
            Controls.Add(Span("<br />"));
        }

        protected HtmlGenericControl Span(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
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
            if (((Device)devicesDictionary[id]).Status == true)
            {
                lamp.ImageUrl = "Images/lamp_start.png";
                ((Lamp)devicesDictionary[id]).ShutDown();
                ((ImageButton)sender).ImageUrl = "Images/off.png";
            }
            else
            {
                switch (((Lamp)devicesDictionary[id]).Bright)
                {
                    case SmartHome.Enums.Brightness.low:
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

        protected ImageButton OnOffCreate()
        {
            ImageButton onOff = new ImageButton();
            if (((Device)devicesDictionary[id]).Status == true)
            {
                onOff.ImageUrl = "Images/on.png";
            }
            else
            {
                onOff.ImageUrl = "Images/off.png";
            }
            onOff.ID = "onOff" + id.ToString();
            onOff.CssClass = "OnOff";
            onOff.Click += LampOnOffClick;
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

        private void DeleteClick(object sender, EventArgs e)
        {
            devicesDictionary.Remove(id);
            Parent.Controls.Remove(this);
        }
    }
}