using System.Collections;
using UnityEngine;

namespace UI.Score
{
    public class HighScores : MonoBehaviour
    {
        private const string PrivateCode = "djGLBMqcPEKnPegOyMUm1QlO8ZIwDRkEGNlx2kbrY9mA";  //Key to Upload New Info
        private const string PublicCode = "6566a04f8f40bb1054cf28a1";   //Key to download
        private const string WebURL = "http://dreamlo.com/lb/"; //  Website the keys are for

        private PlayerScore[] _scoreList;
        private DisplayHighScores _myDisplay;

        private static HighScores _instance; //Required for STATIC usability

        private void Awake()
        {
            _instance = this; //Sets Static Instance
            _myDisplay = GetComponent<DisplayHighScores>();
        }
    
        public static void UploadScore(string username, int score)  //CALLED when Uploading new Score to WEBSITE
        {//STATIC to call from other scripts easily
            _instance.StartCoroutine(_instance.DatabaseUpload(username,score)); //Calls Instance
        }

        private IEnumerator DatabaseUpload(string userame, int score) //Called when sending new score to Website
        {
            WWW www = new WWW(WebURL + PrivateCode + "/add/" + WWW.EscapeURL(userame) + "/" + score);
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                print("Upload Successful");
                DownloadScores();
            }
            else print("Error uploading" + www.error);
        }

        public void DownloadScores()
        {
            StartCoroutine("DatabaseDownload");
        }

        private IEnumerator DatabaseDownload()
        {
            //WWW www = new WWW(webURL + publicCode + "/pipe/"); //Gets the whole list
            WWW www = new WWW(WebURL + PublicCode + "/pipe/0/10"); //Gets top 10
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                OrganizeInfo(www.text);
                _myDisplay.SetScoresToMenu(_scoreList);
            }
            else print("Error uploading" + www.error);
        }

        private void OrganizeInfo(string rawData) //Divides Scoreboard info by new lines
        {
            string[] entries = rawData.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
            _scoreList = new PlayerScore[entries.Length];
            for (int i = 0; i < entries.Length; i ++) //For each entry in the string array
            {
                string[] entryInfo = entries[i].Split(new char[] {'|'});
                string username = entryInfo[0];
                int score = int.Parse(entryInfo[1]);
                _scoreList[i] = new PlayerScore(username,score);
                print(_scoreList[i].username + ": " + _scoreList[i].score);
            }
        }
    }

    public struct PlayerScore //Creates place to store the variables for the name and score of each player
    {
        public string username;
        public int score;

        public PlayerScore(string _username, int _score)
        {
            username = _username;
            score = _score;
        }
    }
}