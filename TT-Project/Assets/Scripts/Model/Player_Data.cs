[System.Serializable]
public class MemoRandomScore
{
    public int best_score;
}
[System.Serializable]
public class SequenceMem_score
{
    public int best_score;
}
[System.Serializable]
public class Reverse_score
{
    public int best_score;
}
[System.Serializable]
public class Mix_score
{
    public int best_score;
}
[System.Serializable]
public class Player_Data
{
    public string username;
    public MemoRandomScore MemoRandom_Score;
    public SequenceMem_score SequenceMem_Score;
    public Reverse_score Reverse_Score;
    public Mix_score Mix_Score;
}
