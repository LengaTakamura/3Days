using System;
using UnityEngine;

public class InputStamp : MonoBehaviour
{
    public Action<InstructionStamp> OnStampClicked; // スタンプがクリックされたときのイベント
    private InstructionStamp _input; // 自身のInstructionStampコンポーネント
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TypeA()
    {
        _input = InstructionStamp.TypeA;// 自身のInstructionStampコンポーネントをTypeAに設定
        OnStampClicked?.Invoke(_input); //nullチェックしてスタンプがクリックされた時のイベントを呼ぶ
    }
    public void TypeB()
    {
        _input = InstructionStamp.TypeB;// 自身のInstructionStampコンポーネントをTypeBに設定
        OnStampClicked?.Invoke(_input); //nullチェックしてスタンプがクリックされた時のイベントを呼ぶ
    }
    public void TypeC()
    {
        _input = InstructionStamp.TypeC;
        OnStampClicked?.Invoke(_input);
    }
}
