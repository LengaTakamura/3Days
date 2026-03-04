using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// てきとーにタイトル
/// </summary>
public class Title : MonoBehaviour
{
    [Header("カーソル")]
    [SerializeField] private Image[] _titleCursor;

    private int _currentIndex = 0;//現在どこにいるかを記録

    private void Start()
    {
        foreach (var item in _titleCursor)
        {
            item.gameObject.SetActive(false);
        }
        _titleCursor[0].gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DownKey();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpKey();
        }
    }

    /// <summary>
    /// 下キーを押した時
    /// </summary>
    private void DownKey()
    {
        if (_currentIndex >= _titleCursor.Count() - 1)
        {
            return;
        }
        else
        {
            _currentIndex++;
            _titleCursor[_currentIndex].gameObject.SetActive(true);
            _titleCursor[_currentIndex - 1].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 上キーを押した時
    /// </summary>
    private void UpKey()
    {
        if (_currentIndex <= 0)
        {
            return;
        }
        else
        {
            _currentIndex--;
            _titleCursor[_currentIndex].gameObject.SetActive(true);
            _titleCursor[_currentIndex + 1].gameObject.SetActive(false);
        }
    }
}
