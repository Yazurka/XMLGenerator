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
        public File()
        {
            FileDialogCommand = new DelegateCommand(setPath);
            IsVisible = true;
            RemoveCommand = new DelegateCommand(RemoveExecute);
        }

        private void setPath()
        {
            From = PathValidator.SelectPath(FromRestriction);

            //FolderBrowserDialog p = new FolderBrowserDialog();
            //p.ShowDialog();
            //var selectedPath = p.SelectedPath;
            //var isValid = PathValidator.ValidatePath(FromRestriction, selectedPath);
            //if (isValid)
            //{
            //    From = p.SelectedPath;
            //}
            //else
            //{
            //    var window = System.Windows.Application.Current.MainWindow as MetroWindow;
            //    MetroDialogSettings Settings = new MetroDialogSettings();
            //    Settings.AffirmativeButtonText = "Yes";
            //    Settings.NegativeButtonText = "No";
            //    var x = await window.ShowMessageAsync("Not allowed", "The selected path is invalid, do you want to try again?", MessageDialogStyle.AffirmativeAndNegative, Settings);

            //    switch (x)
            //    {
            //        case MessageDialogResult.Negative:
            //            return;
            //        case MessageDialogResult.Affirmative:
            //            setPath();
            //            break;
            //    }
            //}
        }

        private ICommand m_removeCommand;

        private void RemoveExecute()
        {
            IsVisible = false;
        }

        public ICommand RemoveCommand { get { return m_removeCommand; } set { m_removeCommand = value; OnPropertyChanged("RemoveCommand"); } }
        public string FromRestriction { get; set; }
        public bool IsVisible { get { return m_isVisible; } set { m_isVisible = value; OnPropertyChanged("IsVisible"); } }
        public ICommand FileDialogCommand { get { return m_fileDialogCommand; } set { m_fileDialogCommand = value; OnPropertyChanged("FileDialogCommand"); } }
        public string From { get { return m_from; } set { m_from = value; OnPropertyChanged("From"); } }
        public string To { get; set; }
    }
}
