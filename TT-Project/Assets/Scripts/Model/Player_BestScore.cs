[System.Serializable]
public class Player_BestScore
{
    public string username;
    public int best_score;

    public Player_BestScore()
    {

    }
    public Player_BestScore(string username, int bestScore)
    {
        this.username = username;
        this.best_score = bestScore;
    }
}
[System.Serializable]
public class Player_BestScoreList
{
    public Player_BestScore[] jsonData;
}
