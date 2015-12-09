using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace JustSmartHome.Controls
{
    public class ConditionerControl : Panel
    {
        private int id;
        private IDictionary<int, Device> devicesDictionary;
        private ImageButton lessTemperature;
        private ImageButton moreTemperature;
        private Image device;
        private Label info;
        private ImageButton onOff;
        private Button delete;

        public ConditionerControl(int id, IDictionary<int, Device> devicesDictionary)
        {
            this.id = id;
            this.devicesDictionary = devicesDictionary;
            Initializer();
        }

        protected void Initializer()
        {
            CssClass = "conditioner";
            device = new Image();
            lessTemperature = new ImageButton();
            info = new Label();
            moreTemperature = new ImageButton();
            delete = DeleteCreate();
            onOff = OnOffCreate();

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

            if (((Device)devicesDictionary[id]).Status)
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
                   onOff.Click += ConditionerOnOffClick;
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
        protected void ConditionerTempClick(object sender, EventArgs e)
        {
            if ((((ImageButton)sender).ID).StartsWith("more"))
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

        private void DeleteClick(object sender, EventArgs e)
        {
            devicesDictionary.Remove(id);
            Parent.Controls.Remove(this);
        }
    }
}