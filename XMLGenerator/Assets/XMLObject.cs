using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using IfcConfigManager.ViewModel;

namespace IfcConfigManager.Assets
{
    class XMLObject
    {
        private XmlViewModel m_xmlViewModel;
        private IFCViewModel m_ifcViewModel;
        private ObservableCollection<DisciplineViewModel> m_disciplineViewModels; 
        public XMLObject(XmlViewModel xmlViewModel)
        {
            m_xmlViewModel = xmlViewModel;
            m_ifcViewModel = xmlViewModel.IFCViewModel;
            m_disciplineViewModels = xmlViewModel.DisciplineViewModels;
        }

        public XDocument GetXML()
        {

            XDocument xml = new XDocument(new XDeclaration("1.0", "ISO-8859-1", "yes"),
                                          new XElement("Project", new XAttribute("Name",m_xmlViewModel.ProjectName), new XAttribute("StructureVersion", "1.1"), from discipline in m_disciplineViewModels
                                                  select new XElement("Discipline", new XAttribute("Value",discipline.Value), from export in discipline.ExportViewModels
                                                    select new XElement("Export", new XAttribute("Value", export.Value),
                                                                                          new XAttribute("IFC", export.IFC),
                                                    from folder in export.FolderViewModel.Folders
                                                            select new XElement("Folder", new XAttribute("From", folder.From), 
                                                                                          new XAttribute("To", folder.To))
                                                  ),
                                                  new XElement("StartFile", new XAttribute("From", discipline.StartFileViewModel.StartFile.FromPath), new XAttribute("To", discipline.StartFileViewModel.StartFile.ToPath)
                                                  )),
                                                  new XElement("Files",
                                                  from file in m_xmlViewModel.FileViewModel.Files
                                                                      select new XElement("File", new XAttribute("From", file.From),
                                                                                                           new XAttribute("To", file.To))),

                                                  new XElement("IFC", new XAttribute("From", m_ifcViewModel.IFC.From), 
                                                                      new XAttribute("To", m_ifcViewModel.IFC.To),
                                                                      new XAttribute("Export", m_ifcViewModel.IFC.Export)
                                                  ),
                                                  new XElement("BaseFolder", new XAttribute("From", m_xmlViewModel.BaseFolderViewModel.FromBasePath),
                                                                             new XAttribute("To", m_xmlViewModel.BaseFolderViewModel.ToBasePath)
                                                  )));            

            return xml;
        }
    }
}
