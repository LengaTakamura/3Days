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
    [SerializeField] private float _superChatFinishTime;
    [Header("顔グラを表示するImage")]
    [SerializeField] private Image _graphicImage;
    [Header("SpriteのList")]
    [SerializeField] private Sprite[] _sprite;
    [Header("ScoreManager")]
    [SerializeField] private ScoreManager _scoreManager;

    private bool _isChatTime = false;
    private bool _isChatTimeFinish = false;
    private bool _isFinish = false;//一回だけGameFinishを呼ぶ
    private bool _isStart = false;//ゲームが始まったらtrueにする
    private Queue<float> _superChatQueue = new Queue<float>();//_superChatTimesListを入れる
    private float _superChatTime = 0;
    private float _limitTimeMax = 0;
    private float _plusTime = 0;
    private InstructionStamp _instructionStamp;

    private Action _onStart;//連打が始まったら呼ぶ
    private Action _onEnd;//連打が終わったら呼ぶ

    private void OnEnable()
    {
        //_onStart+=
        //_onEnd+=
    }

    private void OnDisable()
    {
        //_onStart-=
        //_onEnd-=
    }

    private void Start()
    {
        _limitTimeMax = _limitTime;
        for (int i = 0; i < _superChatTimesList.Count; i++)
        {
            _superChatQueue.Enqueue(_superChatTimesList[i]);
        }

        if (_superChatQueue.Count >= 1)
        {
            _superChatTime = _superChatQueue.Peek();
        }

        Invoke("GameStart", _untilStartTime);
    }

    private void Update()
    {
        if (!_isStart)
        {
            return;
        }

        //時間計測
        _limitTime -= Time.deltaTime;
        if (_limitTime < 0 && !_isFinish)
        {
            _isFinish = true;
            GameFinish();
        }

        //Slider操作
        _plusTime += Time.deltaTime;
        ConvertTime(_plusTime);

        //スパチャの時間になったら呼ぶ
        if (_limitTime <= _superChatTime && !_isChatTime)
        {
            _isChatTime = true;
            SuperChatTimerStart();
            _superChatFinishTime -= Time.deltaTime;
            if (_superChatFinishTime < 0 && !_isChatTimeFinish)
            {
                _isChatTimeFinish = true;
                SuperChatTimeFinish();
            }
        }
    }

    private void ConvertTime(float currentTime)
    {
        float convertLimitTime = Mathf.Clamp01(_limitTimeMax);
        float convertPlusTime = Mathf.Clamp01(currentTime);
        float ratio = convertPlusTime / convertLimitTime;
        _timeSlider.value = Mathf.Lerp(convertPlusTime, convertLimitTime, ratio);
    }

    /// <summary>
    /// ゲームがスタートしたら呼ぶ
    /// </summary>
    private void GameStart()
    {
        Display();
        _isStart = true;
    }

    /// <summary>
    /// ゲームが終了したら呼ぶ
    /// </summary>
    private void GameFinish()
    {

    }

    /// <summary>
    /// スパチャの時のInputを呼ぶ
    /// </summary>
    private void SuperChatTimerStart()
    {
        if (_superChatQueue.Count >= 1)
        {
            ChangeState(InstructionStamp.None, _sprite[0]);
            _superChatQueue.Dequeue();
            _superChatTime = _superChatQueue.Peek();
        }
    }

    /// <summary>
    /// スパチャタイムが終わったら呼ぶ
    /// </summary>
    private void SuperChatTimeFinish()
    {
        if (_superChatQueue.Count >= 1)
        {
            Display();
            _isChatTime = false;
        }
    }

    /// <summary>
    /// 顔グラの表示切り替え
    /// </summary>
    private void Display()
    {
        InstructionStamp[] instructionStampArray = (InstructionStamp[])Enum.GetValues(typeof(InstructionStamp));//顔グラを選ぶ
        InstructionStamp currentStamp = instructionStampArray[UnityEngine.Random.Range(1, instructionStampArray.Length)];//現在の顔グラを記録    
        if (currentStamp == InstructionStamp.TypeA)
        {
            ChangeState(InstructionStamp.TypeA, _sprite[1]);
        }
        else if (currentStamp == InstructionStamp.TypeB)
        {
            ChangeState(InstructionStamp.TypeB, _sprite[2]);
        }
        else if (currentStamp == InstructionStamp.TypeC)
        {
            ChangeState(InstructionStamp.TypeC, _sprite[3]);
        }
    }

    /// <summary>
    /// 状態とグラフィックを切り替える用
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="spr"></param>
    private void ChangeState(InstructionStamp ins, Sprite spr)
    {
        _instructionStamp = ins;
        _graphicImage.sprite = spr;
    }

    /// <summary>
    /// スタンプの正誤判定
    /// </summary>
    private void JudgeState(InstructionStamp ins)
    {
        bool result = ins == _instructionStamp ? true : false;
        if (result)
        {
            _scoreManager.AddScoreStamp();
        }
        else
        {
            _scoreManager.DecreaseScore();
        }
    }
}

/// <summary>
/// スタンプの状態分け
/// </summary>
public enum InstructionStamp
{
    None,
    TypeA,
    TypeB,
    TypeC,
}
