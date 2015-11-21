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
using System.Collections.Generic;

namespace XMLGenerator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool m_isSettingsOpen;
        private ICommand m_openSettings;
        private ICommand m_newProjectCommand;
        private ICommand m_saveCommand;
        private ICommand m_fileExplorerCommand;
        private ICommand m_deleteProjectCommand;
        private ICommand m_openFolderCommand;
        private ICommand m_generateLocalFoldersCommand;
        private int m_selectedTabIndex;
        private ObservableCollection<ViewModelBase> m_viewModelBase;

        public ObservableCollection<ViewModelBase> CurrentViewModel { get { return m_viewModelBase; } set { m_viewModelBase = value; OnPropertyChanged("CurrentViewModel"); } }

        public int SelectedTabIndex
        {
            get
            {
                return m_selectedTabIndex;
            }
            set
            {
                if (value == -1 && CurrentViewModel.Count > 0)
                {
                    return;
                }
                m_selectedTabIndex = value;
                OnPropertyChanged("SelectedTabIndex");
            }
        }

        public ICommand SaveFolderCommand { get; set; }

        public ICommand GenerateLocalFoldersCommand
        {
            get { return m_generateLocalFoldersCommand; }
            set
            {
                m_generateLocalFoldersCommand = value;
                OnPropertyChanged("GenerateLocalFoldersCommand");
            }
        }


        public ICommand OpenFolderCommand
        {
            get { return m_openFolderCommand; }
            set
            {
                m_openFolderCommand = value;
                OnPropertyChanged("OpenFolderCommand");
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
            GenerateLocalFoldersCommand = new DelegateCommand(GenerateLocalFolders);
            m_openSettings = new DelegateCommand(flip);
            m_saveCommand = new DelegateCommand(saveFunction);
            FileExplorerCommand = new DelegateCommand(OpenExplorerExecute);
            SaveFolderCommand = new DelegateCommand(SaveAs);
            CurrentViewModel = InitialSetupXmlViewModel();
            NewProjectCommand = new DelegateCommand(AddNewProject);
            DeleteProjectCommand = new DelegateCommand(DeleteProjectExecute);
            SelectedTabIndex = 0;
        }

        private void saveFunction()
        {
            Save(false);
        }

        private async void GenerateLocalFolders()
        {
            XmlViewModel xmlViewModel = CurrentViewModel[SelectedTabIndex] as XmlViewModel;

            var window = Application.Current.MainWindow as MetroWindow;
            if (xmlViewModel.BaseFolderViewModel.BasePathChanged)
            {
                await window.ShowMessageAsync("Warning" ,"You need to save your changes before you can generate local folders.");
                return;
            }
            if (string.IsNullOrEmpty(xmlViewModel.BaseFolderViewModel.FromBasePath))
            {
                await window.ShowMessageAsync("Warning", "Unable to create directory, base folder from field is empty.");
                return;
            }

            var IfcList = new List<string>();
            foreach (var Discipline in xmlViewModel.DisciplineViewModels)
            {
                foreach (var Export in Discipline.ExportViewModels)
                {
                    foreach (var Folder in Export.FolderViewModel.Folders)
                    {
                        if (Folder.To == null)
                        {
                            break;
                        }
                        Directory.CreateDirectory(Folder.To);
                    }
                    if (IfcList.Find(x => x == Export.IFC) == null)
                    {
                        IfcList.Add(Export.IFC);
                    }
                }
            }

            if (xmlViewModel.IFCViewModel.IFC.To != null)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(xmlViewModel.IFCViewModel.IFC.To));
            }


            foreach (var ifc in IfcList)
            {
                var file = new FileInfo(xmlViewModel.IFCViewModel.IFC.From);
                file.CopyTo(Path.GetDirectoryName(xmlViewModel.IFCViewModel.IFC.To) + "\\" + ifc + ".ifc");
            }
            await window.ShowMessageAsync("Directory created", "Export directory has been created at: \n" + xmlViewModel.BaseFolderViewModel.ToBasePath);
        }


        private async void DeleteProjectExecute()
        {
            var window = Application.Current.MainWindow as MetroWindow;
            MetroDialogSettings Settings = new MetroDialogSettings();
            Settings.AffirmativeButtonText = "Yes";
            Settings.NegativeButtonText = "No";
            var x = await window.ShowMessageAsync("Close", "Are you sure you want to close the project?\nWarning: Unsaved data will be lost.", MessageDialogStyle.AffirmativeAndNegative, Settings);

            switch (x)
            {
                case MessageDialogResult.Negative:
                    return;
                case MessageDialogResult.Affirmative:
                    CurrentViewModel.RemoveAt(SelectedTabIndex);
                    SelectedTabIndex = SelectedTabIndex - 1;
                    break;
            }
        }

        private async void SaveAs()
        {
            var c = CurrentViewModel[SelectedTabIndex] as XmlViewModel;

            var canMakeXML = CanGenerate(c);

            if (!canMakeXML)
            {
                var window = Application.Current.MainWindow as MetroWindow;
                await window.ShowMessageAsync("Invalid data", "Base folder's fields To and From can not be empty, please enter a value");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = c.SavePath;
            saveDialog.AddExtension = true;
            saveDialog.Filter = "XML Files | *.xml"; ;
            saveDialog.ShowDialog();

            if (saveDialog.FileName == null)
            {
                return;
            }
            var Tab = SelectedTabIndex;

            c.SavePath = saveDialog.FileName;
            CurrentViewModel[SelectedTabIndex] = c;
            SelectedTabIndex = Tab;
            Save(true);
        }

        private void AddNewProject()
        {
            var newProject = new XmlViewModel { ProjectName = "New Project" };

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

                    if (Export.IFC != string.Empty && Export.IFC != null)
                    {
                        EmptyBool = false;
                    }

                    foreach (var Folder in Export.FolderViewModel.Folders)
                    {
                        if (Folder.From != string.Empty && Folder.From != null)
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
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            var TabIndex = SelectedTabIndex;
            var mapper = new XMLMapper();

            if (CurrentViewModel.Count == 0)
            {
                CurrentViewModel.Add(new XmlViewModel());
                SelectedTabIndex = 0;
                TabIndex = 0;
                CurrentViewModel[TabIndex] = mapper.MapXMLToXmlViewModel(path, CurrentViewModel[TabIndex] as XmlViewModel);
            }
            else
            {
                var p = mapper.MapXMLToXmlViewModel(path, CurrentViewModel[TabIndex] as XmlViewModel);

                if (IsSelectedProjectEmpty(CurrentViewModel[TabIndex] as XmlViewModel))
                {
                    CurrentViewModel[TabIndex] = p;
                }
                else
                {
                    // Project has data
                    var window = Application.Current.MainWindow as MetroWindow;
                    MetroDialogSettings Settings = new MetroDialogSettings();
                    Settings.AffirmativeButtonText = "Open in this tab";
                    Settings.NegativeButtonText = "New tab";
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
                            CurrentViewModel[TabIndex] = p;
                            break;
                        case MessageDialogResult.FirstAuxiliary:
                            return;
                    }
                    
                }
                SelectedTabIndex = TabIndex;
            }


            var xmlViewModel = CurrentViewModel[TabIndex] as XmlViewModel;
            BasePathHelper.SetFromBaseFolders(xmlViewModel, xmlViewModel.BaseFolderViewModel.FromBasePath);
            CurrentViewModel[TabIndex] = xmlViewModel;
            IsSettingsOpen = false;
            SelectedTabIndex = TabIndex;

        }


        public ICommand FileExplorerCommand
        {
            get { return m_fileExplorerCommand; }
            set { m_fileExplorerCommand = value; OnPropertyChanged("FileExplorerCommand"); }
        }
        public ICommand SaveCommand
        {
            get { return m_saveCommand; }
            set { m_saveCommand = value; OnPropertyChanged("SaveCommand"); }
        }
        private ObservableCollection<ViewModelBase> InitialSetupXmlViewModel()
        {
            var xmlVM = new XmlViewModel();
            xmlVM.ProjectName = "Project1";

            ObservableCollection<ViewModelBase> xmlVms = new ObservableCollection<ViewModelBase>();
            xmlVms.Add(xmlVM);
            return xmlVms;
        }

        private bool CanGenerate(XmlViewModel xmlViewModel)
        {
            return !(string.IsNullOrEmpty(xmlViewModel.BaseFolderViewModel.ToBasePath) ||
                   string.IsNullOrEmpty(xmlViewModel.BaseFolderViewModel.FromBasePath));
        }

        private void CreateNewDirectory(string path)
        {
            var SaveDir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(SaveDir);
        }

        private async void SaveXMLFile(bool SaveAsBool)
        {
            var c = CurrentViewModel[SelectedTabIndex] as XmlViewModel;

            var CTF = new CreateToField();
            c = CTF.ToFieldGenerator(c);
            var xmlo = new XMLObject(c);
            var res = xmlo.GetXML();
            res.Save(c.SavePath);
            var window = Application.Current.MainWindow as MetroWindow;
            if (SaveAsBool)
            {
                await window.ShowMessageAsync("File saved", "Your file has been saved to: \n" + c.SavePath);
            }
         

            c.BaseFolderViewModel.BasePathChanged = false;
        }

        private async void Save(bool SaveAsBool)
        {

            var c = CurrentViewModel[SelectedTabIndex] as XmlViewModel;

            var canMakeXML = CanGenerate(c);

            if (!canMakeXML)
            {
                var window = Application.Current.MainWindow as MetroWindow;
                await window.ShowMessageAsync("Invalid data", "Base folder's fields To and From can not be empty, please enter a value");
                return;
            }

            if (string.IsNullOrEmpty(c.SavePath))
            {
                SaveAs();
                return;
            }

            if (string.IsNullOrEmpty(c.SavePath))
            {
                return;
            }


            if (!Directory.Exists(Path.GetDirectoryName(c.SavePath)))
            {
                var window = Application.Current.MainWindow as MetroWindow;
                var x = await window.ShowMessageAsync("Directory not found!", "The directory you specified does not exist, do you want to create it?", MessageDialogStyle.AffirmativeAndNegative);

                switch (x)
                {
                    case MessageDialogResult.Negative:
                        c.SavePath = "";
                        return;
                    case MessageDialogResult.Affirmative:
                        CreateNewDirectory(c.SavePath);
                        SaveXMLFile(SaveAsBool);
                        break;
                }


                return;

            }

            SaveXMLFile(SaveAsBool);

        }
    }
}
