using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    class Square
    {
        private int fSize;

        public Square(int size)
        {
            fSize = size;
            //this.size = size;
        }

        public int Size
        {
            get
            {
                return fSize;
            }
            set
            {
                fSize = value;
            }
        }

        //public int getSize()
        //{
        //    return fSize;
        //}

        //public void setSize(int size)
        //{
        //    fSize = size;
        //}
    }
}
