using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLGenerator.ViewModel;

namespace XMLGenerator.Assets
{
    public class CreateToField
    {
        private string baseToFolder;
        private string baseFromFolder;

        public XmlViewModel ToFieldGenerator(XmlViewModel _xmlViewModel)
        {
            foreach (var Discipline in _xmlViewModel.DisciplineViewModels)
            {
                foreach (var Export in Discipline.ExportViewModels)
                {
                    foreach (var FolderColl in Export.FolderViewModels)
                    {
                        foreach (var Folder in FolderColl.Folders)
                        {
                            Folder.To = ConvertFromTo(Folder.From);
                        }     
                              
                    }
                    foreach (var FileColl in Export.FileViewModels)
                    {
                        foreach (var File in FileColl.Files)
                        {
                            File.To = ConvertFromTo(File.To);
                        }
                    }
                }
                Discipline.StartFileViewModel.StartFile.Path = ConvertFromTo(Discipline.StartFileViewModel.StartFile.Path);
            }

            _xmlViewModel.IFCViewModel.IFC.To = ConvertFromTo(_xmlViewModel.IFCViewModel.IFC.To);

            return _xmlViewModel;
        }

        private string ConvertFromTo(string From)
        {           
            return baseToFolder + From.Substring(baseFromFolder.Length);
        }
    }


}
