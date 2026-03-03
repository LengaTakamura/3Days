using UnityEngine;

public class InputBarrage : MonoBehaviour
{
    public int score = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddScore(10);
        }
    }

    void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"{score}");
    }
}
