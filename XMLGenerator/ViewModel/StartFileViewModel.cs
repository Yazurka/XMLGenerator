using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using XMLGenerator.Model;

namespace XMLGenerator.ViewModel
{
    public class StartFileViewModel : ViewModelBase
    {
        private StartFile m_startFile;
       
        public StartFileViewModel()
        {
           StartFile = new StartFile();
        }
        public StartFile StartFile
        {
            get { return m_startFile; }
            set
            {
                m_startFile = value; OnPropertyChanged("StartFile");
            }
        }
      
       
    }
}

