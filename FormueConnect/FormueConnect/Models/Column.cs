using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormueConnect.Models
{
    public class Column
    {
        public string Name { get; set; }
        public string DataType { get; set; }

        public string UserInput { get; set; } = "";

        public Column(string name, string dataType)
        {
            Name = name;
            DataType = dataType;
        }
    }
}
