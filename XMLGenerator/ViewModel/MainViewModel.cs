using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using XMLGenerator.Assets;
using System.IO;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XMLGenerator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool m_isSettingsOpen;
        private ICommand m_openSettings;
        private ICommand m_generateXmlCommand;
        private ICommand m_fileExplorerCommand;
        private string m_savePath;
        private int m_selectedTabIndex;
        private ObservableCollection<ViewModelBase> m_viewModelBase;
        private ViewModelBase m_popup;
        private BaseMetroDialog CustomDialog;

        public ObservableCollection<ViewModelBase> CurrentViewModel { get { return m_viewModelBase; } set { m_viewModelBase = value; OnPropertyChanged("CurrentViewModel"); } }
        public ViewModelBase Popup { get { return m_popup; } set { m_popup = value; OnPropertyChanged("Popup"); } }
        public int SelectedTabIndex { get { return m_selectedTabIndex; } set { m_selectedTabIndex = value; OnPropertyChanged("SelectedTabIndex"); } }


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
           
            // CustomDialog = new CustomDialog();
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
            //CurrentViewModel = mapper.MapXMLToXmlViewModel(path);

            SavePath = path;
            IsSettingsOpen = false;
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
        private ObservableCollection<ViewModelBase> InitialSetupXmlViewModel()
        {
            var xmlVM = new XmlViewModel();
            xmlVM.ProjectName = "Project1";
            var xmlVM2 = new XmlViewModel();
            xmlVM2.ProjectName = "Project2";

            ObservableCollection<ViewModelBase> xmlVms = new ObservableCollection<ViewModelBase>();
            xmlVms.Add(xmlVM);
            xmlVms.Add(xmlVM2);
            return xmlVms;
        }

        private bool CanGenerate(XmlViewModel xmlViewModel)
        {
            return !(string.IsNullOrEmpty(xmlViewModel.BaseFolderViewModel.ToBasePath) ||
                   string.IsNullOrEmpty(xmlViewModel.BaseFolderViewModel.FromBasePath));
        }

        private void YesAnswer()
        {
            var c = CurrentViewModel[SelectedTabIndex] as XmlViewModel;
            var CTF = new CreateToField();
            c = CTF.ToFieldGenerator(c);
            var xmlo = new XMLObject(c);
            var res = xmlo.GetXML();


            var SaveDir = Path.GetDirectoryName(SavePath);
            Directory.CreateDirectory(SaveDir);
            res.Save(SavePath);
            MessageBox.Show("File saved to " + SavePath);
            Popup =null;
        }
        private void NoAnswer()
        {
            Popup = null;
            
        }

        private void GenerateXmlExecute()
        {
            //TODO: DO SHIT
            var c = CurrentViewModel[SelectedTabIndex] as XmlViewModel;
            var canMakeXML = CanGenerate(c);

            if (!canMakeXML)
            {
                return;
            }


            if (!Directory.Exists(Path.GetDirectoryName(SavePath)))
            {
                Popup = new YesNoDialogViewModel(YesAnswer, NoAnswer);

                    return;

            }

            YesAnswer();

        }
    }
}
