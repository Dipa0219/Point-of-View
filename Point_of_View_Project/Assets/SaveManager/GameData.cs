﻿ using System;
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

        public bool easyModeOn;
        public bool tipsOn;
        
        public GameData(string playerName, Preferences preferences)
        {
            this.playerName = playerName;
            this.Preferences = preferences;

            easyModeOn = false;
            tipsOn = true;
                        
            this.world1Levels = new LevelState[NumLevelsPerWorld];
            this.world2Levels = new LevelState[NumLevelsPerWorld];
            this.world3Levels = new LevelState[NumLevelsPerWorld];
            
            for (int i = 0; i < NumLevelsPerWorld; i++) {
                world1Levels[i] = new LevelState(i + 1, false, "99:99", 0, 999);
                world2Levels[i] = new LevelState(i + 1, false, "99:99", 0, 999);
                world3Levels[i] = new LevelState(i + 1, false, "99:99", 0, 999);
            }
            this.easyModeOn = false;
            this.tipsOn = true;
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
            this.easyModeOn = false;
            this.tipsOn = true;
        }
        
        

        public GameData InitializeDefaultData() {
            playerName = "Player1";
            world1Levels = new LevelState[NumLevelsPerWorld];
            world2Levels = new LevelState[NumLevelsPerWorld];
            world3Levels = new LevelState[NumLevelsPerWorld];
            
            Preferences = new Preferences(1, 1, 1, playerName);
            
            for (int i = 0; i < world1Levels.Length; i++) {
                world1Levels[i] = new LevelState(i + 1, false, "00:00", 0, 999);
                world2Levels[i] = new LevelState(i + 1, false, "00:00", 0, 999);
                world3Levels[i] = new LevelState(i + 1, false, "00:00", 0, 999);
            }

            easyModeOn = false;
            tipsOn = true;
            return this;
        }

        
        public void UpdateLevel(string levelName, bool isCompleted, int stars, string completionTime)
        {
            var parts = levelName.Split('-');
            int world = int.Parse(parts[0]);
            int level = int.Parse(parts[1]);
            
            switch (world)
            {
                case 0:
                    if(LevelShouldBeUpdated(world1Levels[level - 1], isCompleted, stars, completionTime))
                    {
                        world1Levels[level - 1].SetCompleted(isCompleted);
                        world1Levels[level - 1].SetStars(stars);
                        world1Levels[level - 1].SetTime(completionTime);
                    }
                    break;
                case 1:
                    if(LevelShouldBeUpdated(world2Levels[level - 1], isCompleted, stars, completionTime))
                    {
                        world2Levels[level - 1].SetCompleted(isCompleted);
                        world2Levels[level - 1].SetStars(stars);
                        world2Levels[level - 1].SetTime(completionTime);
                    }
                    break;
                case 2:
                    if(LevelShouldBeUpdated(world3Levels[level - 1], isCompleted, stars, completionTime))
                    {
                        world3Levels[level - 1].SetCompleted(isCompleted);
                        world3Levels[level - 1].SetStars(stars);
                        world3Levels[level - 1].SetTime(completionTime);
                    }
                    break;
            }
        }

        
        public void UpdatePreferences(Preferences preferences)
        {
            throw new NotImplementedException();
        }
        
     
        public bool LevelShouldBeUpdated(LevelState levelState, bool isCompleted, int stars, string completionTime)
        {
            if(isCompleted == false)  // not completed -> no need to update
                return false;
            
            if(levelState.GetCompleted() == false && isCompleted == true) // before not completed now completed -> update
                return true;
            
            if(levelState.GetCompleted() == true && isCompleted == true)
            {
                if(levelState.GetStars() < stars) // if new stars are more than the old ones -> update
                {
                    return true;   
                 }else if(levelState.GetStars() > stars) // if new stars are less than the old ones -> no need to update
                 {
                     return false;
                 }else if(levelState.GetStars() == stars) // if new stars are equal to the old ones -> check time
                 {
                     if(TimeToNumber(levelState.GetTime()) > TimeToNumber(completionTime)) // if new time is less than the old one -> update
                     {
                         return true;
                     }
                 }
            }
            return false;
        }
        
        
        public int TimeToNumber(string time)
        {
            var parts = time.Split(':');
            int minutes = int.Parse(parts[0]);
            int seconds = int.Parse(parts[1]);
            return minutes * 60 + seconds;
        }
        
        public void SetPlayerName(string name)
        {
            playerName = name;
        }
        
        public void setEasyMode(bool easyMode)
        {
            easyModeOn = easyMode;
        }
        
        public bool getEasyMode()
        {
            return easyModeOn;
        }


        public LevelState GetLevel(int world, int num)
        {
            switch (world)
            {
                case 1:
                    return world1Levels[num];
                case 2:
                    return world2Levels[num];
                case 3:
                    return world3Levels[num];
                default:
                    return null;
            }
        }


        public void setTips(bool tips)
        {
            tipsOn = tips;
            //throw new NotImplementedException();
        }

        public bool getTips()
        {
            return tipsOn;
            //throw new NotImplementedException();
        }
    }
}