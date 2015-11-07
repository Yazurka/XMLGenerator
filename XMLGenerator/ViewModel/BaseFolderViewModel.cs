using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLGenerator.ViewModel
{
    public class BaseFolderViewModel : ViewModelBase
    {
        private string m_fromBasePath;

        public string FromBasePath { get { return m_fromBasePath; } set { m_fromBasePath = value; OnPropertyChanged("FromBasePath"); } }
        private string m_toBasePath;

        public string ToBasePath { get { return m_toBasePath; } set { m_toBasePath = value; OnPropertyChanged("ToBasePath"); } }
    }
}
