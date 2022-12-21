using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Reflection;

namespace Stock
{
    public partial class Stock : Form
    {
        public Stock()
        {
            List<Data> Tol = GetData.GetDataList();
            InitializeComponent();
            if (Tol.Count > 0)
            {
                TotDataGrid.DataSource = Tol;
                TotDataGrid.Visible = true;
                int i = 0;
                while(i<13)
                {
                    TotDataGrid.Columns[i].Width = 90;
                    i++;
                }
                //TotDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//調整寬度(標題+儲存格)
            }
            else
                MessageBox.Show("載入失敗，請檢查網路是否正常");
        }

        private void ReLoad_Click(object sender, EventArgs e)
        {
            List<Data> Tol = GetData.GetDataList();
            if (Tol.Count > 0)
                TotDataGrid.DataSource = Tol;
            else
                MessageBox.Show("重新整理失敗，請檢查網路是否正常");
        }

        private void InputBtn_Click(object sender, EventArgs e)
        {
            List<Data> Tmp = TotDataGrid.Rows
                .OfType<DataGridViewRow>()
                .Select(s => new Data
                {
                    Id = Convert.ToString(s.Cells[0].Value),
                    Name = Convert.ToString(s.Cells[1].Value),
                    Price = Convert.ToString(s.Cells[2].Value),
                    Amplitude = Convert.ToString(s.Cells[3].Value),
                    Quote = Convert.ToString(s.Cells[4].Value),
                    WeekQuote = Convert.ToString(s.Cells[5].Value),
                    Range = Convert.ToString(s.Cells[6].Value),
                    Open = Convert.ToString(s.Cells[7].Value),
                    MaxVal = Convert.ToString(s.Cells[8].Value),
                    MinVal = Convert.ToString(s.Cells[9].Value),
                    LastVal = Convert.ToString(s.Cells[10].Value),
                    Turnover = Convert.ToString(s.Cells[11].Value),
                    Value = Convert.ToString(s.Cells[12].Value)
                }).ToList();

            // 1.建立乾淨的活頁簿
            HSSFWorkbook _HSSFWorkbook = new();
            ISheet sheet = _HSSFWorkbook.CreateSheet("sheet"); //建立sheet
            sheet.CreateRow(0); //需先用CreateRow建立,才可通过GetRow取得該欄位

            PropertyInfo[] Props = typeof(Data).GetProperties(BindingFlags.Public | BindingFlags.Instance); //取得Property並建立表頭
            int con = 0;
            foreach (var i in Props)
            {
                sheet.GetRow(0).CreateCell(con).SetCellValue(Convert.ToString(i.CustomAttributes.First().ConstructorArguments.First().Value) ?? "");
                con++;
            }
            int rowIndex = 1;
            for (int row = 0; row < Tmp.Count; row++)
            {
                sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue(Tmp[row].Id);
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(Tmp[row].Name);
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(Tmp[row].Price);
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(Tmp[row].Amplitude);
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(Tmp[row].Quote);
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(Tmp[row].WeekQuote);
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(Tmp[row].Range);
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(Tmp[row].Open);
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(Tmp[row].MaxVal);
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(Tmp[row].MinVal);
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(Tmp[row].LastVal);
                sheet.GetRow(rowIndex).CreateCell(11).SetCellValue(Tmp[row].Turnover);
                sheet.GetRow(rowIndex).CreateCell(12).SetCellValue(Tmp[row].Value);
                rowIndex++;
            }
            var excelDatas = new MemoryStream();
            _HSSFWorkbook.Write(excelDatas);
            var ls_FileName = (DateTime.Now.ToString("yyyyMMddHHmmss")) + ".xls"; 
            string filePath = Application.StartupPath + ls_FileName;

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\tmp";
            string fPath = Path.Combine(desktop, ls_FileName);
            FileStream streamWriter = new(fPath, FileMode.Create);

            // 把活頁簿的資訊都寫進去
            _HSSFWorkbook.Write(streamWriter);
            streamWriter.Close();
            streamWriter.Dispose();
            MessageBox.Show($"匯出至{fPath}");
        }

        private void SearchText_TextChanged(object sender, EventArgs e)
        {
            int count = 0;
            if(SearchText.Text != "")
            {
                this.TotDataGrid.CurrentCell = null;
                foreach (DataGridViewRow row in this.TotDataGrid.Rows)
                {
                    for (int i = 0; i < 2; i++)
                    {
#pragma warning disable CS8602 // 可能 null 參考的取值 (dereference)。
                        if (row.Cells[i].Value.ToString().Contains(SearchText.Text))
                            count += 1;
                    }
                    if (count == 0)
                    {
                        row.Visible = false;
                    }
                    count = 0;
                }
            }
            else
            {
                for(int i = 0; i < this.TotDataGrid.Rows.Count; i++)
                {
                    this.TotDataGrid.Rows[i].Visible = true;
                }
            }
        }
    }
}