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


    // ��ư�� Ÿ��Ʋ ���� �Լ�
    public void SetTitle(string title)
    {
        this.title.text = title;
    }

    // ��ư�� OnClick �̺�Ʈ ���� �Լ�
    public void SetOnClickAction(UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }

    /// <summary>
    /// ��ȣ�ۿ��� �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="value"></param>
    public void SetInteractable(bool value)
    {
        button.interactable = value;
    }
}
