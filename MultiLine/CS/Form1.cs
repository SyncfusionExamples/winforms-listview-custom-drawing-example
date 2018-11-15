using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.ListView.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GettingStarted
{
    public partial class Form1 : SfForm
    {
        #region constructor
        public Form1()
        {
            InitializeComponent();

            List<RTFText> rtfText = new List<RTFText>();
            rtfText.Add(new RTFText() { Text = "James Landon\r\nHappy birthday to an amazing employee!\r\nWishing you great achievements in this career, And I hope that you have a great day today!\r\nJan 12", Value = 0, Read = false });
            rtfText.Add(new RTFText() { Text = "Daniel Canden\r\nLike a vintage auto,your value increases...\r\nHappy birthday to one of the best and most loyal employees ever!\r\nJan 11", Value = 1, Read = false });
            rtfText.Add(new RTFText() { Text = "Holly Steve\r\nHappy Anniversary!Happy Anniversary!\r\nCongrats! May your life continue to be filled with love, laughter and happiness.\r\nJan 11", Value = 2, Read = false });
            rtfText.Add(new RTFText() { Text = "Jacob Oscar\r\nWe wish you an amazing year with accomplishment\r\nWe wish you an amazing year with accomplishment of the great goals that you have set!\r\nJan 10", Value = 3, Read = false });
            rtfText.Add(new RTFText() { Text = "Fiona Michael\r\nNo one could better job then..\r\nNo one could do a better job than the job you do.\r\nJan 10", Value = 4, Read = false });

            this.sfListView1.DataSource = rtfText;
            this.sfListView1.DisplayMember = "Text";
            this.sfListViewSettings();
            try
            {
                Bitmap bmp = new Bitmap(Image.FromFile(@"../../Icon/sficon.ico"));
                this.Icon = Icon.FromHandle(bmp.GetHicon());
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region sfListViewSeetings
        void sfListViewSettings()
        {
            this.sfListView1.Style.ItemStyle.Font = new Font("Microsoft Sans Serif", 9.75f);
            this.sfListView1.SelectionMode = SelectionMode.One;
            this.sfListView1.HotTracking = true;
            this.sfListView1.ItemHeight = 80;

            this.sfListView1.DrawItem += SfListView1_DrawItem;
        }

        private void SfListView1_DrawItem(object sender, Syncfusion.WinForms.ListView.Events.DrawItemEventArgs e)
        {
            if (e.ItemType == ItemType.Record)
            {
                //Custom drawing
                this.DrawRTFText(e);
                e.Handled = true;
            }
        }


        void DrawRTFText(Syncfusion.WinForms.ListView.Events.DrawItemEventArgs e)
        {
            var item = e.ItemData as RTFText;
            var split_text = item.Text.Split(new String[] { "\r\n" }, StringSplitOptions.None);
            var microSoft_10B = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold);
            var microSoft_10 = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
            var microSoft_8 = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
            var microSoft_7 = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
            Image favorite1 = Image.FromFile(@"..\..\Images\Favorites.png");
            Image favorite2 = Image.FromFile(@"..\..\Images\Favorites1.png");
            Bitmap bmp1 = new Bitmap(favorite2, new Size(40, 40));
            Bitmap bmp2 = new Bitmap(favorite1, new Size(40, 40));
            var richTexts = new RichText[]{
            new RichText(split_text[0],microSoft_10B),
            new RichText(split_text[1], microSoft_10),
            new RichText(split_text[2], microSoft_8),
        };
            Point textPosition = new Point(e.Bounds.Location.X, e.Bounds.Y);
            RectangleF rectf = e.Bounds;// new RectangleF(e.Bounds.Location.X + bmp1.Width, e.Bounds.Y, e.Bounds.Width - 17, e.Bounds.Height);
            int maxWidth = e.Bounds.Width;
            foreach (var richText in richTexts)
            {
                var text = richText.Text;
                if (string.IsNullOrEmpty(text)) { continue; }

                var font = richText.Font ?? Control.DefaultFont;
                //var textColor = Array.IndexOf(richTexts, richText) == 0 ? Color.Black : style.ForeColor;//Color.Black;//richText.TextColor ?? Control.DefaultForeColor;
                var textColor = e.Style.ForeColor;
                using (var brush = new SolidBrush(textColor))
                {
                    var rslt_Measure = e.Graphics.MeasureString(text, font, (int)rectf.Width);
                    e.Graphics.DrawString(text, font, brush, rectf);
                    var newRectF = rectf;
                    rectf.Y += (int)rslt_Measure.Height + 4;

                }

            }
            var date_Measure = e.Graphics.MeasureString(split_text[3], microSoft_7, e.Bounds.Width);
            e.Graphics.DrawString(split_text[3], microSoft_7, new SolidBrush(e.Style.ForeColor), new PointF(e.Bounds.Width - date_Measure.Width, e.Bounds.Y));
            //To avoid the border getting drawn top of the item.
            if (e.Bounds.Y == 0)
            {
                if (item.Read)
                    e.Graphics.DrawImage(bmp2, new PointF(rectf.Right - date_Measure.Width, e.Bounds.Y + e.Bounds.Height - bmp2.Height - 2));
                else
                    e.Graphics.DrawImage(bmp1, new PointF(rectf.Right - date_Measure.Width, e.Bounds.Y + e.Bounds.Height - bmp1.Height - 2));
            }
            else
            {
                e.Graphics.DrawLine(new Pen(e.Style.ForeColor), textPosition, new Point(e.Bounds.Width, textPosition.Y));
                if (item.Read)
                    e.Graphics.DrawImage(bmp2, new PointF(rectf.Right - date_Measure.Width, e.Bounds.Y + e.Bounds.Height - bmp2.Height - 2));
                else
                    e.Graphics.DrawImage(bmp1, new PointF(rectf.Right - date_Measure.Width, e.Bounds.Y + e.Bounds.Height - bmp1.Height - 2));
            }

        }

        #endregion

    }

    public class RTFText
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public bool Read { get; set; }
    }

    public class RichText
    {
        public Font Font { get; set; }
        public Color? TextColor { get; set; }
        public string Text { get; set; }

        public RichText() { }
        public RichText(string text)
        {
            this.Text = text;
        }
        public RichText(string text, Font font) : this(text)
        {
            this.Font = font;
        }
        public RichText(string text, Color textColor) : this(text)
        {
            this.TextColor = textColor;
        }
        public RichText(string text, Color textColor, Font font) : this(text, textColor)
        {
            this.Font = font;
        }
    }
}
