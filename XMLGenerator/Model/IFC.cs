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

        public IFC()
        {
            FileDialogFromCommand = new DelegateCommand(setPath);
        }

        private async void setPath()
        {
            var x = await PathValidator.SelectFilePath(FromRestriction);
            if (x != "")
            {
                From = x;
            }
        }

        public ICommand FileDialogFromCommand { get { return m_fileDialogFromCommand; } set { m_fileDialogFromCommand = value; OnPropertyChanged("FileDialogFromCommand"); } }

        public string FromRestriction { get; set; }
        public string From { get { return m_from; } set { m_from = value; OnPropertyChanged("From"); } }
        public string To { get; set; }
        public string Export { get; set; }
    }
}
