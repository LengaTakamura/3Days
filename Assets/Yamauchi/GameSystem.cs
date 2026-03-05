using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームマネージャーです
/// セリフ、顔グラ、吹き出しのSpriteリストの0番目にはスパチャ時のものを入れてください
/// 吹き出しのSpriteリストの1番目には普段のものを入れてください
/// 対応するセリフ、顔グラのSpriteリストのインデックスは同じ数になるようにしてください
/// </summary>
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
    [SerializeField] private Image _faceGraphicImage;
    [Header("顔グラのList")]
    [SerializeField] private Sprite[] _faceSprite;
    [Header("吹き出しを表示するImage")]
    [SerializeField] private Image _speechBallonImage;
    [Header("吹き出しを表示するSprite")]
    [SerializeField] private Sprite[] _speechBallonSprite;
    [Header("セリフ表示用Text")]
    [SerializeField] private Text _dialogueText;
    [Header("セリフを入れる文字列")]
    [SerializeField] private string[] _dialogueString;
    [Header("スコア表示用Text")]
    [SerializeField] private Text _scoreText;
    [Header("ButtonのList")]
    [SerializeField] private Button[] _buttonList;
    [Header("スパチャボタン")]
    [SerializeField] private Button _superChatButton;
    [Header("InputStamp")]
    [SerializeField] private InputStamp _inputStamp;
    [Header("InputBarrage")]
    [SerializeField] private InputBarrage _inputBarrage;
    [Header("SoundController")]
    [SerializeField] private SoundController _soundController;

    private bool _isChatTime = false;
    private bool _isFinish = false;//一回だけGameFinishを呼ぶ
    private bool _isStart = false;//ゲームが始まったらtrueにする
    private Queue<float> _superChatQueue = new Queue<float>();//_superChatTimesListを入れる
    private float _superChatTime = 0;
    private float _limitTimeMax = 0;
    private float _plusTime = 0;
    private InstructionStamp _instructionStamp;

    private Action _onStart;//連打が始まったら呼ぶ
    private Func<int> _onEnd;//連打が終わったら呼ぶ

    private void OnEnable()
    {
        _inputStamp.OnStampClicked += JudgeState;
        _onStart += _inputBarrage.OnStart;
        _onEnd += _inputBarrage.OnEnd;
    }

    private void OnDisable()
    {
        _inputStamp.OnStampClicked -= JudgeState;
        _onStart -= _inputBarrage.OnStart;
        _onEnd -= _inputBarrage.OnEnd;
    }

    private void Start()
    {
        //時間設定
        _limitTimeMax = _limitTime;
        for (int i = 0; i < _superChatTimesList.Count; i++)
        {
            _superChatQueue.Enqueue(_superChatTimesList[i]);
        }

        if (_superChatQueue.Count >= 1)
        {
            _superChatTime = _superChatQueue.Peek();
        }

        //UI設定
        foreach (var b in _buttonList)
        {
            b.interactable = false;
        }
        _superChatButton.interactable = false;
        _speechBallonImage.sprite = _speechBallonSprite[1];
        _dialogueText.text = _dialogueString[4];
        _faceGraphicImage.sprite = _faceSprite[4];//4番に普通の表情が入る？
        _scoreText.text = ScoreManager.Instance.Score.ToString();

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
            StartCoroutine(SuperChatTimeWait());
        }
    }

    IEnumerator SuperChatTimeWait()
    {
        yield return new WaitForSeconds(_superChatFinishTime);
        SuperChatTimeFinish();
    }

    private void ConvertTime(float currentTime)
    {
        float ratio = Mathf.Clamp01(currentTime / _limitTimeMax);
        _timeSlider.value = ratio;
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
        if (_superChatQueue.Count > 0)
        {
            foreach (var button in _buttonList)
            {
                button.interactable = false;
            }
            _superChatButton.interactable = true;

            _onStart?.Invoke();
            _speechBallonImage.sprite = _speechBallonSprite[0];//スパチャの時の吹き出し
            ChangeState(InstructionStamp.Chat, _faceSprite[0]);
            ChangeDialogue(_dialogueString[0]);
            _superChatQueue.Dequeue();
            if (_superChatQueue.Count == 0) return;
            _superChatTime = _superChatQueue.Peek();
        }
    }

    /// <summary>
    /// スパチャタイムが終わったら呼ぶ
    /// </summary>
    private void SuperChatTimeFinish()
    {
        _onEnd?.Invoke();
        Display();
        _isChatTime = false;
    }

    /// <summary>
    /// 顔グラの表示とセリフ切り替え
    /// </summary>
    private void Display()
    {
        foreach (var button in _buttonList)
        {
            button.interactable = true;
        }
        _superChatButton.interactable = false;

        _speechBallonImage.sprite = _speechBallonSprite[1];//普段の吹き出し
        InstructionStamp[] instructionStampArray = (InstructionStamp[])Enum.GetValues(typeof(InstructionStamp));//顔グラを選ぶ
        InstructionStamp currentStamp = instructionStampArray[UnityEngine.Random.Range(1, instructionStampArray.Length)];//現在の顔グラを記録    
        if (currentStamp == InstructionStamp.TypeA)
        {
            ChangeState(InstructionStamp.TypeA, _faceSprite[1]);
            ChangeDialogue(_dialogueString[1]);
        }
        else if (currentStamp == InstructionStamp.TypeB)
        {
            ChangeState(InstructionStamp.TypeB, _faceSprite[2]);
            ChangeDialogue(_dialogueString[2]);
        }
        else if (currentStamp == InstructionStamp.TypeC)
        {
            ChangeState(InstructionStamp.TypeC, _faceSprite[3]);
            ChangeDialogue(_dialogueString[3]);
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
        _faceGraphicImage.sprite = spr;
    }

    private void ChangeDialogue(string dialogue)
    {
        _dialogueText.text = dialogue;
    }

    /// <summary>
    /// スタンプの正誤判定
    /// </summary>
    private void JudgeState(InstructionStamp ins)
    {

        bool result = ins == _instructionStamp ? true : false;
        if (result)
        {
            _soundController.RingSound(true);
            ScoreManager.Instance.AddScoreStamp();
            _scoreText.text = ScoreManager.Instance.Score.ToString();
        }
        else
        {
            _soundController.RingSound(false);
            ScoreManager.Instance.DecreaseScore();
            _scoreText.text = ScoreManager.Instance.Score.ToString();
        }
        Display();
    }
}

/// <summary>
/// スタンプの状態分け
/// </summary>
public enum InstructionStamp
{
    Chat,
    TypeA,
    TypeB,
    TypeC,
}
