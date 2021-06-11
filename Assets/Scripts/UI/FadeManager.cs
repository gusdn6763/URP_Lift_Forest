using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//페이드 인, 아웃 효과
public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    
    public Action OnAction;

    [SerializeField] private Image black;
    private Color color;

    private void Awake()
    {
        instance = this;
    }

    public void FadeOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(_speed));
    }

    IEnumerator FadeOutCoroutine(float _speed)
    {
        color = black.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            black.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        if(OnAction != null)
        {
            OnAction();
            OnAction = null;
        }
        FadeIn();
    }

    public void FadeIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(_speed));
    }

    IEnumerator FadeInCoroutine(float _speed)
    {
        color = black.color;
        while (color.a > 0f)
        {
            color.a -= _speed;
            black.color = color;
            yield return new WaitForSeconds(0.01f);
        }
    }

}