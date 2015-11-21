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
using XMLGenerator.Assets;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace XMLGenerator.Model
{
    public class File : ViewModelBase
    {
        private bool m_isVisible;
        private string m_from;
        private ICommand m_fileDialogCommand;
        private FileViewModel m_fileViewModel;

        public File(FileViewModel _fileViewModel)
        {
            m_fileViewModel = _fileViewModel;
            FileDialogCommand = new DelegateCommand(setPath);
            IsVisible = true;
            RemoveCommand = new DelegateCommand(RemoveExecute);
        }

        private async void setPath()
        {
            var x = await PathValidator.SelectMultipleFilePath(FromRestriction);
            if (x != null)
            {
                if (x.Count > 1)
                {
                    From = x.First();
                    for (int i = 1; i < x.Count; i++)
                    {
                        m_fileViewModel.AddMultipleFilesExecute(x[i]);
                    }
                }
                else
                {
                    From = x.First();
                }                
            }
        }

        private ICommand m_removeCommand;

        private void RemoveExecute()
        {
            IsVisible = false;
        }

        public ICommand RemoveCommand { get { return m_removeCommand; } set { m_removeCommand = value; OnPropertyChanged("RemoveCommand"); } }
        public string FromRestriction
        {
            get;
            set;
        }
        public bool IsVisible { get { return m_isVisible; } set { m_isVisible = value; OnPropertyChanged("IsVisible"); } }
        public ICommand FileDialogCommand { get { return m_fileDialogCommand; } set { m_fileDialogCommand = value; OnPropertyChanged("FileDialogCommand"); } }
        public string From { get { return m_from; } set { m_from = value; OnPropertyChanged("From"); } }
        public string To { get; set; }
    }
}
