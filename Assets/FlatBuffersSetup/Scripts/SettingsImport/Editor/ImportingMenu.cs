using System.Collections.Generic;
using FlatBuffersSetup.SettingsImport.Editor.Importers;
using UnityEditor;
using UnityEngine;

namespace FlatBuffersSetup.SettingsImport.Editor
{
    public static class ImportingMenu
    {
        [MenuItem("MyAwesomeProject/Import Settings/Import All Configs")]
        private static void ImportAllConfigs()
        {
            SettingsImportUtils.ImportAllConfigs();
        }
        
        [MenuItem("MyAwesomeProject/Import Settings/Import Buildings Settings")]
        private static void ImportBuildingsSettings()
        {
            var importer = new BuildingsImporter();
            SettingsImportUtils.ImportConfig(importer);
        }
        
        
        [MenuItem("MyAwesomeProject/Import Settings/Import Chests Settings")]
        private static void ImportChestsSettings()
        {
            var importer = new ChestsImporter();
            SettingsImportUtils.ImportConfig(importer);
        }
        
        // JUST FOR EXAMPLE
        [MenuItem("MyAwesomeProject/Import Settings/Import Building And Chest Settings")]
        private static void ImportSeveralSettings()
        {
            var importers = new IImporter[] { new BuildingsImporter(), new ChestsImporter() };
            SettingsImportUtils.ImportConfigs(importers);
        }
    }
}