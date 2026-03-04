using UnityEngine;

/// <summary>
/// 画像開閉
/// </summary>
public class ImproveTitle : MonoBehaviour
{
    [Header("右に動くか判定")]
    [SerializeField] private bool _isRight;
    [Header("動く速さ")]
    [SerializeField] private int _moveSpeed;
    [SerializeField] private bool _isClicked = false;//クリックされたらtrueにする
    //private bool _isMove = false;//マウスが押されたらtrueにする

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isClicked)
        {
            //マウスが押されたら
            _isClicked = true;
            //シーンを切り替える
            SceneController.instance.OnClickFadeIn("InGame");
        }
        
    }
}
