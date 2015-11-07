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
            XElement xml = new XElement("Project",from dicipline in m_disciplineViewModels
                                                  select new XElement("Dicipline", from export in dicipline.ExportViewModels
                                                  select new XElement("Export", from folder in export.FileViewModels
                                                  select new XElement("File"), from file in export.FolderViewModels
                                                  select new XElement("Folders", from f in file.Folders select new XElement("Folder",f.IFC)))));

            return xml;
        }
    }
}
