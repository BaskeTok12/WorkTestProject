namespace UI.Score
{
    using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

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
        _instance.StartCoroutine(_instance.DatabaseUpload(username, score)); //Calls Instance
        Debug.Log("Uploaded highscore!");
    }

    private IEnumerator DatabaseUpload(string username, int score) //Called when sending new score to Website
    {
        string uploadURL = WebURL + PrivateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score;
        UnityWebRequest www = UnityWebRequest.Get(uploadURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Upload Successful");
            DownloadScores();
        }
        else
        {
            Debug.LogError("Error uploading: " + www.error);
        }
    }

    public void DownloadScores()
    {
        StartCoroutine(DatabaseDownload());
    }

    private IEnumerator DatabaseDownload()
    {
        string downloadURL = WebURL + PublicCode + "/pipe/0/10"; //Gets top 10
        UnityWebRequest www = UnityWebRequest.Get(downloadURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            OrganizeInfo(www.downloadHandler.text);
            _myDisplay.SetScoresToMenu(_scoreList);
        }
        else
        {
            Debug.LogError("Error downloading: " + www.error);
        }
    }

    private void OrganizeInfo(string rawData) //Divides Scoreboard info by new lines
    {
        var entries = rawData.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        _scoreList = new PlayerScore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            _scoreList[i] = new PlayerScore(username, score);
            Debug.Log(_scoreList[i].Username + ": " + _scoreList[i].Score);
        }
    }
}

}