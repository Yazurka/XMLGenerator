using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using XMLGenerator.ViewModel;
using XMLGenerator.Assets;

namespace XMLGenerator.Model
{
    public class IFC : ViewModelBase
    {
        private string m_from;
        private ICommand m_fileDialogFromCommand;
        private ICommand m_fileDialogExportCommand;

        public IFC()
        {
            FileDialogFromCommand = new DelegateCommand(setFromPath);
            FileDialogExportCommand = new DelegateCommand(setExportPath);
        }

        private async void setFromPath()
        {
            var x = await PathValidator.SelectFilePath(FromRestriction);
            if (!string.IsNullOrEmpty(x))
            {
                From = x;
            }
        }

        private async void setExportPath()
        {
            var x = await PathValidator.SelectFilePath(FromRestriction);
            if (!string.IsNullOrEmpty(x))
            {
                Export = x;
            }
        }

        public ICommand FileDialogFromCommand { get { return m_fileDialogFromCommand; } set { m_fileDialogFromCommand = value; OnPropertyChanged("FileDialogFromCommand"); } }
        public ICommand FileDialogExportCommand { get { return m_fileDialogExportCommand; } set { m_fileDialogExportCommand = value; OnPropertyChanged("FileDialogToCommand"); } }

        public string FromRestriction { get; set; }
        public string From { get { return m_from; } set { m_from = value; OnPropertyChanged("From"); } }
        public string To { get; set; }
        public string Export { get; set; }
    }
}
