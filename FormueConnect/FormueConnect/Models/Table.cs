using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormueConnect.Models
{
    public class Table
    {

        public string Name { get; set; }
        public Dictionary<string, string> Columns;

        public Table(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
