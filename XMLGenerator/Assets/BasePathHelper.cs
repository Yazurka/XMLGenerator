using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IfcConfigManager.ViewModel;

namespace IfcConfigManager.Assets
{
    public static class BasePathHelper
    {
        public static void SetFromBaseFolders(XmlViewModel m_xmlViewModel, string FromBasePath)
        {
            m_xmlViewModel.IFCViewModel.IFC.FromRestriction = FromBasePath;

            foreach (var file in m_xmlViewModel.FileViewModel.Files)
            {
                file.FromRestriction = FromBasePath;
            }
            foreach (var dicsipline in m_xmlViewModel.DisciplineViewModels)
            {

                dicsipline.StartFileViewModel.StartFile.FromRestriction = FromBasePath;

                foreach (var export in dicsipline.ExportViewModels)
                {

                    foreach (var folder in export.FolderViewModel.Folders)
                    {
                        folder.FromRestriction = FromBasePath;
                    }
                }
            }
        }
    }
}
