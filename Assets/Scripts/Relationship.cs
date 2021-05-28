using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TalkInformation
{
    public float minDemandRelation;       //이 대화가 나올려는 최소 친밀감 요구치 -> currentRealtion값이 minRelation값보다 낮으면 이 말을 하지 않음
    public float maxDemandRelation;       //이 대화가 나오지 않는 최대 친밀감 요구치 -> currentRealtion값이  maxRelation값보다 높으면 이 말을 하지않음 
    public string talk;             //NPC가 하는 대화
}


/// <summary>
/// NPC는 친밀도가 있다. 친밀도에 따라 NPC 대화가 바뀌도록 만들 예정이다.
/// </summary>
public class Relationship : MonoBehaviour
{
    [SerializeField] private List<TalkInformation> talking;
    [SerializeField] private Text text;

    [SerializeField] float currentRealtion;           //NPC의 현재 친밀감
    [SerializeField] float maxRealtion;               //NPC한테서 올릴수 있는 최대 친밀감


    /// <summary>
    /// 대화를 하거나 선물을 주면 친밀감 상승
    /// </summary>
    /// <param name="value"></param>
    public void GetRealtion(float value)
    {
        currentRealtion += value;
    }

    public void RandomTalking()
    {
        List<TalkInformation> tmp = new List<TalkInformation>();
        for (int i = 0; i < talking.Count; i++)
        {
            if (currentRealtion >= talking[i].minDemandRelation && currentRealtion <= talking[i].maxDemandRelation)
            {
                tmp.Add(talking[i]);
            }
        }
        text.text = tmp[Random.Range(0, tmp.Count)].talk.ToString();
    }
}

