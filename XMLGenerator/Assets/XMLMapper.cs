using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XMLGenerator.Model;
using XMLGenerator.View;
using XMLGenerator.ViewModel;
using File = XMLGenerator.Model.File;

namespace XMLGenerator.Assets
{
    public class XMLMapper
    {
        public XmlViewModel MapXMLToXmlViewModel(string path)
        {
            var XMLvm = new XmlViewModel();
            XElement xdoc = XElement.Load(path);
            var Dicsiplines = xdoc.Elements("Discipline");
            var disciplinecol = new ObservableCollection<DisciplineViewModel>();
            foreach (var dis in Dicsiplines)
            {
               var disciplineVM = new DisciplineViewModel();
                disciplineVM.Value = dis.Attribute("Value").Value;
                var exportVMCOL = new ObservableCollection<ExportViewModel>();
                
                var startfile = new StartFile {Path = dis.Element("StartFile").Attribute("Value").Value};
                disciplineVM.StartFileViewModel = new StartFileViewModel {StartFile = startfile};
                var exps = dis.Elements("Export");
                foreach (var exp in exps)
                { 
                    var expVM = new ExportViewModel();

                    var fVM = new FolderViewModel();
                    var foldersCol = new ObservableCollection<Folder>();
                    var folders = exp.Element("Folders").Elements("Folder");
                    foreach (var folder in folders)
                    {
                       
                        var From = folder.Attribute("From").Value;
                        var To = folder.Attribute("To").Value;
                        var IFC = folder.Attribute("IFC").Value;

                        var f= new Folder {From = From,To=To,IFC  = IFC};
                        foldersCol.Add(f);
                        fVM.Folders = foldersCol;
                    }
                    
                    fVM.Folders = foldersCol;
                    expVM.FolderViewModel = fVM;
                    expVM.Value = exp.Attribute("Value").Value;
                    exportVMCOL.Add(expVM);
                }
                disciplineVM.ExportViewModels = exportVMCOL;
                disciplinecol.Add(disciplineVM);

            }

            var files = xdoc.Element("Files").Elements("File");
            var filecol = new ObservableCollection<File>();
            foreach (var file in files)
            {
                var f = new File {From = file.Attribute("From").Value,To=file.Attribute("To").Value};
                filecol.Add(f);
            }
            var fileVM = new FileViewModel();
            fileVM.Files = filecol;

            XMLvm.FileViewModel = fileVM;
            XMLvm.DisciplineViewModels = disciplinecol;

            var basef = xdoc.Element("BaseFolder");

            var BaseVm = new BaseFolderViewModel {FromBasePath = basef.Attribute("From").Value,ToBasePath = basef.Attribute("To").Value};
            XMLvm.BaseFolderViewModel = BaseVm;

            var ifc = xdoc.Element("IFC");

            var ifcvm = new IFCViewModel
            {
                IFC = new IFC { Export = ifc.Attribute("Export").Value, To = ifc.Attribute("To").Value, From = ifc.Attribute("From").Value }
            };
            XMLvm.IFCViewModel = ifcvm;
            return XMLvm;
        }
    }
}
