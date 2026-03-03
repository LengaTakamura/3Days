using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int _score = 0;//ÉXÉRÉA

    public void AddScore()
    {
        _score++;
    }

    public void DecreaseScore()
    {
        _score--;
    }
}
