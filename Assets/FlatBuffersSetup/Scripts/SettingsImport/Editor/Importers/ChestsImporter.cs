using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlatBuffersSetup.Common;
using FlatBuffersSetup.Gameplay;

namespace FlatBuffersSetup.SettingsImport.Editor.Importers
{
    public class ChestsImporter : ProjectImporter, IImporter
    {
        protected override string SettingsFileName => "ChestsSettings.bytes";

        private ChestSettingsT _currentChestSettings;
        private RewardSettingsT _currentRewardSettings;
        private ResourceRewardT _currentResourceReward;
        private AbilityRewardT _currentAbilityReward;

        public ChestsImporter() : base(ImportConstants.MAIN_CONFIG_SPREADSHEET_ID, "Chests") { }

        public async Task DownloadAndParse()
        {
            LocalSettings = new GameSettingsT
            {
                Chests = new List<ChestSettingsT>()
            };
            
            await DownloadAndParseSheet();
        }

        public void AddToSettings(GameSettingsT gameSettingsT)
        {
            gameSettingsT.Chests = LocalSettings.Chests;
        }

        protected override void ParseCell(string header, string cellData)
        {
            if (header == "ID")
            {
                _currentChestSettings = new ChestSettingsT()
                {
                    Id = Convert.ToInt32(cellData),
                    Rewards = new List<RewardSettingsT>()
                };
                LocalSettings.Chests.Add(_currentChestSettings);
                return;
            }

            if (header == "Rarity")
            {
                if (_currentChestSettings != null)
                {
                    _currentChestSettings.Rarity = cellData;
                }
                return;
            }

            if (header == "RewardType")
            {
                _currentResourceReward = null;
                _currentAbilityReward = null;
               
                if (_currentChestSettings != null)
                {
                    _currentRewardSettings = new RewardSettingsT();

                    if (cellData is "SoftCurrency" or "HardCurrency")
                    {
                        _currentResourceReward = new ResourceRewardT
                        {
                            ResourceType = cellData,
                        };
                        
                        var rewardExt = new RewardExtUnion
                        {
                            Type = RewardExt.ResourceReward,
                            Value = _currentResourceReward
                        };
                        
                        _currentRewardSettings.Ext = rewardExt;
                        _currentChestSettings.Rewards.Add(_currentRewardSettings);

                        return;
                    }

                    if (cellData == "Ability")
                    {
                        _currentAbilityReward = new AbilityRewardT();
                        var rewardExt = new RewardExtUnion
                        {
                            Type = RewardExt.AbilityReward,
                            Value = _currentAbilityReward
                        };
                        
                        _currentRewardSettings.Ext = rewardExt;
                        _currentChestSettings.Rewards.Add(_currentRewardSettings);
                        
                        return;
                    }
                }
                return;
            }

            if (header == "RewardValue")
            {
                if (_currentAbilityReward != null)
                {
                    _currentAbilityReward.AbilityId = cellData;
                    return;
                }

                if (_currentResourceReward != null)
                {
                    _currentResourceReward.ResourceAmount = Convert.ToInt32(cellData);
                    return;
                }
            }
        }

    }
}