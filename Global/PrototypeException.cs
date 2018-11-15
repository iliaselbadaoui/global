using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    class PrototypeException:Exception
    {
        public PrototypeException()
        {
            throw new Exception("Violation of prototype");
        }
        public PrototypeException(string message)
        {
            throw new Exception(message);
        }
    }
}
