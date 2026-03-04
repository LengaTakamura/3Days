using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 動く龍
/// </summary>
public class LetterAnim : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image _letterImage;
    [Header("Sprite集")]
    [SerializeField] private Sprite[] _letterSprite;
    [Header("インターバル")]
    [SerializeField] private int _intervalTime;

    private int _index = 0;
    private float _currentTime;

    private void Start()
    {
        _letterImage.sprite = _letterSprite[_index];
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _intervalTime)
        {
            _index++;
            if (_index > _letterSprite.Length - 1)
            {
                _index = 0;
            }
            _letterImage.sprite = _letterSprite[_index];
            _currentTime = 0;
        }
    }
}
