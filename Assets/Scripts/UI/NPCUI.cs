using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCUI : UI
{
    [SerializeField] private Text dialogueTxt;

    protected void Update()
    {
        transform.LookAt(new Vector3(Player.instance.transform.position.x, transform.position.y, Player.instance.transform.position.z));
    }

    public void ShowDialogue(Transform parent, string dialogue)
    {
        dialogueTxt.text = dialogue.ToString();
        transform.position = parent.position + addSize;
    }
}
