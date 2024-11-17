namespace SaveManager
{
    [System.Serializable]
    public class LevelState
    {
        public int levelNumber;
        public bool completed = false;
        public string time = "00:00";
        public int stars = 0;
        public int starsCameraSwitches = 0;
        
        
        public LevelState(int levelNumber, bool completed, string time, int stars, int starsCameraSwitches)
        {
            this.levelNumber = levelNumber;
            this.completed = completed;
            this.time = time;
            this.stars = stars;
            this.starsCameraSwitches = starsCameraSwitches;
        }
        
        public int GetLevelNumber()
        {
            return levelNumber;
        }
        
        public bool GetCompleted()
        {
            return completed;
        }
        
        public string GetTime()
        {
            return time;
        }
        
        public int GetStars()
        {
            return stars;
        }
        
        public int GetStarsCameraSwitches()
        {
            return starsCameraSwitches;
        }
        
        public void SetCompleted(bool completed)
        {
            this.completed = completed;
        }
        
        public void SetTime(string time)
        {
            this.time = time;
        }
        
        public void SetStars(int stars)
        {
            this.stars = stars;
        }
        
        public void SetStarsCameraSwitches(int starsCameraSwitches)
        {
            this.starsCameraSwitches = starsCameraSwitches;
        }
    }
}