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
    public class TVSetControl : Panel
    {
        private int id;
        private IDictionary<int, Device> devicesDictionary;
        private Image device;
        private Image vol;
        private ImageButton onOff;
        private Button delete;

        public TVSetControl(int id, IDictionary<int, Device> devicesDictionary)
	    {
            this.id = id;
            this.devicesDictionary = devicesDictionary;
            Initializer();
        }

        protected void Initializer()
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
            onOff = OnOffCreate();

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

        protected HtmlGenericControl Span(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
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
                    onOff.Click += TVOnOffClick;
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
            if (((Device)devicesDictionary[id]).Status == true)
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

        private void DeleteClick(object sender, EventArgs e)
        {
            devicesDictionary.Remove(id);
            Parent.Controls.Remove(this);
        }
    }
}