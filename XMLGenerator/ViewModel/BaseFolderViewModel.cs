﻿using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using IfcConfigManager.Assets;

namespace IfcConfigManager.ViewModel
{
    public class BaseFolderViewModel : ViewModelBase
    {
        private string m_toBasePath;
        private string m_fromBasePath;
        private ICommand m_fileDialogFromCommand;
        private ICommand m_fileDialogToCommand;
        private XmlViewModel m_xmlViewModel;
        public bool BasePathChanged;

        public BaseFolderViewModel(XmlViewModel xmlViewModel)
        {
            m_xmlViewModel = xmlViewModel;
            FileDialogToCommand = new DelegateCommand(FileDialogToExecute);
            FileDialogFromCommand = new DelegateCommand(FileDialogFromEcecute);
        }

        public ICommand FileDialogToCommand { get { return m_fileDialogToCommand; } set { m_fileDialogToCommand = value; OnPropertyChanged("FileDialogToCommand"); } }
        public ICommand FileDialogFromCommand { get { return m_fileDialogFromCommand; } set { m_fileDialogFromCommand = value; OnPropertyChanged("FileDialogFromCommand"); } }

        public string FromBasePath {
            get { return m_fromBasePath; }
            set {
                m_fromBasePath = value;
                BasePathChanged = true;
                if (Directory.Exists(FromBasePath) && Directory.Exists(ToBasePath))
                {
                    m_xmlViewModel.BasePathValid = true;
                }
                else
                {
                    m_xmlViewModel.BasePathValid = false;
                }
                OnPropertyChanged("FromBasePath");
            }
        }
        public string ToBasePath {
            get { return m_toBasePath; }
            set { m_toBasePath = value;
                                if (Directory.Exists(FromBasePath) && Directory.Exists(ToBasePath))
                {
                    m_xmlViewModel.BasePathValid = true;
                }
                else
                {
                    m_xmlViewModel.BasePathValid = false;
                }
                BasePathChanged = true;
                OnPropertyChanged("ToBasePath"); }
        }

        private void FileDialogFromEcecute()
        {
            FolderBrowserDialog p = new FolderBrowserDialog();
            if (Directory.Exists(FromBasePath))
            {
                p.SelectedPath = FromBasePath;
            }
            p.ShowDialog();
            FromBasePath = p.SelectedPath;
            BasePathHelper.SetFromBaseFolders(m_xmlViewModel, FromBasePath);
        }

        

        private void FileDialogToExecute()
        {
            FolderBrowserDialog p = new FolderBrowserDialog();
            if (Directory.Exists(ToBasePath))
            {
                p.SelectedPath = ToBasePath;
            }
            p.ShowDialog();
            ToBasePath = p.SelectedPath;          
        }
                
    }
}
