﻿using GemBox.Spreadsheet;
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
using System.Diagnostics;

namespace DecisionSupport
{
    public partial class ShowSolution : Form
    {
        List<Index> idxs = new List<Index>();
        Form1 form = new Form1();
        private List<Table> tables;
        string robotLimit;
        string operatorLimit;
        int allRobot = 0;
        int allWorker = 0;
        int firstSelect = 0;
        Stopwatch stopwatch = new Stopwatch();

        /* For matrix options */
        List<Index> indexList = new List<Index>();

        public ShowSolution(ref List<Table> tables, ref List<Index> idxList, string worker, string robot)
        {
            this.tables = tables;
            robotLimit = robot;
            operatorLimit = worker;
            indexList = idxList;
            InitializeComponent();
            this.Size = new System.Drawing.Size(770, 600);
            Console.WriteLine("w. " + this.Width + " h: " + this.Height);
            fillTable();
            fillChart();
            dataTable.CurrentCell = null;
            dataTable.ClearSelection();
            dataTable.FirstDisplayedCell = null;
        }
        double profitSum = 0;
        public void fillTable()
        {
            int cellIdx = 0;
            int rowIndex = dataTable.Rows.Add();
            int prodCount = 1;
            int keyIndex = 0;


            foreach (var i in indexList)
            {
                Table table = tables[i.Product];
                double profit = form.getU(i.Product, i.Robot, i.Worker);
                profitSum += profit;
                dataTable.Rows[rowIndex].Cells[cellIdx].Selected = false;
                dataTable.Rows[rowIndex].Selected = false;
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
            dataTable.Rows[rowIndex].Cells[++cellIdx].Value = profitSum;

            opNum.Text  = allWorker.ToString();
            robNum.Text = allRobot.ToString();
            robMax.Text = "/  " + robotLimit;
            opMax.Text  = "/  " + operatorLimit;
            dataTable.CurrentCell = null;
            dataTable.ClearSelection();
        }     

        private void ShowSolution_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(770, 600);
         //   maxRobot.KeyPress += limitKeyPress;
        }

        public void fillChart()
        {
            //chart1.Titles.Add("Resource utilization");
            chart1.BackColor = Color.FromArgb(40, Color.White);
            chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(70, Color.White);
            chart1.Series["Used"].LabelBackColor = Color.FromArgb(70, Color.White);
            chart1.Series["Surplus"].LabelBackColor = Color.FromArgb(70, Color.White);

            chart1.Series["Used"].Points.AddXY("Operator", allWorker);
            chart1.Series["Surplus"].Points.AddXY("Operator", Int32.Parse(operatorLimit) - allWorker);

            chart1.Series["Used"].Points.AddXY("Robot", allRobot);
            chart1.Series["Surplus"].Points.AddXY("Robot", Int32.Parse(robotLimit) - allRobot);
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
            int rowIndex = dataTable.Rows.Add();
            string filename;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Xlsx files|*.xlsx|Xls files|*.xls";
            DialogResult res = sfd.ShowDialog();
            filename = sfd.FileName;
            var regExp = @"^(?:[\w]\:|\\)(\\[\p{L}_\-\.\s0-9]+)+\.((xlsx)|(xls))$";
            Regex regex = new Regex(regExp);
            if (res == DialogResult.OK && regex.IsMatch(filename))
            {
                filename = sfd.FileName;
            }
            else if(res == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                MessageBoxIcon messageBoxIcon = MessageBoxIcon.Error;
                MessageBox.Show("Could not save file\nTry again!", "Saving failed", MessageBoxButtons.OK, messageBoxIcon);
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
                excelWorkSheet.Cells["B2"].Value = "Robots:";
                excelWorkSheet.Cells["B2"].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Right;
                excelWorkSheet.Cells["B3"].Value = "Operators:";
                excelWorkSheet.Cells["B3"].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Right;
                excelWorkSheet.Cells["C3"].Value = robotLimit;
                excelWorkSheet.Cells["C2"].Value = operatorLimit;
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
                string fromYellowLine;
                string toYellowLine;
                string all;
                int prodcnt = 1;
                int rowCnt = 6;
                foreach (var i in indexList) // 
                {
                    Console.WriteLine("forban");
                        Table table = tables[i.Product];
                        double profit = form.getU(i.Product, i.Robot, i.Worker);
                        Console.WriteLine("pr: " + profit);
                        excelWorkSheet.Cells["A" + rowCnt].Value = i.Product+1 + ".";
                        excelWorkSheet.Cells["B" + rowCnt].Value = i.Robot;
                        excelWorkSheet.Cells["C" + rowCnt].Value = i.Worker;
                        excelWorkSheet.Cells["D" + rowCnt].Value = profit;
                    //excelWorkSheet.Cells["D" + rowCnt].Style.Numberformat = (OfficeOpenXml.Style.ExcelNumberFormat)NumberFormatBuilder.Accounting(3, true);
                    //writeText.WriteLine(prodcnt + "." + ";" + i[j] + ";" + i[j + 1] + ";" + profit);
                    fromYellowLine = "A" + rowCnt.ToString();
                    toYellowLine = "D" + rowCnt.ToString();
                    all = fromYellowLine + ":" + toYellowLine;
                    excelWorkSheet.Cells[all].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Center;
                    all = fromYellowLine + ":" + toYellowLine;

                    prodcnt++;
                        rowCnt++;                    
                }
                excelWorkSheet.Cells["A" + rowCnt].Value = "Total";
                excelWorkSheet.Cells["B" + rowCnt].Value = allRobot;
                excelWorkSheet.Cells["C" + rowCnt].Value = allWorker;
                excelWorkSheet.Cells["D" + rowCnt].Value = profitSum;
                fromYellowLine = "A" + rowCnt.ToString();
                toYellowLine = "D" + rowCnt.ToString();
                all = fromYellowLine + ":" + toYellowLine;
                excelWorkSheet.Cells[all].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                excelWorkSheet.Cells[all].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                excelWorkSheet.Cells[all].Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)HorizontalAlignmentStyle.Center;

                excelWorkSheet.Column(1).AutoFit();
                excelWorkSheet.Column(2).AutoFit();
                excelWorkSheet.Column(3).AutoFit();
                excelWorkSheet.Column(4).AutoFit();
                excelWorkSheet.Column(5).AutoFit();

                excel.SaveAs(excelFile);
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
           
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            var results = chart1.HitTest(pos.X, pos.Y, false, System.Windows.Forms.DataVisualization.Charting.ChartElementType.DataPoint);
            foreach(var result in results)
            {
                if(result.ChartElementType == System.Windows.Forms.DataVisualization.Charting.ChartElementType.DataPoint)
                {
                    if (result.Series.Points[result.PointIndex].AxisLabel == "Operator")
                    {
                        if(result.Series.Name == "Used")
                        {
                            Console.WriteLine("Used worker");
                            toolTip1.SetToolTip(chart1, "Used: " + allWorker);
                        }
                        else if(result.Series.Name == "Surplus")
                        {
                            Console.WriteLine("Not neccessary worker");
                            toolTip1.SetToolTip(chart1, "Surplus: " + (Int32.Parse(operatorLimit) - allWorker));
                        }
                    }
                    else if(result.Series.Points[result.PointIndex].AxisLabel == "Robot")
                    {
                        if (result.Series.Name == "Used")
                        {
                            Console.WriteLine("Used rob");
                            toolTip1.SetToolTip(chart1, "Used: " + allRobot);
                        }
                        else if (result.Series.Name == "Surplus")
                        {
                            Console.WriteLine("Not neccessary rob");
                            toolTip1.SetToolTip(chart1, "Surplus: " + (Int32.Parse(robotLimit) - allRobot));
                        }
                    }
                }
            }
        }

        private void dataTable_SelectionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("changed to ");
            if(firstSelect < 3)
            {
                dataTable.CurrentCell = null;
                dataTable.ClearSelection();
                dataTable.FirstDisplayedCell = null;
            }
            firstSelect++;
        }
     
    }
}
