using UnityEngine;

/// <summary>
/// ‰æ‘œŠJ•Â
/// </summary>
public class ImproveTitle : MonoBehaviour
{
    [Header("‰E‚É“®‚­‚©”»’è")]
    [SerializeField] private bool _isRight;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isRight)
            {
                this.transform.Translate(1, 0, 0);
            }
            else if (!_isRight)
            {
                this.transform.Translate(-1, 0, 0);
            }
        }
    }
}
