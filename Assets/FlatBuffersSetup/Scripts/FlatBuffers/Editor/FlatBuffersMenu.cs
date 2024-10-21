using FlatBuffers.Editor;
using UnityEditor;

namespace FlatBuffersSetup.FlatBuffers.Editor
{
    public static class FlatBuffersMenu
    {
        private const string OUTPUT_DIRECTORY = "Assets/FlatBuffersSetup/Scripts/Generated/fbs";
        
        // Paths to the .fbs schema file relative to the Unity project.
        private static readonly string[] _schemaPaths =
        {
            "Assets/FlatBuffersSetup/Scripts/Shared/Schemas/GameSettings.fbs",
            "Assets/FlatBuffersSetup/Scripts/Shared/Schemas/Gameplay/Buildings.fbs",
            "Assets/FlatBuffersSetup/Scripts/Shared/Schemas/Gameplay/Chests.fbs",
            "Assets/FlatBuffersSetup/Scripts/Shared/Schemas/Common/Rewards.fbs",
        };

        [MenuItem("FlatBuffers/Compile FlatBuffers Schemas")]
        public static void CompileFlatBuffersSchemas()
        {
            FlatcCompiler.Compile(_schemaPaths, OUTPUT_DIRECTORY);
        }
    }
}