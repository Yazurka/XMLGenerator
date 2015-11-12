using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLGenerator.Assets
{
    public static class PathValidator
    {

        public static bool ValidatePath(string BaseFolderPath, string SelectedPath)
        {
            if (string.IsNullOrEmpty(SelectedPath))
            {
                return false;
            }

            if (SelectedPath.Length < BaseFolderPath.Length)
            {
                return false;
            }

            if (SelectedPath.Substring(0,BaseFolderPath.Length) == BaseFolderPath)
            {
                return true;
            }

            return false;
        }

        public static async Task<string> SelectPath(string BaseFolderPath)
        {
            var returnString = string.Empty;

            FolderBrowserDialog p = new FolderBrowserDialog();
            p.ShowDialog();
            var selectedPath = p.SelectedPath;
            var isValid = PathValidator.ValidatePath(BaseFolderPath, selectedPath);

            if (!isValid)
            {
                var window = System.Windows.Application.Current.MainWindow as MetroWindow;
                MetroDialogSettings Settings = new MetroDialogSettings();
                Settings.AffirmativeButtonText = "Yes";
                Settings.NegativeButtonText = "No";
                var x = await window.ShowMessageAsync("Not allowed", "The selected path is invalid, do you want to try again?", MessageDialogStyle.AffirmativeAndNegative, Settings);

                switch (x)
                {
                    case MessageDialogResult.Negative:
                        return "";
                    case MessageDialogResult.Affirmative:                        
                        return await SelectPath(BaseFolderPath);
                }
            }

            return p.SelectedPath;
        }


    }
}
