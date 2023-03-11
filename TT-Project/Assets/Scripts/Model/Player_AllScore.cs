[System.Serializable]
public class Player_AllScore
{
    public string username;
    public int best_score;
    public int recent_score;

    public Player_AllScore()
    {

    }
    public Player_AllScore(string username, int bestScore, int recentSore)
    {
        this.username = username;
        this.best_score = bestScore;
        recent_score = recentSore;
    }
}
[System.Serializable]
public class Player_AllScoreList
{
    public Player_AllScore[] jsonData;
}
