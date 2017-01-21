using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2D.Controler
{
    class FactoryInvalidArgumentsException: Exception
    {
        public FactoryInvalidArgumentsException():
            base("This kind of object is not supported by the Factory")
        {
        }
    }
}
