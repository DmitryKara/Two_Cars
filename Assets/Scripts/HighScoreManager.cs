using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Serialization<T>
{
    public List<T> data;

    public Serialization(List<T> data)
    {
        this.data = data;
    }

    public List<T> ToList()
    {
        return data;
    }
}

public class HighScoreManager
{
    private static HighScoreManager instance;

    private int hightScoreCount = 10;

    public static HighScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HighScoreManager();
                instance.LoadHighScores();
            }
            return instance;
        }
    }

    public List<HighScore> highScores = new List<HighScore>();
    private const string HighScoreKey = "HighScores";

    private HighScoreManager()
    {

    }

    public void AddScore(int score)
    {
        string currentDate = System.DateTime.Now.ToString("dd-MM-yyyy");
        string currentTime = System.DateTime.Now.ToString("HH:mm:ss");

        HighScore newScore = new HighScore(currentDate, currentTime, score);
        highScores.Add(newScore);
        highScores.Sort((a, b) => b.Score.CompareTo(a.Score));

        if (highScores.Count > hightScoreCount)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        SaveHighScores();
    }

    private void SaveHighScores()
    {
        string json = JsonUtility.ToJson(new Serialization<HighScore>(highScores), true);
        PlayerPrefs.SetString(HighScoreKey, json);
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            string json = PlayerPrefs.GetString(HighScoreKey);
            Serialization<HighScore> data = JsonUtility.FromJson<Serialization<HighScore>>(json);
            highScores = data.ToList();
        }
    }

    public void ResetHighScores()
    {
        highScores.Clear();
        PlayerPrefs.DeleteKey(HighScoreKey);
        PlayerPrefs.Save();
    }
}
