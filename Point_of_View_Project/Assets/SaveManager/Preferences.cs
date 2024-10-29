namespace SaveManager
{
    public class Preferences
    {
        private float masterVolume;
		private float sfxVolume;
        private float musicVolume;
        private string playerName;
        
        public Preferences(float masterVolume, float sfxVolume, float musicVolume, string playerName)
        {
            this.masterVolume = masterVolume;
            this.sfxVolume = sfxVolume;
            this.musicVolume = musicVolume;
            this.playerName = playerName;
        }
        
        public float GetMasterVolume()
        {
            return masterVolume;
        }
        
        public float GetSFXVolume()
        {
            return sfxVolume;
        }
        
        public float GetMusicVolume()
        {
            return musicVolume;
        }
        
        public string GetPlayerName()
        {
            return playerName;
        }
        
        public void SetMasterVolume(float volume)
        {
            masterVolume = volume;
        }
        
        public void SetSFXVolume(float volume)
        {
            sfxVolume = volume;
        }
        
        public void SetMusicVolume(float volume)
        {
            musicVolume = volume;
        }
        
        public void SetPlayerName(string name)
        {
            playerName = name;
        }
    }
}