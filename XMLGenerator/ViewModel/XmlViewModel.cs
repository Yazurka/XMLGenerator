using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using IfcConfigManager.Assets;

namespace IfcConfigManager.ViewModel
{
    public class XmlViewModel: ViewModelBase
    {
        private ObservableCollection<DisciplineViewModel> m_disciplineViewModels;
        private IFCViewModel m_ifcViewModel;
        private BaseFolderViewModel m_baseFolderViewModel;
        private FileViewModel m_fileViewModel;
        private ICommand m_addDiciplineCommand;
        private ICommand m_renameProjectCommand;
        private string m_projectName;
        public string SavePath;
        private bool m_basePathValid;
       
        public XmlViewModel()
        {
            BaseFolderViewModel = new BaseFolderViewModel(this);
            DisciplineViewModels = new ObservableCollection<DisciplineViewModel> {new DisciplineViewModel(this) };
          
            IFCViewModel = new IFCViewModel(this);
            FileViewModel = new FileViewModel(this);
            AddDiciplineCommand = new DelegateCommand(AddDiciplineExecute);
            RenameProjectCommand = new DelegateCommand(RenameProject);
            
        }

        public bool BasePathValid
        {
            get { return m_basePathValid; }
            set { m_basePathValid = value;
                FileViewModel.BasePathValid = value;
                IFCViewModel.BasePathValid = value;
                BasePathHelper.SetFromBaseFolders(this, BaseFolderViewModel.FromBasePath);
                OnPropertyChanged("BasePathValid"); }
        }

        private async void RenameProject()
        {
            var window = Application.Current.MainWindow as MetroWindow;
            var newName = await window.ShowInputAsync("Edit project name", "Project name");

            if (string.IsNullOrEmpty(newName))
            {
                return;
            }

            ProjectName = newName;
        }
        private void AddDiciplineExecute()
        {
            DisciplineViewModels.Add(new DisciplineViewModel(this));
        }
        public string ProjectName { get { return m_projectName; } set { m_projectName = value; OnPropertyChanged("ProjectName"); } }
        public FileViewModel FileViewModel { get { return m_fileViewModel; } set { m_fileViewModel = value; OnPropertyChanged("FileViewModel"); } }
        public BaseFolderViewModel BaseFolderViewModel { get { return m_baseFolderViewModel; } set { m_baseFolderViewModel = value; OnPropertyChanged("BaseFolderViewModel"); } }
        public IFCViewModel IFCViewModel { get { return m_ifcViewModel; } set { m_ifcViewModel = value; OnPropertyChanged("IFCViewModel"); } }
        public ObservableCollection<DisciplineViewModel>  DisciplineViewModels
        {
            get { return m_disciplineViewModels; }
            set { m_disciplineViewModels = value; OnPropertyChanged("DisciplineViewModels"); }
        }
        public ICommand AddDiciplineCommand
        {
            get { return m_addDiciplineCommand; }
            set
            {
                m_addDiciplineCommand = value;
                OnPropertyChanged("AddDiciplineCommand");
            }
        }
        public ICommand RenameProjectCommand
        {
            get { return m_renameProjectCommand; }
            set
            {
                m_renameProjectCommand = value;
                OnPropertyChanged("RenameProjectCommand");
            }
        }

    }
}
