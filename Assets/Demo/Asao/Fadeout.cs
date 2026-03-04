using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class Fadeout : MonoBehaviour
{//フェードインのスクリプト
    public Image fadePanel;
    public float fadeDuracion = 1.0f;//フェードインまでにかかる時間

    private void Start()
    {
        fadePanel.enabled = false;
    }
    public void OnClickFadeIn()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    public IEnumerator FadeOutAndLoadScene()
    {
        fadePanel.enabled = true;

        float elapsedTime = 0.0f;//経過時間

        Color startColor = fadePanel.color;//フェードイン開始時の色（通常は透明）
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);
        while (elapsedTime < fadeDuracion)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuracion);

            fadePanel.color = Color.Lerp(startColor, endColor, alpha);

            yield return null;
        }

        fadePanel.color = endColor;

       // SceneManager.LoadScene("Menu");
    }
}
