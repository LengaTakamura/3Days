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

    private bool _isMove = false;//マウスが押されたらtrueにする

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isMove)
        {
            _isMove = true;     
        }

        if (_isRight && _isMove)
        {
            this.transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (!_isRight && _isMove)
        {
            this.transform.Translate(-_moveSpeed * Time.deltaTime, 0, 0);
        }
    }
}
