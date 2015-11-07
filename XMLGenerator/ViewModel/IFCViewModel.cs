using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using XMLGenerator.Model;

namespace XMLGenerator.ViewModel
{
    public class IFCViewModel:ViewModelBase
    {
        private IFC m_ifc;

        public IFCViewModel()
        {
            IFC = new IFC();
        }

        public IFC IFC { get { return m_ifc; } set { m_ifc = value; OnPropertyChanged("IFC"); } }
    }
}
