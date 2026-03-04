using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int _score = 0;//スコア

    public static ScoreManager Instance { get; private set; }
    public int Score => _score;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

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
