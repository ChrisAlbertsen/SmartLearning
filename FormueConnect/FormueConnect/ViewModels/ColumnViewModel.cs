using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormueConnect.ViewModels
{
    public class ColumnViewModel : ViewModelBase
    {
        public List<Models.Column> Columns { get; set; }
        public Models.Table table { get; set; }

        public ColumnViewModel(Models.Table tableTemp)
        {
            table = tableTemp;
            Columns = Connection.ConstructColumns(table.Name);
            Views.ColumnWindow columnWindow = new(this);
            columnWindow.Show();
        }
        
        public void WriteClickHandler()
        {
            Connection.WriteColumns(Columns, table.Name);
        }
    }
}
