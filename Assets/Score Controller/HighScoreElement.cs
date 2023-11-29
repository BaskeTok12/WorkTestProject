using UnityEngine;

namespace Score_Controller
{
    public class HighScoreElement : MonoBehaviour
    {
        public string PlayerName { get; private set; }
        public int Points { get; private set; }

        public HighScoreElement(string name, int points)
        {
            PlayerName = name;
            Points = points;
        }
    }
}
