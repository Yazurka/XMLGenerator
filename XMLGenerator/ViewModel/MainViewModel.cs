using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using XMLGenerator.Assets;

namespace XMLGenerator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool m_isSettingsOpen;
        private ICommand m_openSettings;
        private ICommand m_generateXmlCommand;
        private string m_savePath;
        public ViewModelBase CurrentViewModel { get; set; }
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
           
            CurrentViewModel = InitialSetupXmlViewModel();
            SavePath = ConfigurationManager.AppSettings["SavePath"];
        }

      
        public ICommand GenerateXmlCommand
        {
            get { return m_generateXmlCommand; }
            set { m_generateXmlCommand = value; OnPropertyChanged("GenerateXmlCommand"); }
        }
        private XmlViewModel InitialSetupXmlViewModel()
        {
            var xmlVM = new XmlViewModel(this);
            
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
