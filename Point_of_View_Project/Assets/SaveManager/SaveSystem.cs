using System;
using System.IO;
using UnityEngine;

namespace SaveManager
{
    public class SaveSystem: MonoBehaviour
    {
        private const string SaveFileName = "GameData.json";
        //public GameData gameData;
        private static string _saveFilePath = Path.Combine(Application.persistentDataPath, SaveFileName);

        
        
        public static void SaveGameData(GameData gameData) {
            string json = JsonUtility.ToJson(gameData);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, SaveFileName), json);
        }
        
        
        public static GameData LoadGameData() {
            GameData gameData = ScriptableObject.CreateInstance<GameData>();

            if (CheckIfDataExists()) {
                string json = File.ReadAllText(_saveFilePath);
                JsonUtility.FromJsonOverwrite(json, gameData);
            } else {
                gameData = InitializeDefaultData(gameData);
                SaveGameData(gameData);
            }
            return gameData;
        }
        
        
        public static bool CheckIfDataExists() {
            return File.Exists(_saveFilePath);
        }
        
        
        public static GameData UpdateLevel(String levelName, bool isCompleted, int stars, string completionTime) {
            GameData gameData = ScriptableObject.CreateInstance<GameData>();
            gameData = LoadGameData();
            
            // TODO 
            gameData.UpdateLevel(levelName, isCompleted, stars, completionTime);
            
            SaveGameData(gameData);
            return gameData;
        }
                
        public static GameData UpdatePreferences(Preferences preferences) {
            GameData gameData = ScriptableObject.CreateInstance<GameData>();
            gameData = LoadGameData();
            
            // TODO 
            gameData.UpdatePreferences(preferences);
            
            SaveGameData(gameData);
            return gameData;
        }
        
        
        public static GameData InitializeDefaultData(GameData gameData) {
            
            return gameData.InitializeDefaultData();
        }

        public static void SaveName(string s)
        {
            GameData gameData = ScriptableObject.CreateInstance<GameData>();
            gameData = InitializeDefaultData(gameData);
            gameData.SetPlayerName(s);
            SaveGameData(gameData);
        }
    }
}