using System.Collections.Generic;
using Managers.Data_Management;
using UnityEngine;

namespace Score_Controller
{
    public class HighScoreHandler : MonoBehaviour
    {
        private List<HighScoreElement> _highScoreList = new List<HighScoreElement> ();
        [Header("Score Elements")]
        [SerializeField] private int maxCount = 7;
        [Header("Saving Parameters")]
        [SerializeField] private string filename;

        public delegate void OnHighScoreListChanged (List<HighScoreElement> list);
        
        public static event OnHighScoreListChanged OnHighscoreListChanged;

        private void Start () {
            LoadHighScores ();
        }

        private void LoadHighScores () {
            _highScoreList = FileHandler.ReadListFromJson<HighScoreElement> (filename);

            while (_highScoreList.Count > maxCount) {
                _highScoreList.RemoveAt (maxCount);
            }

            OnHighscoreListChanged?.Invoke (_highScoreList);
        }

        private void SaveHighScore () {
            FileHandler.SaveToJson (_highScoreList, filename);
        }

        public void AddHighScoreIfPossible (HighScoreElement element) {
            for (int i = 0; i < maxCount; i++)
            {
                if (i < _highScoreList.Count && element.Points <= _highScoreList[i].Points) continue;
                
                _highScoreList.Insert (i, element);

                while (_highScoreList.Count > maxCount) {
                    _highScoreList.RemoveAt (maxCount);
                }

                SaveHighScore ();

                OnHighscoreListChanged?.Invoke (_highScoreList);
                Debug.Log(element.Points);
                break;
            }
        }

    }
}