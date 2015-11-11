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
        private bool m_isVisible;
        private string m_from;
        private ICommand m_fileDialogCommand;
        public File()
        {
            FileDialogCommand = new DelegateCommand(setPath);
        }

        private void setPath()
        {
            FolderBrowserDialog p = new FolderBrowserDialog();
            p.ShowDialog();
            From = p.SelectedPath;
        }
        
        public bool IsVisible { get { return m_isVisible; } set { m_isVisible = value; OnPropertyChanged("IsVisible"); } }
        public ICommand FileDialogCommand { get { return m_fileDialogCommand; } set { m_fileDialogCommand = value; OnPropertyChanged("FileDialogCommand"); } }
        public string From { get { return m_from; } set { m_from = value; OnPropertyChanged("From"); } }
        public string To { get; set; }
    }
}
