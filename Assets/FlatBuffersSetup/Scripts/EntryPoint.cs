using FlatBuffersSetup.Common;
using UnityEngine;

namespace FlatBuffersSetup
{
    public class EntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            var gameSettingsProvider = new SettingsProvider("settings");
            var gameSettings = gameSettingsProvider.GameSettings;
            
            PrintAllBuildings(gameSettings);
            PrintAllChests(gameSettings);
        }

        private static void PrintAllBuildings(GameSettings gameSettings)
        {
            var buildingsAmount = gameSettings.BuildingsLength;
            for (var i = 0; i < buildingsAmount; i++)
            {
                var buildingSettingsEntry = gameSettings.Buildings(i);
                if (!buildingSettingsEntry.HasValue)
                {
                    continue;
                }
                
                var buildingSettings = buildingSettingsEntry.Value;
                var typeId = buildingSettings.TypeId;
                var titleLid = buildingSettings.TitleLid;
                var buildingPriceResourceType = buildingSettings.BuildingPriceResourceType;
                var buildingPriceValue = buildingSettings.BuildingPriceValue;
                var debugLine = $"Building: type = {typeId}\n" +
                                $"title = {titleLid}\n" +
                                $"buildingPriceResourceType = {buildingPriceResourceType}\n" +
                                $"buildingPriceValue = {buildingPriceValue}";
                
                Debug.Log(debugLine);
            }
        }

        private static void PrintAllChests(GameSettings gameSettings)
        {
            var chestsAmount = gameSettings.ChestsLength;
            for (var i = 0; i < chestsAmount; i++)
            {
                var chestSettingsEntry = gameSettings.Chests(i);
                if (!chestSettingsEntry.HasValue)
                {
                    continue;
                }
                
                var chestSettings = chestSettingsEntry.Value;
                var id = chestSettings.Id;
                var rarity = chestSettings.Rarity;
                var rewardsAmount = chestSettings.RewardsLength;
                var debugLine = $"Chest: id = {id}\n" +
                                $"rarity = {rarity}\n";
                for (var j = 0; j < rewardsAmount; j++)
                {
                    var rewardEntry = chestSettings.Rewards(j);
                    if (!rewardEntry.HasValue)
                    {
                        continue;
                    }

                    var rewardSettings = rewardEntry.Value;
                    var rewardSettingsExtType = rewardSettings.ExtType;

                    switch (rewardSettingsExtType)
                    {
                        case RewardExt.NONE:
                            break;
                        case RewardExt.ResourceReward:
                            var rewardSettingsExtResource = rewardSettings.ExtAsResourceReward();
                            debugLine += $"Reward: type = {rewardSettingsExtResource.ResourceType}\n" +
                                         $"amount = {rewardSettingsExtResource.ResourceAmount}\n";
                            break;
                        case RewardExt.AbilityReward:
                            var rewardSettingsExtAbility = rewardSettings.ExtAsAbilityReward();
                            debugLine += $"Reward: type = Ability\n" +
                                         $"amount = {rewardSettingsExtAbility.AbilityId}\n";
                            break;
                    }
                }
                
                Debug.Log(debugLine);
            }
        }
    }
}