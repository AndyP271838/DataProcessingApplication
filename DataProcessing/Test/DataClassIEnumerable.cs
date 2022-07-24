using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Test
{
    internal class DataClassIEnumerable : IEnumerable<DataClass>
    {
        public DataClass test;  //in docs is _filepath, not sure if just example or must be string
        public DataClassIEnumerable(DataClass test)   //in description uses parameter to set local variable
        {
            this.test = test;
        }

        public IEnumerator<DataClass> GetEnumerator()
        {
            return new DataClassIEnumerator(test);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
