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
using IfcConfigManager.Model;
using IfcConfigManager.View;
using IfcConfigManager.ViewModel;
using File = IfcConfigManager.Model.File;

namespace IfcConfigManager.Assets
{
    public class XMLMapper
    {
        public XmlViewModel MapXMLToXmlViewModel(string path, XmlViewModel xmlViewModel)
        {
             
            var XMLvm = new XmlViewModel();
            XElement xdoc = XElement.Load(path);
            XMLvm.ProjectName = xdoc.Attribute("Name").Value;

            var basef = xdoc.Element("BaseFolder");
            var BaseVm = new BaseFolderViewModel(XMLvm) { FromBasePath = basef.Attribute("From").Value, ToBasePath = basef.Attribute("To").Value };
            XMLvm.BaseFolderViewModel = BaseVm;

            var Dicsiplines = xdoc.Elements("Discipline");
            var disciplinecol = new ObservableCollection<DisciplineViewModel>();
            foreach (var dis in Dicsiplines)
            {
                var disciplineVM = new DisciplineViewModel(XMLvm);
                disciplineVM.Value = dis.Attribute("Value").Value;
                var exportVMCOL = new ObservableCollection<ExportViewModel>();
                
                var startfile = new StartFile {FromPath = dis.Element("StartFile").Attribute("From").Value, ToPath = dis.Element("StartFile").Attribute("To").Value };
                disciplineVM.StartFileViewModel = new StartFileViewModel(XMLvm) {StartFile = startfile};
                var exps = dis.Elements("Export");
                foreach (var exp in exps)
                {
                    var expVM = new ExportViewModel(XMLvm);

                    var fVM = new FolderViewModel(XMLvm);
                    var foldersCol = new ObservableCollection<Folder>();
                    var folders = exp.Elements("Folder");
                    foreach (var folder in folders)
                    {
                       
                        var From = folder.Attribute("From").Value;
                        var To = folder.Attribute("To").Value;

                        var f= new Folder {From = From,To=To};
                        foldersCol.Add(f);
                        fVM.Folders = foldersCol;
                    }                    

                    fVM.Folders = foldersCol;
                    expVM.FolderViewModel = fVM;
                    expVM.Value = exp.Attribute("Value").Value;
                    expVM.IFC = exp.Attribute("IFC").Value;
                    if (exp.Attribute("Id") is XAttribute xAtt)
                    {
                        if (Guid.TryParse(xAtt.Value, out Guid id))
                        {
                            expVM.Id = id;
                        };
                    }
                    if (expVM.Id == null || expVM.Id == Guid.Empty)
                    {
                        expVM.Id = Guid.NewGuid();
                    }

                    exportVMCOL.Add(expVM);
                }
                disciplineVM.ExportViewModels = exportVMCOL;
                disciplinecol.Add(disciplineVM);

            }
            var fileVM = new FileViewModel(XMLvm);

            var files = xdoc.Element("Files").Elements("File");
            var filecol = new ObservableCollection<File>();
            foreach (var file in files)
            {
                var f = new File(fileVM) {From = file.Attribute("From").Value,To=file.Attribute("To").Value};
                filecol.Add(f);
            }

            fileVM.Files = filecol;

            XMLvm.FileViewModel = fileVM;
            XMLvm.DisciplineViewModels = disciplinecol;

            var ifc = xdoc.Element("IFC");

            var ifcvm = new IFCViewModel(XMLvm)
            {
                IFC = new IFC { Export = ifc.Attribute("Export").Value, To = ifc.Attribute("To").Value, From = ifc.Attribute("From").Value }
            };
            XMLvm.IFCViewModel = ifcvm;

            XMLvm.SavePath = path;
                
            return XMLvm;
        }
    }
}
