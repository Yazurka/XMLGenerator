using System.Configuration;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using XMLGenerator.Assets;
using System.IO;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;

namespace XMLGenerator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool m_isSettingsOpen;
        private ICommand m_openSettings;
        private ICommand m_newProjectCommand;
        private ICommand m_generateXmlCommand;
        private ICommand m_fileExplorerCommand;
        private ICommand m_deleteProjectCommand;
        private string m_savePath;
        private string m_basePath;
        private int m_selectedTabIndex;
        private ObservableCollection<ViewModelBase> m_viewModelBase;

        public ObservableCollection<ViewModelBase> CurrentViewModel { get { return m_viewModelBase; } set { m_viewModelBase = value; OnPropertyChanged("CurrentViewModel"); } }
        public int SelectedTabIndex 
        { 
            get 
            { 
                return m_selectedTabIndex; 
            } 
            set {

                m_selectedTabIndex = value;
                OnPropertyChanged("SelectedTabIndex");

                if (m_selectedTabIndex != -1)
                {                    
                    SaveFolderPath = m_basePath;                    
                }
            }        
        }


        public ICommand OpenSettings
        {
            get { return m_openSettings; }
            set
            {
                m_openSettings = value;
                OnPropertyChanged("OpenSettings");
            }
        }
        public ICommand NewProjectCommand
        {
            get { return m_newProjectCommand; }
            set
            {
                m_newProjectCommand = value;
                OnPropertyChanged("NewProjectCommand");
            }
        }
        public ICommand DeleteProjectCommand
        {
            get { return m_deleteProjectCommand; }
            set
            {
                m_deleteProjectCommand = value;
                OnPropertyChanged("DeleteProjectCommand");
            }
        }
        public string SaveFolderPath
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
           
            m_basePath = ConfigurationManager.AppSettings["SavePath"];
            NewProjectCommand = new DelegateCommand(AddNewProject);
            DeleteProjectCommand = new DelegateCommand(DeleteProjectExecute);
            SelectedTabIndex = 0;
        }
        private async void DeleteProjectExecute ()
        {
            var window = Application.Current.MainWindow as MetroWindow;
            MetroDialogSettings Settings = new MetroDialogSettings();
            Settings.AffirmativeButtonText = "Yes";
            Settings.NegativeButtonText = "No";
            var x = await window.ShowMessageAsync("Delete", "Are you sure you want to delete the project?", MessageDialogStyle.AffirmativeAndNegative, Settings);

            switch (x)
            {
                case MessageDialogResult.Negative:
                    return;
                case MessageDialogResult.Affirmative:
                    CurrentViewModel.RemoveAt(SelectedTabIndex);
                    break;
            }
        }
        private void AddNewProject()
        {
            var newProject = new XmlViewModel{ProjectName = "New Project"};

            CurrentViewModel.Add(newProject);

            SelectedTabIndex = CurrentViewModel.Count - 1;
        }
        private void OpenExplorerExecute()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.ShowDialog();
            LoadFromXML(fileDialog.FileName);

        }

        private bool IsSelectedProjectEmpty(XmlViewModel p)
        {
            var EmptyBool = true;

            if (p.IFCViewModel.IFC.Export != string.Empty && p.IFCViewModel.IFC.Export != null)
            {
                EmptyBool = false;
            }

            if (p.IFCViewModel.IFC.From != string.Empty && p.IFCViewModel.IFC.From != null)
            {
                EmptyBool = false;
            }
            if (p.IFCViewModel.IFC.To != string.Empty && p.IFCViewModel.IFC.To != null)
            {
                EmptyBool = false;
            }

            if (p.BaseFolderViewModel.FromBasePath != string.Empty && p.BaseFolderViewModel.FromBasePath != null)
            {
                EmptyBool = false;
            }

            if (p.BaseFolderViewModel.ToBasePath != string.Empty && p.BaseFolderViewModel.ToBasePath != null)
            {
                EmptyBool = false;
            }

            foreach (var file in p.FileViewModel.Files)
            {
                if (file.To != string.Empty && file.To != null)
                {
                    EmptyBool = false;
                }

                if (file.From != string.Empty && file.From != null)
                {
                    EmptyBool = false;
                }
            }

            foreach (var Discipline in p.DisciplineViewModels)
            {
                if (Discipline.Value != string.Empty && Discipline.Value != null)
                {
                    EmptyBool = false;
                }

                if (Discipline.StartFileViewModel.StartFile.FromPath != string.Empty && Discipline.StartFileViewModel.StartFile.FromPath != null)
                {
                    EmptyBool = false;
                }

                foreach (var Export in Discipline.ExportViewModels)
                {
                    if (Export.Value != string.Empty && Export.Value != null)
                    {
                        EmptyBool = false;
                    }

                    foreach (var Folder in Export.FolderViewModel.Folders)
                    {
                        if (Folder.From != string.Empty && Folder.From != null)
                        {
                            EmptyBool = false;
                        }

                        if (Folder.IFC != string.Empty && Folder.IFC != null)
                        {
                            EmptyBool = false;
                        }

                        if (Folder.To != string.Empty && Folder.To != null)
                        {
                            EmptyBool = false;
                        }
                    }
                }
            }

            return EmptyBool;
        }

        private async void LoadFromXML(string path)
        {
            var TabIndex = SelectedTabIndex;
            var mapper = new XMLMapper();
            var p = mapper.MapXMLToXmlViewModel(path);

            if (CurrentViewModel.Count == 0)
            {
                CurrentViewModel.Add(new XmlViewModel());
                SelectedTabIndex = 0;
                TabIndex = 0;
            }

            if (IsSelectedProjectEmpty(CurrentViewModel[SelectedTabIndex] as XmlViewModel))
            {
                CurrentViewModel[SelectedTabIndex] = p;
            }
            else
            {
                // Project has data
                var window = Application.Current.MainWindow as MetroWindow;
                MetroDialogSettings Settings = new MetroDialogSettings();
                Settings.AffirmativeButtonText = "Overwrite";
                Settings.NegativeButtonText = "New page";
                Settings.FirstAuxiliaryButtonText = "Abort";
                var x = await window.ShowMessageAsync("Oops!", "This project already contains data, what do you want to do?", MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, Settings);
                switch (x)
                {
                    case MessageDialogResult.Negative:
                        // Ny fane
                        CurrentViewModel.Add(p);
                        TabIndex = CurrentViewModel.Count - 1;
                        break;
                    case MessageDialogResult.Affirmative:
                        // Overskrivs
                        CurrentViewModel[SelectedTabIndex] = p;
                        break;
                    case MessageDialogResult.FirstAuxiliary:
                        return;
                }
            }

            SaveFolderPath = Path.GetDirectoryName(path);
            IsSettingsOpen = false;
            SelectedTabIndex = TabIndex;

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
           // var xmlVM2 = new XmlViewModel();
           // xmlVM2.ProjectName = "Project2";

            ObservableCollection<ViewModelBase> xmlVms = new ObservableCollection<ViewModelBase>();
            xmlVms.Add(xmlVM);
           // xmlVms.Add(xmlVM2);
            return xmlVms;
        }

        private bool CanGenerate(XmlViewModel xmlViewModel)
        {
            return !(string.IsNullOrEmpty(xmlViewModel.BaseFolderViewModel.ToBasePath) ||
                   string.IsNullOrEmpty(xmlViewModel.BaseFolderViewModel.FromBasePath));
        }

        private void CreateNewDirectory()
        {
            var SaveDir = Path.GetDirectoryName(SaveFolderPath);
            Directory.CreateDirectory(SaveDir);
        }

        private async void SaveXMLFile()
        {

            var c = CurrentViewModel[SelectedTabIndex] as XmlViewModel;
            var CTF = new CreateToField();
            c = CTF.ToFieldGenerator(c);
            var xmlo = new XMLObject(c);
            var res = xmlo.GetXML();
            SaveFolderPath = Path.GetDirectoryName(SaveFolderPath) + "\\" + c.ProjectName + ".xml";
            res.Save(SaveFolderPath);
            var window = Application.Current.MainWindow as MetroWindow;
            await window.ShowMessageAsync("File saved", "Your file has been saved to: \n" + SaveFolderPath);
        }

        private async void GenerateXmlExecute()
        {
            var c = CurrentViewModel[SelectedTabIndex] as XmlViewModel;
            var canMakeXML = CanGenerate(c);

            if (!canMakeXML)
            {
                var window = Application.Current.MainWindow as MetroWindow;
                await window.ShowMessageAsync("Invalid data", "Base folder's fields To and From can not be empty, please enter a value");
                return;
            }
            

            if (!Directory.Exists(Path.GetDirectoryName(SaveFolderPath)))
            {
                var window = Application.Current.MainWindow as MetroWindow;
                var x = await window.ShowMessageAsync("Directory not found!", "The directory you specified does not exist, do you want to create it?", MessageDialogStyle.AffirmativeAndNegative);

                switch (x)
                {
                    case MessageDialogResult.Negative:
                        return;
                    case MessageDialogResult.Affirmative:
                        CreateNewDirectory();
                        SaveXMLFile();
                        break;
                }


                return;

            }

            SaveXMLFile();

        }
    }
}
