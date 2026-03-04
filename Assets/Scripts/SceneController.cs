using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuracion = 1.0f;//フェードインまでにかかる時間
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
    
    //フェードイン
    private void Start()
    {
        fadePanel.enabled = false;
    }
    
    public void OnClickFadeIn(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        fadePanel.enabled = true;
        
        //経過時間
        float elapsedTime = 0.0f;
        
        //フェードイン開始時の色（通常は透明）
        Color startColor = fadePanel.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);
        while (elapsedTime < fadeDuracion)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuracion);

            fadePanel.color = Color.Lerp(startColor, endColor, alpha);

            yield return null;
        }

        fadePanel.color = endColor;
        
        //フェードインが完了した後にシーンをロードするときはここで実行
        LoadScene(sceneName); 
    }
    
    // シーン遷移用のメソッドですわよ🎮
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
