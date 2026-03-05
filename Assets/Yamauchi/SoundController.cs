using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] private AudioSource _audioSource;
    [Header("ê≥âÇÃâπ")]
    [SerializeField]private AudioClip _correctClip;
    [Header("ïsê≥âÇÃâπ")]
    [SerializeField] private AudioClip _incorrectClip;

    public void RingSound(bool correct)
    {
        if(correct)
        {
            _audioSource.PlayOneShot(_correctClip);
        }
        else
        {
            _audioSource.PlayOneShot(_incorrectClip);
        }
    }
}
