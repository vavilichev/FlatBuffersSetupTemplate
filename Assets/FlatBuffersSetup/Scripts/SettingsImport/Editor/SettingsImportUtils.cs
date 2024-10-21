using System.Collections.Generic;
using FlatBuffersSetup.SettingsImport.Editor.Importers;
using UnityEngine;

namespace FlatBuffersSetup.SettingsImport.Editor
{
    public static class SettingsImportUtils
    {
        private static List<IImporter> GetAllImporters()
        {
            var allImporters = new List<IImporter>
            {
                new ChestsImporter(),
                new BuildingsImporter()
            };

            return allImporters;
        }

        public static async void ImportAllConfigs()
        {
            var importers = GetAllImporters();

            ImportConfigs(importers);
        }
        
        public static async void ImportConfig(IImporter importer)
        {
            var resultSettings = new GameSettingsT();
            var allImporters = GetAllImporters();
            allImporters.Remove(importer);

            await importer.DownloadAndParse();
            importer.SaveToFile();
            importer.AddToSettings(resultSettings);
            
            Debug.Log($"Local config has been updated and saved: {importer.SheetName}");

            foreach (var otherImporter in allImporters)
            {
                otherImporter.LoadFromFile();
                otherImporter.AddToSettings(resultSettings);
            }
            
            SettingsFileUtils.SaveSettings(resultSettings, ImportConstants.RESOURCES_FOLDER, ImportConstants.SETTINGS_FILE_NAME);
            Debug.Log($"{ImportConstants.SETTINGS_FILE_NAME} has been updated and saved");
        }

        public static async void ImportConfigs(IEnumerable<IImporter> importers)
        {
            var resultSettings = new GameSettingsT();
            var allImporters = GetAllImporters();
            
            foreach (var importer in importers)
            {
                allImporters.Remove(importer);

                await importer.DownloadAndParse();
                importer.SaveToFile();
                importer.AddToSettings(resultSettings);
                
                Debug.Log($"Local config has been updated and saved: {importer.SheetName}");
            }

            foreach (var otherImporter in allImporters)
            {
                otherImporter.LoadFromFile();
                otherImporter.AddToSettings(resultSettings);
            }
            
            SettingsFileUtils.SaveSettings(resultSettings, ImportConstants.RESOURCES_FOLDER, ImportConstants.SETTINGS_FILE_NAME);
            Debug.Log($"{ImportConstants.SETTINGS_FILE_NAME} has been updated and saved");
        }
    }
}