using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using XMLGenerator.ViewModel;

namespace XMLGenerator.Model
{
    public class Folder: ViewModelBase
    {
        private string m_from;
        private string m_ifc;
        private bool m_isVisible;

        private ICommand m_fileDialogFromCommand;
       

        public Folder()
        {
            FileDialogFromCommand = new DelegateCommand(FileDialogFromExecute);
            IsVisible = true;
        }

        private void FileDialogFromExecute()
        {
            FolderBrowserDialog p = new FolderBrowserDialog();
            p.ShowDialog();
            From = p.SelectedPath;
        }

        private void FileDialogIFCExecute()
        {
            FolderBrowserDialog p = new FolderBrowserDialog();
            p.ShowDialog();
            IFC = p.SelectedPath;
        }

        
        public bool IsVisible { get { return m_isVisible; } set { m_isVisible = value; OnPropertyChanged("IsVisible"); } }
        public ICommand FileDialogFromCommand { get { return m_fileDialogFromCommand; } set { m_fileDialogFromCommand = value; OnPropertyChanged("FileDialogFromCommand"); } }
      

        public string From { get { return m_from; } set { m_from = value; OnPropertyChanged("From"); } }
        public string To { get; set; }
        public string IFC { get { return m_ifc; } set { m_ifc = value; OnPropertyChanged("IFC"); } }
    }
}
