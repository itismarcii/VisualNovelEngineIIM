using System;
using System.IO;

using UnityEngine;
using SFB;
using Application = UnityEngine.Application;

namespace SaveSystem
{
    
    public static class SaveHandler
    {
        public static string SavePath { get; private set; } = null;
        public static Action OnSave, OnLoad;

        public static void SelectSaveFolder()
        {
            var path = StandaloneFileBrowser.OpenFolderPanel("Open Save Folder", Application.dataPath, false);
            if (path.Length > 0) SavePath = path[0] + @"/";
        }
        
        public static void Save(string fileName)
        {
            SavePath ??= Application.persistentDataPath + @"/";
            
            try
            {
                var filePath = Path.Combine(SavePath, fileName);
                SavableSerializer.SerializeToBinary(filePath);
                OnSave?.Invoke();

                // Debug.Log($"Successfully saved to '{filePath}'.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error saving to '{fileName}': {ex.Message}");
            }
        }
        
        public static void Load(string fileName)
        {
            try
            {
                var filePath = Path.Combine(SavePath, fileName);

                if (File.Exists(filePath))
                {
                    using var stream = new FileStream(filePath, FileMode.Open);
                    SavableSerializer.Deserialize(stream);
                    OnLoad?.Invoke();

                    // Debug.Log($"Successfully loaded from '{filePath}'.");
                }
                else
                {
                    Debug.LogWarning($"File not found: '{filePath}'.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading from '{fileName}': {ex.Message}");
            }
        }
    }
}
