using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLGenerator.Assets;
using XMLGenerator.ViewModel;

namespace XMLGenerator.Model
{
    public class StartFile : ViewModelBase
    {
        private string m_fromPath;
        private string m_toPath;

        private async void setPath()
        {
            var x = await PathValidator.SelectFilePath(FromRestriction);
            if (!string.IsNullOrEmpty(x))
            {
                FromPath = x;
            }
        }

        public string FromRestriction { get; set; }
        public string FromPath { get { return m_fromPath; } set { m_fromPath = value; OnPropertyChanged("FromPath"); } }
        public string ToPath { get { return m_toPath; } set { m_toPath = value; OnPropertyChanged("ToPath"); } }
    }
}
