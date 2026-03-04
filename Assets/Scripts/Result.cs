using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private List<int> _count;
    private ScoreManager _score;
    private Image _image;
    [SerializeField] private Text _text;

    public void Start()
    {
        var score = _score.Score;
        _image = GetComponent<Image>();

        if (score > 10)
        {
            _image.sprite = _sprites[0];
            _text.text = score + "great";
        }
        else if (score > 5)
        {
            _image.sprite = _sprites[1];
            _text.text = score + "good";
        }else
        {
            _image.sprite = _sprites[2];
            _text.text = score + "bad";
        }
    }
}