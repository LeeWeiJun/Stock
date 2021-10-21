using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;


namespace FormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.moneydj.com/funddj/yb/YP302000.djhtm?a=ET001001");
                HtmlNodeCollection data = doc.DocumentNode.SelectNodes("/html/body/div[2]/div/div[4]/form/div[2]/table/tbody/tr");

                int i = 1;
                foreach (var k in data)
                {
                    while (i <= 11)
                    {

                        if (i == 2)
                        {
                            if(k.SelectSingleNode("td[" + i + "]").InnerText.Length > 10 && k.SelectSingleNode("td[" + i + "]").InnerText.Contains("2000")==false || k.SelectSingleNode("td[" + i + "]").InnerText.Length > 15)
                            {
                                textBox1.Text += k.SelectSingleNode("td[" + i + "]").InnerText.PadRight(28, ' ') + "\r";

                            }
                            else
                            textBox1.Text += k.SelectSingleNode("td[" + i + "]").InnerText.PadRight(30, ' ') +"\t";
                        }
                        else if (i == 3)
                        {
                            textBox1.Text +=  "-\r" + k.SelectSingleNode("td[" + i + "]").InnerText.PadRight(8, '　') + "  " + "\t";
                        }
                        else
                        {
                            textBox1.Text += k.SelectSingleNode("td[" + i + "]").InnerText + "  " + "\t";
                        }
                        i++;
                    }
                    textBox1.Text += "\r\n";
                    i = 1;
                }
            }
            catch
            {
                textBox1.Text = textBox1.Text;
            }
        }
    }
}
