using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
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
            List<string> list2 = new List<string>();
            List<string> totli = new List<string>();
            List<string> totli2 = new List<string>();
            List<string> sli = new List<string>();
            HtmlWeb web = new HtmlWeb();

            string[] title = { "代號▼-----\t\r", "名稱▼-----\t\r", "價格▼-----\t\r", "漲跌▼-----\t\r", "漲跌幅▼-----\t\r", "周漲跌▼-----\t\r", "振幅▼-----\t\r", "開盤▼-----\t\r", "最高▼-----\t\r", "最低▼-----\t\r", "昨收▼-----\t\r", "成交量▼-----\t\r", "成交值(億)▼-----" };
            string[] stringSeparators = new string[] { "\r\n" };
            string[] stringSeparators2 = new string[] { "</td><td>" };
            string startline = "</td><td>";
            string endline = "</td>";
            string[] remove = new String[] { "最新資訊", "盤後交易量", "台股排行－全部資訊", "全部資訊", "現股當沖", "台股排行榜－價格", "顯示全部", "代號▼名稱▼價格▼漲跌▼漲跌幅▼周漲跌▼振幅▼開盤▼最高▼最低▼昨收▼成交量▼成交值(億)▼" };
            string up = "上一頁";
            string down = "下一頁";
            string[] lines = new string[] { };
            string[] lines2 = new string[] { };

            Menu.Text = "";


            HtmlAgilityPack.HtmlDocument doc = web.Load("https://histock.tw/stock/rank.aspx?&p=" + page + "&d=1");
            HtmlNodeCollection data = doc.DocumentNode.SelectNodes("html/body/form/div[4]/div[5]");
            foreach (var i in data)
            {
                if (i.InnerText.Trim() != "")
                {
                    lines = i.InnerText.Split(stringSeparators, StringSplitOptions.None);
                    lines2 = i.InnerHtml.Split(stringSeparators, StringSplitOptions.None);

                    foreach (var j in lines)
                    {
                        if (j.Trim() != "" && remove.Contains(j.Trim()) == false)
                        {
                            if (j.Trim().Contains(down) == false && j.Trim().Contains(up) == false)
                                list.Add(j.Trim());
                        }
                    }
                    foreach (var j in lines2)
                    {
                        if (j.Trim().StartsWith(startline) && j.Trim().Length > 10 && j.EndsWith(endline))
                        {
                            list2.Add(j.Trim());
                        }
                    }
                }
            }
            int con = 0;
            foreach (var i in list)
            {
                string tmp = i;

                switch (con % 10)
                {
                    default:
                        while (tmp.Length <= 11)
                        {
                            tmp += "-";
                        }
                        break;
                    case 6:
                        if (tmp.IndexOf('%') > -1)
                        {
                            lines = new string[] { };

                            lines = tmp.Split('%');
                            lines[0] += "%";
                            while (lines[0].Length <= 10)
                            {
                                lines[0] += "-";
                            }
                            while (lines[1].Length <= 10)
                            {
                                lines[1] += "-";
                            }
                        }
                        break;
                }
                if (lines.Length > 0 && lines.Length < 10)
                {
                    foreach (var ii in lines)
                    {
                        totli.Add(ii + "\t\r");
                    }
                    lines = new string[] { };
                }
                else
                {
                    if (tmp.Substring(0, 3) == "---" && tmp.EndsWith("-") && tmp.TrimEnd('-').Length>=2)
                    {
                        totli.Add(tmp + "\t\t\r");
                    }
                    else if(tmp.TrimEnd('-').Length>6 && tmp.Contains("▼") != true && tmp.Contains("▲") != true && tmp.Contains("%") !=true && tmp.EndsWith("+") !=true)
                    {
                        if(tmp.TrimEnd('-').Length >= 8)
                            totli.Add(tmp.TrimEnd('-') + "     \r");
                        else
                        totli.Add(tmp.TrimEnd('-') + "\t\r");

                    }
                    else
                    totli.Add(tmp + "\t\r");
                }
                con++;
            }
            lines2 = new string[] { };
            foreach (var i in list2)
            {
                lines2 = i.Split(stringSeparators2, StringSplitOptions.None);
                lines2[3] = lines2[3].Replace("</td>", "");
                con = 0;
                foreach (var j in lines2)
                {
                    string tmp = j;
                    while (tmp.Length <= 11)
                    {
                        tmp += "-";
                    }
                    lines2[con] = tmp + "\t\r";

                    con++;
                }
                totli2.AddRange(lines2);
                lines2 = new string[] { };
            }
            con = 0;
            int num = 1;
            foreach (var i in totli2)
            {
                if (con < 1)
                    con++;
                else if (con >= 1 && con <= 3)
                {
                    sli.Add(i);
                    con++;
                }
                if (con == 4)
                {
                    totli.InsertRange(10 * num + (4 * (num - 1)), sli);
                    sli.RemoveRange(0, 3);
                    num++;
                    con = 0;
                }
            }

            con = 1;

            while (totli.Count >= 13 * con - 1)
            {
                totli.RemoveAt(13 * con);
                con++;
            }

            con = 1;
            foreach (var i in totli)
            {
                if (con % 13 == 0)
                {
                    Menu.Text += i + "\n";
                }
                else
                {
                    Menu.Text += i;
                }
                con++;
            }
            list.Clear();
            list2.Clear();
            totli.Clear();
            totli2.Clear();
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
