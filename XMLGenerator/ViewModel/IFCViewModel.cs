using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using IfcConfigManager.Model;

namespace IfcConfigManager.ViewModel
{
    public class IFCViewModel:ViewModelBase
    {
        private IFC m_ifc;
        private XmlViewModel m_xmlViewModel;

        public IFCViewModel(XmlViewModel xmlViewModel)
        {
            m_xmlViewModel = xmlViewModel;
            IFC = new IFC() {FromRestriction = xmlViewModel.BaseFolderViewModel.FromBasePath };
        }

        public bool BasePathValid
        {
            get { return m_xmlViewModel.BasePathValid; }
            set { OnPropertyChanged("BasePathValid"); }
        }

        public IFC IFC { get { return m_ifc; } set { m_ifc = value; OnPropertyChanged("IFC"); } }
    }
}
