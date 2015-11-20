using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLGenerator.ViewModel;

namespace XMLGenerator.Assets
{
    public class CreateToField
    {
        private string baseToFolder = "";
        private string baseFromFolder = "";

        public XmlViewModel ToFieldGenerator(XmlViewModel _xmlViewModel)
        {
            baseToFolder = _xmlViewModel.BaseFolderViewModel.ToBasePath;
            baseFromFolder = _xmlViewModel.BaseFolderViewModel.FromBasePath;

            foreach (var Discipline in _xmlViewModel.DisciplineViewModels)
            {
                foreach (var Export in Discipline.ExportViewModels)
                {
                    foreach (var Folder in Export.FolderViewModel.Folders)
                    {
                        if (string.IsNullOrEmpty(Folder.From))
                        {
                            Folder.From = string.Empty;
                        }
                        Folder.To = ConvertFromTo(Folder.From);
                    }

                    var FoldersToRemove = Export.FolderViewModel.Folders.Where(x => x.IsVisible == false).ToList();

                    foreach (var folder in FoldersToRemove)
                    {
                        Export.FolderViewModel.Folders.Remove(folder);
                    }

                    if (string.IsNullOrEmpty(Export.IFC))
                    {
                        Export.IFC = string.Empty;
                    }

                    if (string.IsNullOrEmpty(Export.Value))
                    {
                        Export.Value = string.Empty;
                    }
                }
                if (string.IsNullOrEmpty(Discipline.Value))
                {
                    Discipline.Value = string.Empty;
                }

                if (string.IsNullOrEmpty(Discipline.StartFileViewModel.StartFile.FromPath))
                {
                    Discipline.StartFileViewModel.StartFile.FromPath = string.Empty;
                }
                                
                Discipline.StartFileViewModel.StartFile.ToPath = ConvertFromTo(Discipline.StartFileViewModel.StartFile.FromPath);

                var ExportsToRemove = Discipline.ExportViewModels.Where(x => x.IsVisible == false).ToList();

                foreach (var export in ExportsToRemove)
                {
                    Discipline.ExportViewModels.Remove(export);
                }

            }

            if (string.IsNullOrEmpty(_xmlViewModel.IFCViewModel.IFC.Export))
            {
                _xmlViewModel.IFCViewModel.IFC.Export = string.Empty;
            }
            if (string.IsNullOrEmpty(_xmlViewModel.IFCViewModel.IFC.From))
            {
                _xmlViewModel.IFCViewModel.IFC.From = string.Empty;
            }

            foreach (var File in _xmlViewModel.FileViewModel.Files)
            {
                if (string.IsNullOrEmpty(File.From))
                {
                    File.From = string.Empty;
                }


                File.To = ConvertFromTo(File.From);
            }

            var FilesToRemove = _xmlViewModel.FileViewModel.Files.Where(x => x.IsVisible == false).ToList();

            foreach (var export in FilesToRemove)
            {
                _xmlViewModel.FileViewModel.Files.Remove(export);
            }

            var DisciplineToRemove = _xmlViewModel.DisciplineViewModels.Where(x => x.IsVisible == false).ToList();

            foreach (var export in DisciplineToRemove)
            {
                _xmlViewModel.DisciplineViewModels.Remove(export);
            }

            _xmlViewModel.IFCViewModel.IFC.To = ConvertFromTo(_xmlViewModel.IFCViewModel.IFC.From);

            return _xmlViewModel;
        }

        private string ConvertFromTo(string From)
        {
            if (!string.IsNullOrEmpty(From) && !string.IsNullOrEmpty(baseFromFolder))
            {
                return baseToFolder + From.Substring(baseFromFolder.Length);
            }

            return string.Empty;


        }
    }


}
