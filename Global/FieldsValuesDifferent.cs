using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    class FieldsValuesDifferent:Exception
    {
        public FieldsValuesDifferent()
        {
            throw new Exception("Fields count is different than values one");
        }
        public FieldsValuesDifferent(string message)
        {
            throw new Exception(message);
        }
    }
}
