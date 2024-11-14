using System;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class TimerUI : MonoBehaviour
    {
        private bool _timerActive;
        private float _currentTime;
        [SerializeField] private TMP_Text _text;
        public GameObject timerUI;


        private void Start()
        {
            _currentTime = 0;
        }


        private void Update()
        {
            if (_timerActive)
            {
                _currentTime = _currentTime + Time.deltaTime;
            }

            TimeSpan time = TimeSpan.FromSeconds(_currentTime);
            //_text.text = _currentTime.ToString();
            _text.text = PaddingString(time.Minutes.ToString()) + ":" + PaddingString(time.Seconds.ToString());
            //print(_text.text);
        }

        private string PaddingString(string a)
        {
            if (a.Length == 1)
            {
                return "0" + a;
            }
            return a;
        }

        public void StartTimer()
        {            
            _timerActive = true;
        }
        
        public void StopTimer()
        {
            _timerActive = false;
        }

        public void ShowTimerUI()
        {
            timerUI.SetActive(true);
            StartTimer();
        }
        
        public void UnShowTimerUI()
        {
            timerUI.SetActive(false);
            StopTimer();
        }
        
        public string GetTimeAsString()
        {
            TimeSpan time = TimeSpan.FromSeconds(_currentTime);
            return PaddingString(time.Minutes.ToString()) + ":" + PaddingString(time.Seconds.ToString());
        }
    }

}