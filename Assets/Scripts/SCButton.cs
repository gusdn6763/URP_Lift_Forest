using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SCButton : MonoBehaviour
{
    private Text title;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        title = GetComponentInChildren<Text>();
    }


    // 버튼에 타이틀 변경 함수
    public void SetTitle(string title)
    {
        this.title.text = title;
    }

    // 버튼의 OnClick 이벤트 변경 함수
    public void SetOnClickAction(UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }

    /// <summary>
    /// 상호작용을 할지말지 결정하는 함수
    /// </summary>
    /// <param name="value"></param>
    public void SetInteractable(bool value)
    {
        button.interactable = value;
    }
}
