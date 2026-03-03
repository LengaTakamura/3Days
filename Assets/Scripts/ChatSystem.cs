using System.Collections.Generic;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objList = new();
    [SerializeField] private float _interval = 3f;
    [SerializeField] private CanvasGroup _canvasGroup;

    private Queue<GameObject> _activeObjects = new();

    void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        foreach(var obj in _objList)
        {
            var chat = Instantiate(obj,_canvasGroup.transform);
            _activeObjects.Enqueue(chat);
        }
    }

    private void RandomChat()
    {
        var random = UnityEngine.Random.Range(0,_objList.Count);
        var chat = Instantiate(_objList[random],_canvasGroup.transform);
        _activeObjects.Dequeue();
        _activeObjects.Enqueue(chat);
    }



}
