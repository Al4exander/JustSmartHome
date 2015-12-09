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
    public class AlarmControl : Panel
    {
        private int id;
        private IDictionary<int, Device> devicesDictionary;
        private ImageButton onOff;
        private Button delete;
        private Label info;
        private TextBox alarm;
        private ImageButton save;

        public AlarmControl(int id, IDictionary<int, Device> devicesDictionary)
        {
            this.id = id;
            this.devicesDictionary = devicesDictionary;
            Initializer();
        }

        protected void Initializer()
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
            onOff = OnOffCreate();
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
            onOff.Click += AlarmOnOffClick;
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
            if (((Device)devicesDictionary[id]).Status)
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
            if (alarm.Text != "" && alarm.Text.Length >= 3 && alarm.Text.Length <= 10)
            {
                ((AlarmSystem)devicesDictionary[id]).Password = alarm.Text;
                info.Text = "Пароль: " + alarm.Text;
            }
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            devicesDictionary.Remove(id);
            Parent.Controls.Remove(this);
        }

    }
}