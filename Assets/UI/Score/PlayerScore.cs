namespace UI.Score
{
    public struct PlayerScore
    {
        public readonly string Username;
        public readonly int Score;

        public PlayerScore(string username, int score)
        {
            Username = username;
            Score = score;
        }
    }

}