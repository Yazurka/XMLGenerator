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
        private static bool Cancel = false;

        private static bool ValidatePath(string BaseFolderPath, string SelectedPath)
        {
            if (string.IsNullOrEmpty(BaseFolderPath))
            {
                return false;
            }

            if (SelectedPath.Substring(BaseFolderPath.Length) == BaseFolderPath)
            {
                return true;
            }

            return false;
        }

        public static string SelectPath(string BaseFolderPath)
        {
            var returnString = string.Empty;

            FolderBrowserDialog p = new FolderBrowserDialog();
            p.ShowDialog();
            var selectedPath = p.SelectedPath;
            var isValid = PathValidator.ValidatePath(BaseFolderPath, selectedPath);
            while (PathValidator.ValidatePath(BaseFolderPath, selectedPath) && !Cancel)
            {
                if (PathValidator.ValidatePath(BaseFolderPath, selectedPath))
                {
                    returnString = p.SelectedPath;
                    break;
                }
                else
                {
                    Message(BaseFolderPath, p.SelectedPath);
                }        
            }
            return p.SelectedPath;
        }

        private static async void Message(string BaseFolderPath, string SelectedPath)
        {
            var window = System.Windows.Application.Current.MainWindow as MetroWindow;
            MetroDialogSettings Settings = new MetroDialogSettings();
            Settings.AffirmativeButtonText = "Yes";
            Settings.NegativeButtonText = "No";
            var x = await window.ShowMessageAsync("Not allowed", "The selected path is invalid, do you want to try again?", MessageDialogStyle.AffirmativeAndNegative, Settings);

            switch (x)
            {
                case MessageDialogResult.Negative:
                    Cancel = true;
                    break;
                case MessageDialogResult.Affirmative:                    
                    break;
            }
        }

    }
}
