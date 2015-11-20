using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLGenerator.Model
{
    public class Export
    {
        public string Value { get; set; }
        public ObservableCollection<Folder> Folders { get; set; }
        public ObservableCollection<File> Files { get; set; }
    }
}
