using Google.FlatBuffers;
using UnityEngine;

namespace FlatBuffersSetup
{
    public class SettingsProvider
    {
        public GameSettings GameSettings { get; }

        public SettingsProvider(string settingsFilePath)
        {
            var gameSettingsAsset = Resources.Load<TextAsset>(settingsFilePath);
            var byteBuffer = new ByteBuffer(gameSettingsAsset.bytes);
            GameSettings = GameSettings.GetRootAsGameSettings(byteBuffer);
        }
    }
}