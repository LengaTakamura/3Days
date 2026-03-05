using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // スコア本体
    private static int _score = 0;

    // どこからでもアクセスできるようにするためのインスタンス
    public static ScoreManager Instance { get; private set; }

    public int Score => _score;

    void Awake()
    {
        // シングルトンの重複チェック
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // シーンを跨いでもスコアを保持する
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// スタンプ成功時のスコア加算
    /// </summary>
    public void AddScoreStamp(int amount)
    {
       _score += amount;
    }

    /// <summary>
    /// チャット（スパチャなど）でのスコア加算
    /// </summary>
    /// <param name="amount">加算量</param>
    public void AddScoreChat(int amount)
    {
        _score += amount;
    }

    /// <summary>
    /// 失敗時のスコア減算
    /// </summary>
    public void DecreaseScore(int amount)
    {
        _score -= amount;
    }
}