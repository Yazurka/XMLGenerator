using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLGenerator.Assets
{
    public static class PathValidator
    {
        public static bool ValidatePath(string BaseFolderPath, string SelectedPath)
        {
            var IsValid = false;

            if (SelectedPath.Substring(BaseFolderPath.Length) == BaseFolderPath)
            {
                IsValid = true;
            }           
            

            return IsValid;
        }
    }
}
