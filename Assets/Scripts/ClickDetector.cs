using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// マウスクリックで画面遷移
/// </summary>
public class ClickDetector : MonoBehaviour
{
    [Header("シーンの名前")]
    [SerializeField] private string _sceneName;
    [Header("フェードにかかる時間")]
    [SerializeField] private float _fadeTime;
    [Header("フェードパネル")]
    [SerializeField] private Image _fade;

    private float _currentTime = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeOut());
            Debug.Log("遷移完了");
        }
    }

    private IEnumerator FadeOut()
    {
        var c = _fade.color;
        while (_currentTime < _fadeTime)
        {
            _currentTime += Time.deltaTime;
            c.a = Mathf.Clamp01(_currentTime / _fadeTime) * 1;
            _fade.color = c;

            yield return null;
        }
        SceneController.instance.LoadScene(_sceneName);
    }
}
