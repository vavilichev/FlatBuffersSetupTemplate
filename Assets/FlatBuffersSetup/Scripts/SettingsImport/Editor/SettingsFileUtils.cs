using System.IO;
using Google.FlatBuffers;
using UnityEditor;

namespace FlatBuffersSetup.SettingsImport.Editor
{
    public static class SettingsFileUtils
    {
        public static GameSettingsT LoadSettings(string filePath)
        {
            if (File.Exists(filePath))
            {
                var data = File.ReadAllBytes(filePath);
                var byteBuffer = new ByteBuffer(data);
        
                var gameSettings = GameSettings.GetRootAsGameSettings(byteBuffer);
                var gameSettingsT = gameSettings.UnPack();
                
                return gameSettingsT;
            }
            
            return new GameSettingsT();
        }
        
        public static void SaveSettings(GameSettingsT gameSettingsT, string folderPath, string fileName)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            
            var filePath = Path.Combine(folderPath, fileName);
            var builder = new FlatBufferBuilder(1024);
            var offset = GameSettings.Pack(builder, gameSettingsT);
            builder.Finish(offset.Value);
        
            var data = builder.SizedByteArray();
            File.WriteAllBytes(filePath, data);
            
            AssetDatabase.Refresh();
        }
    }
}