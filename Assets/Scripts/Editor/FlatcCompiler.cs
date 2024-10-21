using System.Diagnostics;
using System.IO;
using FlatbuffersSetup;
using Google.FlatBuffers;
using UnityEditor;
using UnityEngine;

public class FlatcCompiler
{
   // Path to the flatc executable. Make sure flatc is in your system PATH, or provide a full path here.
    private const string FlatcPath = "Assets/Flatbuffers/flatc";

    // Path to the .fbs schema file relative to the Unity project.
    private const string SchemaFilePath = "Assets/Scripts/Shared/Schemes/GameSettings.fbs";

    // Output directory for the generated C# files.
    private const string OutputDirectory = "Assets/Scripts/Generated/fbs";

    [MenuItem("Tools/Compile Flatbuffers Schemas")]
    public static void CompileFlatbuffersSchemas()
    {
        // Ensure the schema file exists.
        if (!File.Exists(SchemaFilePath))
        {
            UnityEngine.Debug.LogError($"Schema file not found: {SchemaFilePath}");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(OutputDirectory))
        {
            Directory.CreateDirectory(OutputDirectory);
        }

        // Build the command for flatc.
        string arguments = $"--csharp --gen-object-api -o \"{OutputDirectory}\" \"{SchemaFilePath}\"";

        try
        {
            // Run the flatc command.
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = FlatcPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processInfo))
            {
                // Read the output and error streams.
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    UnityEngine.Debug.Log("Flatbuffers compilation completed successfully.\n" + output);
                    AssetDatabase.Refresh(); // Refresh the AssetDatabase so Unity recognizes the new files.
                }
                else
                {
                    UnityEngine.Debug.LogError($"Flatbuffers compilation failed with errors:\n{error}");
                }
            }
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Error during Flatbuffers compilation: " + ex.Message);
        }
    }

    public void HowToSaveFromObjectToBinary()
    {
        var builder = new FlatBufferBuilder(1024);
        var gameSettings = new GameSettingsT();
        var offset = GameSettings.Pack(builder, gameSettings);
        builder.Finish(offset.Value);
        
        byte[] data = builder.SizedByteArray();
        string path = Path.Combine(Application.persistentDataPath, "GameSettings.bin");
        File.WriteAllBytes(path, data);
    }

    public void HoToLoadBinaryToObject()
    {
        var path = "";
        byte[] data = File.ReadAllBytes(path);
        ByteBuffer byteBuffer = new ByteBuffer(data);
        
        // Deserialize the byte buffer into a GameConfig object.
        GameSettings gameSettings = GameSettings.GetRootAsGameSettings(byteBuffer);
        var building = gameSettings.Buildings(2);
    }
}