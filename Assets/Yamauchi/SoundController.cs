using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("効果音用AudioSource")]
    [SerializeField] private AudioSource _audioSource;
    [Header("通常用AudioSource")]
    [SerializeField] private AudioSource _normalAudioSource;
    [Header("スパチャ用AudioSource")]
    [SerializeField] private AudioSource _superChatAudioSource;
    [Header("正解の音")]
    [SerializeField] private AudioClip _correctClip;
    [Header("不正解の音")]
    [SerializeField] private AudioClip _incorrectClip;
    [Header("通常音楽")]
    [SerializeField] private AudioClip _normalClip;
    [Header("スパチャ時音楽")]
    [SerializeField] private AudioClip _superChatClip;

    public void RingSound(bool correct)
    {
        if (correct)
        {
            _audioSource.PlayOneShot(_correctClip);
        }
        else
        {
            _audioSource.PlayOneShot(_incorrectClip);
        }
    }

    public void FlowSound(string state)
    {
        if (state == "normal")
        {
            _superChatAudioSource.Stop();
            _normalAudioSource.Play();
        }
        else if (state == "superChat")
        {
            _normalAudioSource.Stop();
            _superChatAudioSource.Play();
        }
    }
}
