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
    public class MicrowaveControl : Panel
    {
        private int id;
        private IDictionary<int, Device> devicesDictionary;
        private Image device;
        private Label info;
        private DropDownList list;
        private ImageButton onOff;
        private Button delete;

        public MicrowaveControl(int id, IDictionary<int, Device> devicesDictionary)
        {
            this.id = id;
            this.devicesDictionary = devicesDictionary;
            Initializer();
        }

        protected void Initializer()
        {
            device = new Image();
            info = new Label();
            Button b1 = new Button();
            Button b2 = new Button();
            Button b3 = new Button();
            Button b4 = new Button();
            Button start = new Button();
            list = new DropDownList();
            onOff = OnOffCreate();
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
            if (((Device)devicesDictionary[id]).Status == false)
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
            onOff.Click += MicrowaveOnOffClick;
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
            if (((Device)devicesDictionary[id]).Status == true)
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
            if (((Device)devicesDictionary[id]).Status == true)
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

        private void DeleteClick(object sender, EventArgs e)
        {
            devicesDictionary.Remove(id);
            Parent.Controls.Remove(this);
        }
    }
}