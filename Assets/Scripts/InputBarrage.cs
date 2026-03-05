using System;
using UnityEngine;

public class InputBarrage : MonoBehaviour
{
    // クリック回数の初期値
    private int _count = 0;

    // 連打できるかどうか
    private bool _canPress = false;
    //private int _totalScore = 0;
    
    public Action OnClick;

    void Start()
    {
        _canPress = false;
    }

    void Update()
    {
        if (_canPress && Input.GetMouseButtonDown(0))
        {
            _count++;
        }
    }

    /// <summary>
    /// スパチャタイムを開始・連打可能にする
    /// </summary>
    public void OnStart()
    {
        _canPress = true;
        _count = 0;
    }

    /// <summary>
    /// スパチャタイムを終了・連打不可能にする
    /// スコアをScoreManagerに渡す
    /// </summary>
    public int OnEnd()
    {
        _canPress = false;
        // ScoreManager.Instance.AddScore(_count);
        return _count;
    }

    public void OnClicked()
    {
        if (_canPress)
        {
            OnClick?.Invoke();
            _count++;
        }
    }
}