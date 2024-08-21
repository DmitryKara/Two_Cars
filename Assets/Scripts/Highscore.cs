[System.Serializable]
public class HighScore
{
    public string Date;
    public string Time;
    public int Score;

    public HighScore(string date, string time, int score)
    {
        Date = date;
        Time = time;
        Score = score;
    }
}