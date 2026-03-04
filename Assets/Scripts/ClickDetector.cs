using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private void Update()
    {
        // 画面のどこかを押したら遷移する
        if (Input.GetMouseButtonDown(0))
        {
            SceneController.instance.LoadScene(_sceneName);
            Debug.Log("タイトルに遷移");
        }
    }
}
