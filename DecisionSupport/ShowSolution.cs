using GemBox.Spreadsheet;
using OfficeOpenXml;
using Microsoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LicenseContext = System.ComponentModel.LicenseContext;


namespace DecisionSupport
{
    public partial class ShowSolution : Form
    {
        List<Index> indexes = new List<Index>();
        double optimum;
        private List<Table> tables;
        int allRobot = 0;
        int allWorker = 0;
        int prodCount = 1;
        int calculate = 0;
        public ShowSolution(ref List<Table> tables)
        {

            this.tables = tables;
            InitializeComponent();
            this.Size = new System.Drawing.Size(770, 600);
            Console.WriteLine("w. " + this.Width + " h: " + this.Height);
            
        }

        public void fillTable()
        {
            int cellIdx = 0;
            int rowIndex = dataTable.Rows.Add();
            int prodCount = 1;
            int keyIndex = 0;
            int allRobot = 0;
            int allWorker = 0;
            Form1 f = new Form1();
            foreach (var i in indexes) // 3,1,6,2,1,1, = i
            {
                    Console.WriteLine("forban");
                    Table table = tables[i.Product];
                    double profit = f.getU(i.Product, i.Robot, i.Worker);
                    Console.WriteLine("pr: " + profit);
                    dataTable.Rows[rowIndex].Cells[cellIdx].Value = i.Product+1;
                    dataTable.Rows[rowIndex].Cells[++cellIdx].Value = i.Robot;
                    dataTable.Rows[rowIndex].Cells[++cellIdx].Value = i.Worker;
                    dataTable.Rows[rowIndex].Cells[++cellIdx].Value = profit;
                    allRobot += i.Robot;
                    allWorker += i.Worker;

                    rowIndex = dataTable.Rows.Add();
                    prodCount++;
                    cellIdx = 0;
                    keyIndex++; 
            }
            dataTable.Rows[rowIndex].HeaderCell.Value = "Total";
            dataTable.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            dataTable.Rows[rowIndex].Cells[cellIdx].Value = prodCount - 1;
            dataTable.Rows[rowIndex].Cells[++cellIdx].Value = allRobot;
            dataTable.Rows[rowIndex].Cells[++cellIdx].Value = allWorker;
            dataTable.Rows[rowIndex].Cells[++cellIdx].Value = optimum;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ShowSolution_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(770, 600);
            Console.WriteLine("0 w. " + this.Width + " h: " + this.Height);
            maxRobot.KeyPress += limitKeyPress;
        }
        public void limitKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            TextBox t = sender as TextBox;

            //Delete the placeholder 0
            if (t.Text == "0" && char.IsDigit(e.KeyChar))
            {
                t.Text = "";
            }

            // not allow decimal point / comma
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.Handled = true;
                //Console.WriteLine("handledtrue");
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if(dataTable.RowCount == 0)
            {
                return;
            }
            int cellIdx = 0;
            int rowIndex = dataTable.Rows.Add();
            int keyIndex = 0;
            Form1 f = new Form1();
            string filename;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Xlsx files|*.xlsx|Xls files|*.xls";
            DialogResult res = sfd.ShowDialog();
            filename = sfd.FileName;
            var regExp = @"^(?:[\w]\:|\\)(\\[a-zA-Z_\-\s0-9]+)+\.(xlsx)$";
            Regex regex = new Regex(regExp);
            if (res == DialogResult.OK && regex.IsMatch(filename))
            {
                filename = sfd.FileName;
            }
            else
            {
                MessageBox.Show("Could not save file\nTry again!", "Saving failed");
                return;
            }
            using (ExcelPackage excel = new ExcelPackage())
            {
                //var xlApp = new OfficeOpenXml.FormulaParsing.Excel.Application();
                excel.Workbook.Worksheets.Add("Worksheet1");
                FileInfo excelFile = new FileInfo(filename);
                OfficeOpenXml.ExcelWorksheet excelWorkSheet = excel.Workbook.Worksheets["Worksheet1"];

                var style = new CellStyle();
                style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                style.VerticalAlignment = VerticalAlignmentStyle.Center;
                excelWorkSheet.Cells["A1"].Value = "Date:";
                excelWorkSheet.Cells["B1"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                excelWorkSheet.Column(2).Width = 20;
                excelWorkSheet.Cells["A2"].Value = "Available sources:";
                excelWorkSheet.Column(1).AutoFit();
                excelWorkSheet.Column(2).AutoFit();
                excelWorkSheet.Column(3).AutoFit();
                excelWorkSheet.Column(4).AutoFit();
                excelWorkSheet.Cells["B2"].Value = "Robots:";
                excelWorkSheet.Cells["B2"].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Right;
                excelWorkSheet.Cells["B3"].Value = "Operators:";
                excelWorkSheet.Cells["B3"].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Right;
                excelWorkSheet.Cells["C3"].Value = 4;
                excelWorkSheet.Cells["C2"].Value = 10;
                excelWorkSheet.Cells["A5"].Value = "Product";
                excelWorkSheet.Cells["B5"].Value = "Number of robots";
                excelWorkSheet.Cells["C5"].Value = "Number of operators";
                excelWorkSheet.Cells["D5"].Value = "Profit";

                var mergedRange = excelWorkSheet.Cells["A4:D4"];
                mergedRange.Merge = true;
                mergedRange.Value = "Allocation by products";
                mergedRange.Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Center;

                for(int i = 1; i < 10; i++)
                {
                    excelWorkSheet.Cells["A" + i].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Center;
                    excelWorkSheet.Cells["B" + i].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Center;
                    excelWorkSheet.Cells["C" + i].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Center;
                    excelWorkSheet.Cells["D" + i].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Center;
                }

                //writeText.WriteLine("Available sources:;" + "robots;" + 10);
                //writeText.WriteLine(";operators;" + 4);
                //writeText.Write("All products: " + ";" + prodCount + "\n");
                //writeText.WriteLine("Allocation by products:");
                //writeText.Write("Product" + ";" + "Number of robots" + ";" + "Number of operators" + ";" + "Profit\n");
                int prodcnt = 1;
                int rowCnt = 6;
                foreach (var i in indexes) // 
                {
                    Console.WriteLine("forban");
                        Table table = tables[i.Product];
                        double profit = f.getU(i.Product, i.Robot, i.Worker);
                        Console.WriteLine("pr: " + profit);
                        excelWorkSheet.Cells["A" + rowCnt].Value = i.Product+1 + ".";
                        excelWorkSheet.Cells["B" + rowCnt].Value = i.Robot;
                        excelWorkSheet.Cells["C" + rowCnt].Value = i.Worker;
                        excelWorkSheet.Cells["D" + rowCnt].Value = profit;
                        //excelWorkSheet.Cells["D" + rowCnt].Style.Numberformat = (OfficeOpenXml.Style.ExcelNumberFormat)NumberFormatBuilder.Accounting(3, true);
                        //writeText.WriteLine(prodcnt + "." + ";" + i[j] + ";" + i[j + 1] + ";" + profit);
                        prodcnt++;
                        rowCnt++;                    
                }
                excelWorkSheet.Cells["C" + rowCnt].Value = "Total";
                excelWorkSheet.Cells["D" + rowCnt].Value = optimum;
                //writeText.WriteLine(";" + ";" + "Total;" + values.Values.First());

                excel.SaveAs(excelFile);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            int robotLimit = Int32.Parse(maxRobot.Text);
            int operatorLimit = Int32.Parse(maxOperator.Text);
            Form1 form = new Form1();
            if (calculate != 0)
            {
                dataTable.Rows.Clear();
            }
            if(maxRobot.Text.Length == 0 || maxOperator.Text.Length == 0)
            {
                MessageBox.Show("Please give me the maximum limits!", "Missing number.");
            }
            else
            {
                optimum = form.getPrevOptVal(tables.Count - 1, robotLimit, operatorLimit, ref indexes);
                fillTable();
                calculate = 1;
            }
            Console.WriteLine("OPT: " + optimum);
            Console.WriteLine("cachből: " + form.ReadCache);
            Console.WriteLine("cache " + form.Cache.Count);
        }
    }
}
