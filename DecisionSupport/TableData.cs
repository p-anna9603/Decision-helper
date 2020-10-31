using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionSupport
{
    public class TableData
    {
        double robotCost;
        double workerCost;
        double productValue;

        List<List<int>> values;
        public TableData()
        {
            values = new List<List<int>>();
        }

        public double WorkerCost { get => workerCost; set => workerCost = value; }
        public double RobotCost { get => robotCost; set => robotCost = value; }
        public double ProductValue { get => productValue; set => productValue = value; }

        public void addToRow(int row, string value)
        {
            Console.WriteLine("row: " + row + ", values: " + value);
            int valInt = -1;
            try
            {
                valInt = Int32.Parse(value);
            }
            catch (Exception e) { 
            }

            if (values.Count == row)
            {
                values.Add(new List<int>());
            }

            values[row].Add(valInt);
        }

        public int get(int i, int j)
        {
            return values[i][j];
        }

        public int getRowCount()
        {
            return values.Count;
        }

        public int getColumnCount()
        {
            if (values.Count == 0)
            {
                //Console.WriteLine("getColumnCount()-ban");
                return 0;
            }
            //Console.WriteLine("values[0].COunt: " + values[0].Count);
            return values[values.Count - 1].Count;
        }
    }
}
