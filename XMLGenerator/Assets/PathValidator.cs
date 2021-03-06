﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IfcConfigManager.Assets
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

            if (SelectedPath.Substring(0, BaseFolderPath.Length) == BaseFolderPath)
            {
                return true;
            }

            return false;
        }

        public static async Task<string> SelectFolderPath(string BaseFolderPath)
        {
            var returnString = string.Empty;

            FolderBrowserDialog p = new FolderBrowserDialog();
            p.SelectedPath = BaseFolderPath;
            p.ShowDialog();
            var selectedPath = p.SelectedPath;

            if (string.IsNullOrEmpty(selectedPath))
            {
                return "";
            }

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
                        return await SelectFolderPath(BaseFolderPath);
                }
            }

            return selectedPath;
        }

        public static async Task<string> SelectFilePath(string BaseFolderPath)
        {
            var returnString = string.Empty;

            OpenFileDialog p = new OpenFileDialog();
            p.InitialDirectory = BaseFolderPath;
            p.ShowDialog();
            var selectedPath = p.FileName;

            if (string.IsNullOrEmpty(p.FileName))
            {
                return null;
            }

            bool isValid = PathValidator.ValidatePath(BaseFolderPath, selectedPath);

            
            if (!isValid)
            {
                var window = System.Windows.Application.Current.MainWindow as MetroWindow;
                MetroDialogSettings Settings = new MetroDialogSettings();
                Settings.AffirmativeButtonText = "Yes";
                Settings.NegativeButtonText = "No";
                var x = await window.ShowMessageAsync("Not allowed", "Selected filepat is invalid, do you want to select another?", MessageDialogStyle.AffirmativeAndNegative, Settings);

                switch (x)
                {
                    case MessageDialogResult.Negative:
                        return null;
                    case MessageDialogResult.Affirmative:
                        return await SelectFilePath(BaseFolderPath);
                }
            }

            return selectedPath;
        }

        public static async Task<List<string>> SelectMultipleFilePath(string BaseFolderPath)
        {
            var returnString = string.Empty;

            OpenFileDialog p = new OpenFileDialog();
            p.Multiselect = true;
            p.InitialDirectory = BaseFolderPath;
            p.ShowDialog();
            var selectedPath = p.FileNames.ToList();

            if (p.FileNames.Count() == 0)
            {
                return null;
            }

            selectedPath.RemoveAll(x => x == string.Empty);

            bool isValid = false;

            foreach (var path in selectedPath)
            {
                isValid = PathValidator.ValidatePath(BaseFolderPath, path);
            }

            if (!isValid)
            {
                var window = System.Windows.Application.Current.MainWindow as MetroWindow;
                MetroDialogSettings Settings = new MetroDialogSettings();
                Settings.AffirmativeButtonText = "Yes";
                Settings.NegativeButtonText = "No";
                var x = await window.ShowMessageAsync("Not allowed", "One or more files has an invalid path, do you want to select another?", MessageDialogStyle.AffirmativeAndNegative, Settings);

                switch (x)
                {
                    case MessageDialogResult.Negative:
                        return null;
                    case MessageDialogResult.Affirmative:
                        return await SelectMultipleFilePath(BaseFolderPath);
                }
            }

            return selectedPath;
        }

    }
}
