using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galileo6;

namespace DataProcessing.Test
{
    internal class DataClass
    {
        public LinkedList<double>? SensorA { get; set; } //no longer static, IObserver of type double
        public LinkedList<double>? SensorB { get; set; }  //no longer static

        //Constructor, takes in mu and sig, runs LoadData method on startup
        public DataClass(double mu, double sig)
        {
            this.SensorA = new LinkedList<double>();
            this.SensorB = new LinkedList<double>();
            LoadData(mu, sig);
        }

        //LoadData Method
        public void LoadData(double mu, double sig)
        {
            ReadData dataReader = new ReadData();
            for (int i = 0; i < 400; i++)
            {
                SensorA.AddFirst(dataReader.SensorA(mu, sig));
                SensorB.AddFirst(dataReader.SensorB(mu, sig));
            }

        }
    }
}
