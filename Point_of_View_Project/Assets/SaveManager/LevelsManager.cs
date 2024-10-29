using System.Collections.Generic;

namespace SaveManager
{
    public class LevelsManager
    {
        private Dictionary<int, LevelState> levels = new Dictionary<int, LevelState>();

        
        public void AddLevel(int levelNumber, LevelState levelState)
        {
            if (levelNumber < 1)
            {
                return;
            }
            levels[levelNumber] = levelState;
        }

        
        public LevelState GetLevel(int levelNumber)
        {
            if (levels.TryGetValue(levelNumber, out LevelState levelState))
            {
                return levelState;
            }
            return null;
        }

        public bool RemoveLevel(int levelNumber)
        {
            return levels.Remove(levelNumber);
        }

        public bool LevelExists(int levelNumber)
        {
            return levels.ContainsKey(levelNumber);
        }
        
    }
}