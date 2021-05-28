using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TalkInformation
{
    public float minDemandRelation;       //�� ��ȭ�� ���÷��� �ּ� ģ�а� �䱸ġ -> currentRealtion���� minRelation������ ������ �� ���� ���� ����
    public float maxDemandRelation;       //�� ��ȭ�� ������ �ʴ� �ִ� ģ�а� �䱸ġ -> currentRealtion����  maxRelation������ ������ �� ���� �������� 
    public string talk;             //NPC�� �ϴ� ��ȭ
}


/// <summary>
/// NPC�� ģ�е��� �ִ�. ģ�е��� ���� NPC ��ȭ�� �ٲ�� ���� �����̴�.
/// </summary>
public class Relationship : MonoBehaviour
{
    [SerializeField] private List<TalkInformation> talking;
    [SerializeField] private Text text;

    [SerializeField] float currentRealtion;           //NPC�� ���� ģ�а�
    [SerializeField] float maxRealtion;               //NPC���׼� �ø��� �ִ� �ִ� ģ�а�


    /// <summary>
    /// ��ȭ�� �ϰų� ������ �ָ� ģ�а� ���
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

