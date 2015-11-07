using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using XMLGenerator.ViewModel;

namespace XMLGenerator.Assets
{
    class XMLObject
    {
        private IFCViewModel m_ifcViewModel;
        private ObservableCollection<DisciplineViewModel> m_disciplineViewModels; 
        public XMLObject(IFCViewModel ifcViewModel, ObservableCollection<DisciplineViewModel> disciplineViewModels)
        {
            m_ifcViewModel = ifcViewModel;
            m_disciplineViewModels = disciplineViewModels;
        }

        public XElement GetXML()
        {

            XElement xml = new XElement("Project",from discipline in m_disciplineViewModels
                                                  select new XElement("Dicipline", from export in discipline.ExportViewModels
                                                    select new XElement("Export", new XAttribute("Value", export.Value), from files in export.FileViewModels
                                                        select new XElement("Files", from file in files.Files
                                                            select new XElement("File", new XAttribute("From", file.From), 
                                                                                        new XAttribute("To", file.To))), 
                                                    from folders in export.FolderViewModels
                                                        select new XElement("Folders", from folder in folders.Folders
                                                            select new XElement("Folder", new XAttribute("From", folder.From), 
                                                                                          new XAttribute("To", folder.To), 
                                                                                          new XAttribute("IFC", folder.IFC)))
                                                  ),
                                                  new XElement("StartFile", discipline.StartFileViewModel.StartFile.Path
                                                  )), 
                                                  new XElement("IFC", new XAttribute("From", m_ifcViewModel.IFC.From), 
                                                                      new XAttribute("To", m_ifcViewModel.IFC.To),
                                                                      new XAttribute("Export", m_ifcViewModel.IFC.Export)
                                                  ));

            return xml;
        }
    }
}
