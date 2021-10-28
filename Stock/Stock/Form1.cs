using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Stock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Title1.Text= "代號▼";
            Title2.Text= "名稱▼";
            Title3.Text = "價格▼";
            Title4.Text = "漲跌▼";
            Title5.Text = "漲跌幅▼";
            Title6.Text = "周漲跌▼";
            Title7.Text = "振幅▼";
            Title8.Text = "開盤▼";
            Title9.Text = "最高▼";
            Title10.Text = "最低▼";
            Title11.Text = "昨收▼";
            Title12.Text = "成交量▼";
            Title13.Text = " 成交值(億)▼";
        }

        string page = "1";
        public void button1_Click(object sender, EventArgs e)
        {
            string page = Page.Text;
            List<string> list = new List<string>();
            List<string> totallist = new List<string>();
            HtmlWeb web = new HtmlWeb();
            string[] stringSeparators = new string[] { "\r\n" };
            Menu.Text = "";
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://histock.tw/stock/rank.aspx?&p=" + page + "&d=1");
            HtmlNodeCollection data = doc.DocumentNode.SelectNodes("html/body/form/div[4]/div[5]");
            foreach (var i in data)
            {
                if (i.InnerText.Trim() != "")
                {
                    String[] lines = i.InnerHtml.Split(stringSeparators, StringSplitOptions.None);

                    foreach (var j in lines)
                    {
                        if(j.Trim().EndsWith("</td><td>") && j.Trim().Length >15 ||  j.Trim().Contains("_blank") && j.Trim().EndsWith("</a>") || !j.Trim().StartsWith("<div") && j.Trim().Contains("CPHB") && j.Trim().EndsWith("</span>") || j.Trim().StartsWith("</td><td>") && j.Trim().EndsWith("</td>") && j.Trim().Length >15)
                            list.Add(j.Trim());
                    }
                }
            }
            foreach (var i in list)
            {
                string str = "";
                if (i.StartsWith("<td>"))
                {
                    str = i.Substring(i.IndexOf("<td>"), i.IndexOf("</td><td>")).Replace("<td>", "");
                    totallist.Add(str);
                }
                else if (i.StartsWith("<a"))
                {
                    str = i.Substring(i.IndexOf(">")+1).Replace("</a>", "");
                    totallist.Add(str);
                }
                else if (i.StartsWith("<span"))
                {
                    str = i.Substring(i.IndexOf(">")+1).Replace("</span>", "");
                    totallist.Add(str);
                }
                else if (i.StartsWith("</td>"))
                {
                    str =i.Replace("</td>", "").Replace("<td>", "@");
                    String[] lastthreeitem = str.Split('@');
                    foreach(var j in lastthreeitem)
                    {
                        str = j.Trim(',');
                        if(str.Length>0)
                        totallist.Add(str);
                    }
                }
            };
            Regex rxnum = new Regex(@"^\d");
            Regex rxEng = new Regex(@"^[A-Z]");
            foreach(var i in totallist)
            {
                string s = i;
                while (s.Length < 10)
                    s += "-";
                if(s.Trim('-').Length == 0 || s.Trim('-').Length <3 && rxnum.IsMatch(s.Trim('-')) == true)
                    Menu.Text += s + "\r\t\t";
                else if(s.Trim('-').Length >6 && rxEng.IsMatch(s.Trim('-')) == true || s.Trim('-').Length >6 && rxnum.IsMatch(s.Trim('-')) == true && s.Trim('-').Contains(",")==false )
                    Menu.Text += s + "\r";
                else
                    Menu.Text += s + "\r\t";
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            Page.Text = (Convert.ToInt32(Page.Text) + 1).ToString();
            button1_Click(sender,  e);
        }

        private void back_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(Page.Text);
            if (page >= 2)
            {
                Page.Text = (Convert.ToInt32(Page.Text) - 1).ToString();
                button1_Click(sender, e);
            }
            else
            {
                MSG.Text = "Page can't < 1";
                check.Visible = true;
                MSG.Visible = true;
            }
                
        }

        private void check_Click(object sender, EventArgs e)
        {
            MSG.Visible = false;
            check.Visible = false;
        }
    }
}
