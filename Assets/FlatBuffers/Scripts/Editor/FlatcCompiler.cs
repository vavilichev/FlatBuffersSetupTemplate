using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;

namespace FlatBuffers.Editor
{
    public static class FlatcCompiler
    {
        private const string FLATC_PATH = "Assets/FlatBuffers/flatc";

        /// <summary>
        /// Compile a single schema to C# object
        /// </summary>
        /// <param name="schemaPath"></param>
        /// <param name="outputDirectory"></param>
        public static void Compile(string schemaPath, string outputDirectory)
        {
            CleanOldScripts(outputDirectory);

            var arguments = $"--csharp --gen-object-api -o \"{outputDirectory}\" \"{schemaPath}\"";
        
            Compile(arguments);
        }

        /// <summary>
        /// Compile a list of schemes into C# objects
        /// </summary>
        /// <param name="schemaPaths"></param>
        /// <param name="outputDirectory"></param>
        public static void Compile(IEnumerable<string> schemaPaths, string outputDirectory)
        {
            CleanOldScripts(outputDirectory);
        
            var schemaPathsSingleLine = "";
            foreach (var schemaPath in schemaPaths)
            {
                schemaPathsSingleLine += $"\"{schemaPath}\" ";
            }
        
            var arguments = $"--csharp --gen-object-api -o \"{outputDirectory}\" {schemaPathsSingleLine}";

            Compile(arguments);
        }

        private static void Compile(string arguments)
        {
            UnityEngine.Debug.Log("FlatBuffers compilation started");

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = FLATC_PATH,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(processInfo))
                {
                    var output = process.StandardOutput.ReadToEnd();
                    var error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        UnityEngine.Debug.Log("FlatBuffers compilation completed successfully\n" + output);
                        AssetDatabase.Refresh(); // Refresh the AssetDatabase
                    }
                    else
                    {
                        UnityEngine.Debug.LogError($"FlatBuffers compilation failed with errors:\n{error}");
                    }
                }
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError("Error during FlatBuffers compilation: " + ex.Message);
            }
        }

        private static void CleanOldScripts(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        
            Directory.CreateDirectory(directory);
        }
    }
}