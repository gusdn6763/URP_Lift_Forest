
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

public class ItemLocation : MonoBehaviour
{
	public int radius;
	[HideInInspector] public int itemNum;

	void Update()
	{
		if (Vector3.Distance(transform.position, Player.instance.transform.position) < radius)
		{
			TutorialManager.instance.changeTarget();
		}
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag(Constant.player))
		{
			TutorialManager.instance.changeTarget();
		}
	}
}