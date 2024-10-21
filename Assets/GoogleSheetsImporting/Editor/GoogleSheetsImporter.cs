using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;
using FileMode = System.IO.FileMode;
using Task = System.Threading.Tasks.Task;

namespace GoogleSheetsImporting.Editor
{
    public abstract class GoogleSheetsImporter
    {
        private readonly string _spreadsheetId;
        private readonly List<string> _headers = new();
        private SheetsService _sheetsService;
        
        protected GoogleSheetsImporter(string spreadsheetId)
        {
            _spreadsheetId = spreadsheetId;
        }

        public GoogleSheetsImporter WithCredentials(string credentialsPath)
        {
            GoogleCredential credential;
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(SheetsService.ScopeConstants.SpreadsheetsReadonly);
            }

            _sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });

            return this;
        }

        public GoogleSheetsImporter WithApiKey(string apiKey)
        {
            _sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
            });

            return this;
        }

        public async Task DownloadAndParseSheet(string sheetName)
        {
            Debug.Log($"Downloading sheet: {sheetName}..");

            var range = $"{sheetName}!A1:Z";
            var request = _sheetsService.Spreadsheets.Values.Get(_spreadsheetId, range);

            ValueRange response;
            try
            {
                response = await request.ExecuteAsync();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error retrieving Google Sheets data: {e.Message}");
                return;
            }

            // Parse the received data
            if (response is { Values: not null })
            {
                var tableArray = response.Values;
                Debug.Log($"Sheet downloaded successfully: {sheetName}");
                Debug.Log($"Parsing sheet: {sheetName}..");

                var firstRow = tableArray[0];
                foreach (var cell in firstRow)
                {
                    _headers.Add(cell.ToString());
                }

                var rowsCount = tableArray.Count;
                for (var rowIndex = 1; rowIndex < rowsCount; rowIndex++)
                {
                    var row = tableArray[rowIndex];
                    var rowLength = row.Count;

                    for (var columnIndex = 0; columnIndex < rowLength; columnIndex++)
                    {
                        var cellData = row[columnIndex].ToString();
                        
                        if (!string.IsNullOrEmpty(cellData))
                        {
                            var header = _headers[columnIndex];
                            
                            ParseCell(header, cellData);
                        }
                    }
                }
                
                Debug.Log($"Sheet parsed successfully: {sheetName}");
            }
            else
            {
                Debug.LogWarning($"No data found in Google Sheet: {sheetName}");
            }
        }

        protected abstract void ParseCell(string header, string cellData);
    }
}