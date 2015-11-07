using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace XMLGenerator.ViewModel
{
    public class XmlViewModel: ViewModelBase
    {
        private MainViewModel m_mainViewModel;
        private FileViewModel m_fileViewModel;
        private ObservableCollection<DisciplineViewModel> m_disciplineViewModels;
        private FolderViewModel m_folderViewModel;
        private StartFileViewModel m_startFileViewModel;
        private IFCViewModel m_ifcViewModel;
        private BaseFolderViewModel m_baseFolderViewModel;
        private ICommand m_addDiciplineCommand;

       
        public XmlViewModel()
        {
            DisciplineViewModels = new ObservableCollection<DisciplineViewModel> {new DisciplineViewModel() };
            BaseFolderViewModel = new BaseFolderViewModel();
            IFCViewModel = new IFCViewModel();
            AddDiciplineCommand = new DelegateCommand(AddDiciplineExecute);
            
        }

        private void AddDiciplineExecute()
        {
            DisciplineViewModels.Add(new DisciplineViewModel());
        }
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
