using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [Header("ゲームがスタートするまでの秒数")]
    [SerializeField] private int _untilStartTime = 5;
    [Header("制限時間")]
    [SerializeField] private float _limitTime;
    [Header("制限時間のSlider")]
    [SerializeField] private Slider _timeSlider;
    [Header("スパチャまでの時間")]
    [SerializeField] private List<float> _superChatTimesList = new List<float>();
    [Header("スパチャする時間")]
    [SerializeField] private float _superChatDuration = 5.0f; // スパチャの継続時間
    [Header("顔グラを表示するImage")]
    [SerializeField] private Image _graphicImage;
    [Header("SpriteのList")]
    [SerializeField] private Sprite[] _sprite;
    [Header("ScoreManager")]
    [SerializeField] private ScoreManager _scoreManager;

    private bool _isChatTime = false;
    private bool _isFinish = false;
    private bool _isStart = false;
    private Queue<float> _superChatQueue = new Queue<float>();
    private float _superChatTime = -1.0f;
    private float _superChatTimer = 0; // カウントダウン用
    private float _limitTimeMax = 0;
    private float _plusTime = 0;
    private InstructionStamp _instructionStamp;

    private void Start()
    {
        _limitTimeMax = _limitTime;
        
        // ListからQueueへコピー
        foreach (float t in _superChatTimesList)
        {
            _superChatQueue.Enqueue(t);
        }

        if (_superChatQueue.Count > 0)
        {
            _superChatTime = _superChatQueue.Peek();
        }

        Invoke("GameStart", _untilStartTime);
    }

    private void Update()
    {
        if (!_isStart || _isFinish) return;

        // 全体の時間計測
        _limitTime -= Time.deltaTime;
        if (_limitTime < 0 && !_isFinish)
        {
            _isFinish = true;
            GameFinish();
        }

        // Slider操作
        _plusTime += Time.deltaTime;
        ConvertTime(_plusTime);

        // スパチャの発生チェック
        if (!_isChatTime && _limitTime <= _superChatTime)
        {
            SuperChatTimerStart();
        }

        // スパチャ中のカウントダウン処理
        if (_isChatTime)
        {
            _superChatTimer -= Time.deltaTime;
            if (_superChatTimer <= 0)
            {
                SuperChatTimeFinish();
            }
        }
    }

    private void ConvertTime(float currentTime)
    {
        if (_timeSlider != null)
        {
            float ratio = Mathf.Clamp01(currentTime / _limitTimeMax);
            _timeSlider.value = ratio;
        }
    }

    private void GameStart()
    {
        Display();
        _isStart = true;
    }

    private void GameFinish() { /* 終了処理 */ }

    private void SuperChatTimerStart()
    {
        if (_superChatQueue.Count > 0)
        {
            _isChatTime = true;
            _superChatTimer = _superChatDuration; // 5秒セット
            
            ChangeState(InstructionStamp.None, _sprite[0]);
            _superChatQueue.Dequeue(); // 現在の時間を消費
            
            // 次のスパチャ時間をセット
            _superChatTime = (_superChatQueue.Count > 0) ? _superChatQueue.Peek() : -1.0f;
        }
    }

    private void SuperChatTimeFinish()
    {
        _isChatTime = false;
        Display(); // 次のスタンプを表示
    }

    private void Display()
    {
        InstructionStamp[] instructionStampArray = (InstructionStamp[])Enum.GetValues(typeof(InstructionStamp));
        InstructionStamp currentStamp = instructionStampArray[UnityEngine.Random.Range(1, instructionStampArray.Length)];
        
        int spriteIndex = (int)currentStamp; // EnumとSpriteの順番が合っている前提
        if (spriteIndex < _sprite.Length)
        {
            ChangeState(currentStamp, _sprite[spriteIndex]);
        }
    }

    private void ChangeState(InstructionStamp ins, Sprite spr)
    {
        _instructionStamp = ins;
        if (_graphicImage != null) _graphicImage.sprite = spr;
    }
}