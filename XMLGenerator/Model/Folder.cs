﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using XMLGenerator.Assets;
using XMLGenerator.ViewModel;


namespace XMLGenerator.Model
{
    public class Folder: ViewModelBase
    {
        private string m_from;
        private string m_ifc;
        private bool m_isVisible;
        private ICommand m_removeCommand;

        private ICommand m_fileDialogFromCommand;
       

        public Folder()
        {
            FileDialogFromCommand = new DelegateCommand(FileDialogFromExecute);
            IsVisible = true;
            RemoveCommand = new DelegateCommand(RemoveExecute);
        }

        private async void FileDialogFromExecute()
        {
            FolderBrowserDialog p = new FolderBrowserDialog();
            p.ShowDialog();
            var selectedPath = p.SelectedPath;
            var isValid = PathValidator.ValidatePath(FromRestriction, selectedPath);
            
            if (isValid)
            {
                From = p.SelectedPath;
            }
            else
            {
                var window = System.Windows.Application.Current.MainWindow as MetroWindow;
                MetroDialogSettings Settings = new MetroDialogSettings();
                Settings.AffirmativeButtonText = "Yes";
                Settings.NegativeButtonText = "No";
                var x = await window.ShowMessageAsync("Not allowed", "The selected path is invalid, do you want to try again?", MessageDialogStyle.AffirmativeAndNegative, Settings);

                switch (x)
                {
                    case MessageDialogResult.Negative:
                        return;
                    case MessageDialogResult.Affirmative:
                        FileDialogFromExecute();
                        break;
                }

            }
        }

        private void FileDialogIFCExecute()
        {
            FolderBrowserDialog p = new FolderBrowserDialog();
            p.ShowDialog();
            IFC = p.SelectedPath;
            
        }
       
        private void RemoveExecute()
        {
            IsVisible = false;
        }

        public string FromRestriction { get; set; }

        public ICommand RemoveCommand { get { return m_removeCommand; } set { m_removeCommand = value; OnPropertyChanged("RemoveCommand"); } }
        public bool IsVisible { get { return m_isVisible; } set { m_isVisible = value; OnPropertyChanged("IsVisible"); } }
        public ICommand FileDialogFromCommand { get { return m_fileDialogFromCommand; } set { m_fileDialogFromCommand = value; OnPropertyChanged("FileDialogFromCommand"); } }
      

        public string From { get { return m_from; } set { m_from = value; OnPropertyChanged("From"); } }
        public string To { get; set; }
        public string IFC { get { return m_ifc; } set { m_ifc = value; OnPropertyChanged("IFC"); } }
    }
}
