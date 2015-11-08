using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using XMLGenerator.ViewModel;

namespace XMLGenerator.Model
{
    public class File : ViewModelBase
    {
        private string m_from;
        public File()
        {
            FolderDialogCommand = new DelegateCommand(setPath);
        }

        private void setPath()
        {
            FolderBrowserDialog p = new FolderBrowserDialog();
            p.ShowDialog();
            From = p.SelectedPath;
        }
        public ICommand FolderDialogCommand { get; set; }
        public string From { get { return m_from; } set { m_from = value; OnPropertyChanged("From"); } }
        public string To { get; set; }
    }
}
