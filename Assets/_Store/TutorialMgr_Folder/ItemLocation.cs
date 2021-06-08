namespace TurnTheGameOn.ArrowWaypointer
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class ItemLocation : MonoBehaviour
	{

		public int radius;
		public TutorialManager TutoMgr;
		[HideInInspector] public int itemNum;

        private void Start()
        {
			TutoMgr = TutorialManager.instance;

		}

        void Update()
		{
			if (TutoMgr.player)
			{
				if (Vector3.Distance(transform.position, TutoMgr.player.position) < radius)
				{
					TutoMgr.changeTarget();
				}
			}
		}

		void OnTriggerEnter(Collider col)
		{
			if (col.gameObject.tag == "Player")
			{
				TutoMgr.ItemEvent(itemNum);
				TutoMgr.changeTarget();
			}
		}

	#if UNITY_EDITOR
			void OnDrawGizmosSelected()
			{
				if (TutoMgr != null)
				{
					TutoMgr.OnDrawGizmosSelected(radius);
				}
			}
	#endif
	}
}

