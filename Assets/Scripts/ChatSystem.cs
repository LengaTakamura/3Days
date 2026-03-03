using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objList = new();
    [SerializeField] private float _interval = 3f;
    [SerializeField] private VerticalLayoutGroup _layoutGroup;

    private Queue<GameObject> _activeObjects = new();

    void OnEnable()
    {
        Init();
        UpdateLayOutGroup();
    }

    private void Init()
    {
        foreach (var obj in _objList)
        {
            var chat = Instantiate(obj, _layoutGroup.transform);
            _activeObjects.Enqueue(chat);
        }
    }

    public void OnGameStart()
    {
        StartCoroutine(Chat());
    }

    private IEnumerator Chat()
    {
        while (true)
        {
            yield return new WaitForSeconds(_interval);
            UpdateChat();
        }
    }



    private void UpdateChat()
    {
        var random = UnityEngine.Random.Range(0, _objList.Count);
        var chat = Instantiate(_objList[random], _layoutGroup.transform);
        var oldChat = _activeObjects.Dequeue();
        Destroy(oldChat.gameObject);
        _activeObjects.Enqueue(chat);
        UpdateLayOutGroup();
    }

    private void UpdateLayOutGroup()
    {
        // レイアウト内の入力値を再計算
        _layoutGroup.CalculateLayoutInputHorizontal();

        // レイアウト再設定(表示更新)
        _layoutGroup.SetLayoutHorizontal();
    }



}
