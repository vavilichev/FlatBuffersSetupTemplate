using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlatBuffersSetup.Gameplay;

namespace FlatBuffersSetup.SettingsImport.Editor.Importers
{
    public class BuildingsImporter : ProjectImporter, IImporter
    {
        protected override string SettingsFileName => "BuildingsSettings.bytes";

        private BuildingSettingsT _currentBuildingSettings;

        public BuildingsImporter() : base(ImportConstants.MAIN_CONFIG_SPREADSHEET_ID,"Buildings") { }

        public async Task DownloadAndParse()
        {
            LocalSettings = new GameSettingsT
            {
                Buildings = new List<BuildingSettingsT>()
            };

            await DownloadAndParseSheet();
        }

        public void AddToSettings(GameSettingsT gameSettingsT)
        {
            gameSettingsT.Buildings = LocalSettings.Buildings;
        }

        protected override void ParseCell(string header, string cellData)
        {
            if (header == "TypeId")
            {
                _currentBuildingSettings = new BuildingSettingsT
                {
                    TypeId = cellData
                };
                LocalSettings.Buildings.Add(_currentBuildingSettings);
                return;
            }

            if (header == "TitleLID")
            {
                if (_currentBuildingSettings != null)
                {
                    _currentBuildingSettings.TitleLid = cellData;
                }

                return;
            }

            if (header == "BuildingPriceResourceType")
            {
                if (_currentBuildingSettings != null)
                {
                    _currentBuildingSettings.BuildingPriceResourceType = cellData;
                }

                return;
            }

            if (header == "BuildingPriceValue")
            {
                if (_currentBuildingSettings != null)
                {
                    _currentBuildingSettings.BuildingPriceValue = Convert.ToInt32(cellData);
                }
            }
        }
    }
}