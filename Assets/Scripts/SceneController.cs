using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance {get; private set;}

    private void Awake()
    {
        // 重複の確認してるよ👀　もしあったら...
        if (instance != null)
        {
            Destroy(gameObject); // 消せ！今すぐにだ！消せ！
            return;
        }
        instance = this;
        // シーン遷移しても破棄しないでね
        DontDestroyOnLoad(gameObject);
    }

    // シーン遷移用のメソッドですわよ🎮
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
