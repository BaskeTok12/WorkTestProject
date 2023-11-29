using System.Collections;
using UnityEngine;

namespace UI.Score
{
    public class DisplayHighScores : MonoBehaviour 
    {
        public TMPro.TextMeshProUGUI[] PlayersNames;
        public TMPro.TextMeshProUGUI[] PlayersScores;
        private HighScores _myScores;

        private void Start() //Fetches the Data at the beginning
        {
            for (int i = 0; i < PlayersNames.Length;i ++)
            {
                PlayersNames[i].text = i + 1 + ". Fetching...";
            }
            _myScores = GetComponent<HighScores>();
            StartCoroutine("RefreshHighscores");
        }
        public void SetScoresToMenu(PlayerScore[] highscoreList) //Assigns proper name and score for each text value
        {
            for (int i = 0; i < PlayersNames.Length;i ++)
            {
                PlayersNames[i].text = i + 1 + ". ";
                if (highscoreList.Length > i)
                {
                    PlayersScores[i].text = highscoreList[i].Score.ToString();
                    PlayersNames[i].text = highscoreList[i].Username;
                }
            }
        }

        private IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
        {
            while(true)
            {
                _myScores.DownloadScores();
                yield return new WaitForSeconds(30);
            }
        }
    }
}
