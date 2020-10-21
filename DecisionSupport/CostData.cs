using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionSupport
{
    public class TableData
    { 
        int numOfRobot, numOfWorker, value;


        public TableData(int a, int b, int c)
        {
            NumOfRobot = a;
            NumOfWorker = b;
            Value = c;

        }

        public int NumOfRobot { get => numOfRobot; set => numOfRobot = value; }
        public int NumOfWorker { get => numOfWorker; set => numOfWorker = value; }
        public int Value { get => value; set => this.value = value; }

    }

    public class CostData
    {
        double robotCost;
        double workerCost;
        double productValue;

        List<List<TableData>> values;
        public CostData(double robotCost, double workerCost, double productValue)
        {
            this.RobotCost = robotCost;
            this.WorkerCost = workerCost;
            this.ProductValue = productValue;
            
            values = new List<List<TableData>>();
        }

        public double WorkerCost { get => workerCost; set => workerCost = value; }
        public double RobotCost { get => robotCost; set => robotCost = value; }
        public double ProductValue { get => productValue; set => productValue = value; }

        public void addToRow(int row, int numOfRobot, int numOfWorker, int value)
        {
            if (values.Count == row)
            {
                values.Add(new List<TableData>());
            }

            values[row].Add(new TableData(numOfRobot, numOfWorker, value));
        }

        public int getNumOfRobot(int i, int j)
        {
            return values[i][j].NumOfRobot;
        }

        public int getNumOfWorker(int i, int j)
        {
            return values[i][j].NumOfWorker;
        }

        public int getValue(int i, int j)
        {
            return values[i][j].Value;
        }

        public int getRowCount()
        {
            return values.Count;
        }

        public int getColumnCount()
        {
            if (values.Count == 0)
            {
                return 0;
            }

            return values[0].Count;
        }
    }
}
