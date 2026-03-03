using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int _score = 0;//スコア

    public int Score => _score;

    /// <summary>
    /// スタンプ用
    /// </summary>
    public void AddScoreStamp()
    {
        _score++;
    }

    public void AddScoreChat(int amount)
    {
        _score += amount;
    }

    public void DecreaseScore()
    {
        _score--;
    }
}
