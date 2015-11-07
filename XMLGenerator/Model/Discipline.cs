using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLGenerator.Model
{
    public class Discipline
    {
        public string Value { get; set; }
        public ObservableCollection<Export> Exports { get; set; }
        public StartFile StartFile { get; set; }
    }
}
