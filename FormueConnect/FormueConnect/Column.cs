using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormueConnect
{
    public class Column
    {
        public String Name { get; set; }
        public String DataType { get; set; }

        public String UserInput { get; set; } = "";

        public Column(String name, String dataType)
        {
            Name = name;
            DataType = dataType;
        }
    }
}
