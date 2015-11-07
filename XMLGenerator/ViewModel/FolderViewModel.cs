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
     public class FolderViewModel : ViewModelBase
     {
        private ICommand m_addFolderCommand;
        private ObservableCollection<Folder> m_folders;

         public FolderViewModel()
         {
             AddFolderCommand = new DelegateCommand(AddFolderExecute);
            
             
             Folders = new ObservableCollection<Folder> {new Folder()};
             
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
        public ObservableCollection<Folder> Folders
        {
            get { return m_folders; }
            set { m_folders = value; OnPropertyChanged("Folders"); }
        }
        private void AddFolderExecute()
        {
            Folders.Add(new Folder());
        }
    }
}
