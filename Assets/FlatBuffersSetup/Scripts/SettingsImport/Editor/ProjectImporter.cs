using System.IO;
using System.Threading.Tasks;
using GoogleSheetsImporting.Editor;

namespace FlatBuffersSetup.SettingsImport.Editor
{
    public abstract class ProjectImporter : GoogleSheetsImporter
    {
        public string SheetName { get; }
        public GameSettingsT LocalSettings { get; protected set; }
        
        protected abstract string SettingsFileName { get; }
        protected string LocalSettingsFilePath => Path.Combine(ImportConstants.SETTINGS_FOLDER, SettingsFileName);

        protected ProjectImporter(string spreadsheetId, string sheetName) : base(spreadsheetId)
        {
            SheetName = sheetName;

            WithCredentials(ImportConstants.CREDENTIALS_PATH);
        }
        
        public void SaveToFile()
        {
            SettingsFileUtils.SaveSettings(LocalSettings, ImportConstants.SETTINGS_FOLDER, SettingsFileName);
        }
        
        public void LoadFromFile()
        {
            LocalSettings = SettingsFileUtils.LoadSettings(LocalSettingsFilePath);
        }

        protected Task DownloadAndParseSheet()
        {
            return DownloadAndParseSheet(SheetName);
        }
    }
}