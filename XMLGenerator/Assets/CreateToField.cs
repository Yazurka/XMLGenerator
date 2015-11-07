//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using XMLGenerator.ViewModel;

//namespace XMLGenerator.Assets
//{
//    public class CreateToField
//    {
//        private string baseToFolder = "";
//        private string baseFromFolder = "";

//        public XmlViewModel ToFieldGenerator(XmlViewModel _xmlViewModel)
//        {
//            baseToFolder = _xmlViewModel.BaseFolderViewModel.ToBasePath;
//            baseFromFolder = _xmlViewModel.BaseFolderViewModel.FromBasePath;

//            foreach (var Discipline in _xmlViewModel.DisciplineViewModels)
//            {
//                foreach (var Export in Discipline.ExportViewModels)
//                {
//                    //foreach (var FolderColl in Export.FolderViewModels)
//                    //{
//                    //    foreach (var Folder in FolderColl.Folders)
//                    //    {
//                    //        if (string.IsNullOrEmpty(Folder.IFC))
//                    //        {
//                    //            Folder.IFC = string.Empty;
//                    //        }
//                    //        if (string.IsNullOrEmpty(Folder.From))
//                    //        {
//                    //            Folder.From = string.Empty;
//                    //        }
//                    //        Folder.To = ConvertFromTo(Folder.From);
//                    //    }     
                              
//                    }
//                    //foreach (var FileColl in Export.FileViewModels)
//                    //{
//                    //    foreach (var File in FileColl.Files)
//                    //    {
//                    //        if (string.IsNullOrEmpty(File.From))
//                    //        {
//                    //            File.From = string.Empty;
//                    //        }


//                    //        File.To = ConvertFromTo(File.From);
//                    //    }
//                    //}

//                    if (string.IsNullOrEmpty(Export.Value))
//                    {
//                        Export.Value = string.Empty;
//                    }
//                }
//                if (string.IsNullOrEmpty(Discipline.Value))
//                {
//                    Discipline.Value = string.Empty;
//                }
//                Discipline.StartFileViewModel.StartFile.Path = ConvertFromTo(Discipline.StartFileViewModel.StartFile.Path);
//            }

//            if (string.IsNullOrEmpty(_xmlViewModel.IFCViewModel.IFC.Export))
//            {
//                _xmlViewModel.IFCViewModel.IFC.Export = string.Empty;
//            }
//            if (string.IsNullOrEmpty(_xmlViewModel.IFCViewModel.IFC.From))
//            {
//                _xmlViewModel.IFCViewModel.IFC.From = string.Empty;
//            }

//            _xmlViewModel.IFCViewModel.IFC.To = ConvertFromTo(_xmlViewModel.IFCViewModel.IFC.From);

//            return _xmlViewModel;
//        }

//        private string ConvertFromTo(string From)
//        {
//            if (!string.IsNullOrEmpty(From))
//            {
//                return baseToFolder + From.Substring(baseFromFolder.Length - 1);
//            }

//            return string.Empty;

//        }
//    }


//}
