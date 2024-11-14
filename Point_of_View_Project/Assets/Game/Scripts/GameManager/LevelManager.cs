using MenuManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.GameManager
{
    public class LevelManager : MonoBehaviour
    {
        private static int _nextLevel = 0;
        private static int _maxLevel = 2;
        private static int _levelPlayed = -1;

        private static string SceneName(int level)
        {
            // this should load the level to play
            return "Level" + level;
        }
    
        public static void LoadLevel(int level)
        {
            if (level < _maxLevel)
            {
                _levelPlayed = level;
                _nextLevel = level+1;
                SceneManager.LoadScene(SceneName(_levelPlayed));        
            }
        }

        public static void LoadFirstLevel()
        {
            _levelPlayed = 0;
            _nextLevel = 1;
            SceneManager.LoadScene("0-1");
        }

        public static bool CompletedAllLevels()
        {
            return (_nextLevel == _maxLevel);
        }
    
        public static void LoadNextLevel()
        {
            if (_nextLevel < _maxLevel)
            {
                _levelPlayed = _nextLevel;
                _nextLevel = _nextLevel + 1;
                SceneManager.LoadScene(SceneName(_levelPlayed));
            }
            else
            {
                SceneManager.LoadScene("MainMenu_WIP");
            }
        }

        public static void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //SceneManager.LoadScene("Level_EndgameTest");
        }

        public static void LoadMainMenuLevel()
        {
            SceneManager.LoadScene("MainMenu_WIP");
            MainMenu.Open();
        }
    }
}
