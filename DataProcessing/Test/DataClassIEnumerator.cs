using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataProcessing.Test
{
    internal class DataClassIEnumerator : IEnumerator<DataClass>
    {
        private bool disposedValue;

        //public DataClass Current => throw new NotImplementedException();
        public DataClass Current { get; set; }  //privateset?

        public int countA { get; set; }  //Used to count current current index
        public int countB { get; set; }
        public int currentIndexA { get; set; }
        public int currentIndexB { get; set; }
        public double currentSensorAValue { get; set; } //not sure if necessary
        public double currentSensorBValue { get; set; } //not sure if necessary

        public bool aORb = true;    //TEMPORARY

        //object IEnumerator.Current => throw new NotImplementedException();  //In description has get Current
        object IEnumerator.Current { get
            {
                //return Current; 
                //old method

                //if (aORb)
                //{
                //    return Current.SensorA.ElementAt(currentIndexA);
                //}
                //else
                //{
                //    return Current.SensorB.ElementAt(currentIndexB);
                //}
                //return new string[2]{ Current.SensorA.ElementAt(currentIndexA).ToString(), Current.SensorA.ElementAt(currentIndexB).ToString()};
                return new TestRow(Current.SensorA.ElementAt(currentIndexA), Current.SensorA.ElementAt(currentIndexB));
            }
        }   //Currently set to return current dataclass



        //Constructor, Instantiate new Enumerator
        public DataClassIEnumerator(DataClass a)
        {
            Current = a;
            countA = 0;
            countB = 0;
            currentIndexA = 0;
            currentIndexB = 0;
        }

        //true if the enumerator successfully moved to the next element, false if enumerator passed to the end of the collection
        public bool MoveNext()
        {
            if (currentIndexA < Current.SensorA.Count() - 1)//Differentiate between previous item in collection and next item in collection
            {
                aORb = true;    //temp
                currentIndexA = countA;
                countA++;
                return true;
            }
            else if (currentIndexB < Current.SensorB.Count() - 1)
            {
                aORb = false;   //TEMPORARY
                currentIndexB = countB;
                countB++;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Reset position, necessary for interface
        public void Reset()
        {
            countA = 0;
            currentIndexA= 0;
            currentIndexB= 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DataClassIEnumerator()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    //A test row class for binding purposes
    public class TestRow
    {
        string a;
        string b;
        public TestRow(double a, double b)
        {
            this.a = a.ToString();
            this.b = b.ToString();
        }

        //public override string ToString()
        //{
        //    return "testString";
        //}
    }

}
