﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using XMLGenerator.Assets;

namespace XMLGenerator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool m_isSettingsOpen;
        private ICommand m_openSettings;
        private ICommand m_generateXmlCommand;
        private ICommand m_fileExplorerCommand;
        private string m_savePath;
        private ViewModelBase m_viewModelBase;
        
        public ViewModelBase CurrentViewModel { get { return m_viewModelBase; } set { m_viewModelBase = value; OnPropertyChanged("CurrentViewModel"); } }


        public ICommand OpenSettings
        {
            get { return m_openSettings; }
            set
            {
                m_openSettings = value;
                OnPropertyChanged("OpenSettings");
            }
        }
        public string SavePath
        {
            get { return m_savePath; }
            set
            {
                m_savePath = value;
                OnPropertyChanged("SavePath");
            }
        }
        

        public bool IsSettingsOpen
        {
            get { return m_isSettingsOpen; }
            set
            {
                m_isSettingsOpen = value;
                OnPropertyChanged("IsSettingsOpen");
            }
        }

        private void flip()
        {
            IsSettingsOpen = !IsSettingsOpen;
        }
        public MainViewModel()
        {
            m_openSettings = new DelegateCommand(flip);
            m_generateXmlCommand = new DelegateCommand(GenerateXmlExecute);
            FileExplorerCommand = new DelegateCommand(OpenExplorerExecute);
            CurrentViewModel = InitialSetupXmlViewModel();
            SavePath = ConfigurationManager.AppSettings["SavePath"];
        }

        private void OpenExplorerExecute()
        {
            OpenFileDialog fileDialog  = new OpenFileDialog();
            
            fileDialog.ShowDialog();
            LoadFromXML(fileDialog.FileName);

        }

        private void LoadFromXML(string path)
        {
            var mapper = new XMLMapper();
            CurrentViewModel = mapper.MapXMLToXmlViewModel(path);
        }
        public ICommand FileExplorerCommand
        {
            get { return m_fileExplorerCommand; }
            set { m_fileExplorerCommand = value; OnPropertyChanged("FileExplorerCommand"); }
        }
        public ICommand GenerateXmlCommand
        {
            get { return m_generateXmlCommand; }
            set { m_generateXmlCommand = value; OnPropertyChanged("GenerateXmlCommand"); }
        }
        private XmlViewModel InitialSetupXmlViewModel()
        {
            var xmlVM = new XmlViewModel();
            
            return xmlVM;
        }

        private bool CanGenerate(XmlViewModel xmlViewModel)
        {
            return !(string.IsNullOrEmpty(xmlViewModel.BaseFolderViewModel.ToBasePath) ||
                   string.IsNullOrEmpty(xmlViewModel.BaseFolderViewModel.FromBasePath));
        }
        private void GenerateXmlExecute()
        {
            //TODO: DO SHIT
            var c = CurrentViewModel as XmlViewModel;
            var canMakeXML = CanGenerate(c);
            if (!canMakeXML)
            {
               return;
            }
            var CTF = new CreateToField();
            CTF.ToFieldGenerator(c);
            var xmlo = new XMLObject(c.IFCViewModel,c.DisciplineViewModels);
            var res = xmlo.GetXML();

        }
    }
}
