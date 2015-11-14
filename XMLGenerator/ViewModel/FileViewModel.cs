using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using XMLGenerator.Model;

namespace XMLGenerator.ViewModel
{
    public class FileViewModel : ViewModelBase
    {
        private ObservableCollection<File> m_files;
        private ICommand m_addFolderCommand;
        private XmlViewModel m_xmlViewModel;
        public FileViewModel(XmlViewModel xmlViewModel)
        {
            m_xmlViewModel = xmlViewModel;
            AddFolderCommand = new DelegateCommand(AddFolderExecute);
            Files = new ObservableCollection<File> {new File{ FromRestriction = m_xmlViewModel.BaseFolderViewModel.FromBasePath}};
        }
        public ObservableCollection<File> Files { get { return m_files; } set
        {
            m_files = value;
            OnPropertyChanged("Files");}
        }

        public ICommand AddFolderCommand
        {
            get { return m_addFolderCommand; }
            set
            {
                m_addFolderCommand = value;
                OnPropertyChanged("AddFolderCommand");
            }
        }

        private void AddFolderExecute()
        {
            Files.Add(new File { FromRestriction = m_xmlViewModel.BaseFolderViewModel.FromBasePath});
        }
    }
}
