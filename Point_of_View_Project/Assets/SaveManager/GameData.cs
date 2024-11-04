 using System;
 using UnityEngine;
 
 namespace SaveManager
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Game Data")]
    public class GameData: ScriptableObject
    {
        public string playerName;
        const int NumLevelsPerWorld = 3;
        public LevelState[] world1Levels = new LevelState[NumLevelsPerWorld];
        public LevelState[] world2Levels = new LevelState[NumLevelsPerWorld];
        public LevelState[] world3Levels = new LevelState[NumLevelsPerWorld];
        public Preferences Preferences;

        public GameData(string playerName, Preferences preferences)
        {
            this.playerName = playerName;
            this.Preferences = preferences;
        }        
        
        public GameData(string playerName, Preferences preferences, LevelState[] w1, LevelState[] w2, LevelState[] w3)
        {
            this.playerName = playerName;
            this.Preferences = preferences;
            
            this.world1Levels = new LevelState[w1.Length];
            this.world2Levels = new LevelState[w2.Length];
            this.world3Levels = new LevelState[w3.Length];

            Array.Copy(w1, this.world1Levels, w1.Length);
            Array.Copy(w2, this.world2Levels, w2.Length);
            Array.Copy(w3, this.world3Levels, w3.Length);
            
        }
        
        

        public GameData InitializeDefaultData() {
            playerName = "Player1";
            world1Levels = new LevelState[NumLevelsPerWorld];
            world2Levels = new LevelState[NumLevelsPerWorld];
            world3Levels = new LevelState[NumLevelsPerWorld];
            
            Preferences = new Preferences(1, 1, 1, playerName);
            
            for (int i = 0; i < world1Levels.Length; i++) {
                world1Levels[i] = new LevelState(i + 1, false, "00:00", 0, 0);
                world2Levels[i] = new LevelState(i + 1, false, "00:00", 0, 0);
                world3Levels[i] = new LevelState(i + 1, false, "00:00", 0, 0);
            }

            return this;
        }

        public void UpdateLevel(string levelName, bool isCompleted, int stars, string completionTime)
        {
            throw new NotImplementedException();
        }

        public void UpdatePreferences(Preferences preferences)
        {
            throw new NotImplementedException();
        }
        
    }
    
    
}