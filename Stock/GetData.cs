using HtmlAgilityPack;
using System.ComponentModel;

namespace Stock
{
    public class Data
    {
        [DisplayName("編號")]
        public string? Id { get; set; }

        [DisplayName("名稱")]
        public string? Name { get; set; }

        [DisplayName("價格")]
        public string? Price { get; set; }

        [DisplayName("漲跌")]
        public string? Amplitude { get; set; }

        [DisplayName("漲跌幅")]
        public string? Quote { get; set; }

        [DisplayName("周漲跌")]
        public string? WeekQuote { get; set; }

        [DisplayName("振幅")]
        public string? Range { get; set; }

        [DisplayName("開盤")]
        public string? Open { get; set; }

        [DisplayName("最高")]
        public string? MaxVal { get; set; }

        [DisplayName("最低")]
        public string? MinVal { get; set; }

        [DisplayName("昨收")]
        public string? LastVal { get; set; }

        [DisplayName("成交量")]
        public string? Turnover { get; set; }

        [DisplayName("成交值(億)")]
        public string? Value { get; set; }
    }

    public static class GetData
    {
        public static List<Data> GetDataList()
        {
            HtmlWeb web = new();
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://histock.tw/stock/rank.aspx?p=all");
            HtmlNodeCollection data = doc.DocumentNode.SelectNodes("html/body/form/div[4]/div[5]");
            List<string> list = new();
            List<string> tmp = new();
            List<Data> Tol = new();
            string[] stringSeparators = new string[] { "\r\n" };
            foreach (var i in data)
            {
                if (i.InnerText.Trim() != "")
                {
                    String[] lines = i.InnerHtml.Split(stringSeparators, StringSplitOptions.None);

                    foreach (var j in lines)
                    {
                        if (j.Trim().EndsWith("</td><td>") && j.Trim().Length > 15 || j.Trim().Contains("_blank") && j.Trim().EndsWith("</a>") || !j.Trim().StartsWith("<div") && j.Trim().Contains("CPHB") && j.Trim().EndsWith("</span>") || j.Trim().StartsWith("</td><td>") && j.Trim().EndsWith("</td>") && j.Trim().Length > 15)
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
                    tmp.Add(str);
                }
                else if (i.StartsWith("<a"))
                {
                    str = i.Substring(i.IndexOf(">") + 1).Replace("</a>", "");
                    tmp.Add(str);
                }
                else if (i.StartsWith("<span"))
                {
                    str = i.Substring(i.IndexOf(">") + 1).Replace("</span>", "");
                    tmp.Add(str);
                }
                else if (i.StartsWith("</td>"))
                {
                    str = i.Replace("</td>", "").Replace("<td>", "@");
                    String[] lastthreeitem = str.Split('@');
                    foreach (var j in lastthreeitem)
                    {
                        str = j.Trim(',');
                        if (str.Length > 0)
                            tmp.Add(str);
                    }
                }
            };
            int ii = 0;
            while (ii < tmp.Count)
            {
                Tol.Add(new Data
                {
                    Id = tmp[ii++],
                    Name = tmp[ii++],
                    Price = tmp[ii++],
                    Amplitude = tmp[ii++],
                    Quote = tmp[ii++],
                    WeekQuote = tmp[ii++],
                    Range = tmp[ii++],
                    Open = tmp[ii++],
                    MaxVal = tmp[ii++],
                    MinVal = tmp[ii++],
                    LastVal = tmp[ii++],
                    Turnover = tmp[ii++],
                    Value = tmp[ii++],
                });
            }
            return Tol;
        }
    }
}
