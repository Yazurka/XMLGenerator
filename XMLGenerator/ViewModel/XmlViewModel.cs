using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace XMLGenerator.ViewModel
{
    public class XmlViewModel: ViewModelBase
    {
        private ObservableCollection<DisciplineViewModel> m_disciplineViewModels;
        private IFCViewModel m_ifcViewModel;
        private BaseFolderViewModel m_baseFolderViewModel;
        private FileViewModel m_fileViewModel;
        private ICommand m_addDiciplineCommand;
        private string m_projectName;
       
        public XmlViewModel()
        {
            DisciplineViewModels = new ObservableCollection<DisciplineViewModel> {new DisciplineViewModel() };
            BaseFolderViewModel = new BaseFolderViewModel();
            IFCViewModel = new IFCViewModel();
            FileViewModel = new FileViewModel();
            AddDiciplineCommand = new DelegateCommand(AddDiciplineExecute);
            
        }

        private void AddDiciplineExecute()
        {
            DisciplineViewModels.Add(new DisciplineViewModel());
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

    }
}
