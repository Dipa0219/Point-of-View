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

        
        
        public void SaveGameData(GameData gameData) {
            string json = JsonUtility.ToJson(gameData);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, SaveFileName), json);
        }
        
        
        public GameData LoadGameData() {
            GameData gameData = ScriptableObject.CreateInstance<GameData>();

            if (File.Exists(_saveFilePath)) {
                string json = File.ReadAllText(_saveFilePath);
                JsonUtility.FromJsonOverwrite(json, gameData);
            } else {
                gameData = InitializeDefaultData(gameData);
                SaveGameData(gameData);
            }
            return gameData;
        }
        
        public GameData UpdateLevel(String levelName, bool isCompleted, int stars, string completionTime) {
            GameData gameData = ScriptableObject.CreateInstance<GameData>();
            gameData = LoadGameData();
            
            // TODO 
            gameData.UpdateLevel(levelName, isCompleted, stars, completionTime);
            
            SaveGameData(gameData);
            return gameData;
        }
                
        public GameData UpdatePreferences(Preferences preferences) {
            GameData gameData = ScriptableObject.CreateInstance<GameData>();
            gameData = LoadGameData();
            
            // TODO 
            gameData.UpdatePreferences(preferences);
            
            SaveGameData(gameData);
            return gameData;
        }
        
        
        public GameData InitializeDefaultData(GameData gameData) {
            
            return gameData.InitializeDefaultData();
            
            /*gameData.playerName = "Player";
            gameData.levels = new LevelData[20];
        
            for (int i = 0; i < gameData.levels.Length; i++) {
                gameData.levels[i] = new LevelData {
                    isCompleted = false,
                    stars = 0,
                    completionTime = "00:00:00"
                };
            }
            
            */
        }
    }
}