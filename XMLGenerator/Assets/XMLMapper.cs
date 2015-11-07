using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XMLGenerator.Model;
using XMLGenerator.View;
using XMLGenerator.ViewModel;

namespace XMLGenerator.Assets
{
    public class XMLMapper
    {
        public XmlViewModel MapXMLToXmlViewModel(string path)
        {
            StringBuilder output = new StringBuilder();

            var XMLvm = new XmlViewModel();

            XElement xdoc = XElement.Load(path);
           var Dicsiplines = xdoc.Elements("Discipline");
            var disciplinecol = new ObservableCollection<DisciplineViewModel>();
            foreach (var dis in Dicsiplines)
            {
               var disciplineVM = new DisciplineViewModel();

                var exportVMCOL = new ObservableCollection<ExportViewModel>();
                

                var exps = dis.Elements("Export");
                foreach (var exp in exps)
                {
                    
                    var foldersCol = new ObservableCollection<Folder>();
                    var folders = exp.Element("Folders").Elements("Folder");
                    foreach (var folder in folders)
                    {
                        var fVM = new FolderViewModel();
                        var From = folder.Attribute("From").Value;
                        var To = folder.Attribute("To").Value;
                        var IFC = folder.Attribute("IFC").Value;

                        var f= new Folder {From = From,To=To,IFC  = IFC};
                        foldersCol.Add(f);
                        fVM.Folders = foldersCol;
                    }
                   
                }

                
            }
            //TODO:read file and mapp
           
           
            return XMLvm;
        }
    }
}
