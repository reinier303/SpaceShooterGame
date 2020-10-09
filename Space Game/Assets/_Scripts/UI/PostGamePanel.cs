using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceGame
{
    public class PostGamePanel : MonoBehaviour
    {
        public TMP_Text UnitsEarnedText;
        public TMP_Text ExperienceEarnedText;
        public TMP_Text RareDropsFoundText;
        public TMP_Text EnemiesKilledText;
        public TMP_Text BossesKilledText;
        public TMP_Text RunTimeSecondsText;
        public TMP_Text KilledByText;

        public float UnitsEarned;
        public float ExperienceEarned;
        public int RareDropsFound;
        public float EnemiesKilled;
        public float BossesKilled;
        public float RunTimeSeconds;
        public string KilledBy;

        public void UpdateSummary()
        {
            UnitsEarnedText.text = "Units Earned:\n" + UnitsEarned;
            ExperienceEarnedText.text = "Experience Gained:\n" + UnitsEarned;
            RareDropsFoundText.text = "Rare Drops Found:\n" + RareDropsFound;
            EnemiesKilledText.text = "Enemies Killed:\n" + EnemiesKilled;
            BossesKilledText.text = "Bosses Killed:\n" + BossesKilled;
            RunTimeSecondsText.text = "Run Time:\n" + ConvertToTime(RunTimeSeconds);
            KilledByText.text = "Killed By:\n" + KilledBy;

        }

        private string ConvertToTime(float totalSeconds)
        {
            int hours;
            int minutes;
            int seconds;
            hours = (int)totalSeconds / 3600;
            minutes = (int)(totalSeconds % 3600) / 60;
            seconds = (int)(totalSeconds % 3600) % 60;

            return hours + ":" + minutes + ":" + seconds;
        }
    }
}