namespace TurnTheGameOn.ArrowWaypointer
{ 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    [ExecuteInEditMode]
    public class TutorialManager : MonoBehaviour
    {
        public enum Switch { Off, On }

        [System.Serializable]
        public class ItemComponents
        {
            public string ItemName = "Item Name";
            public ItemLocation itemLocation;
            public UnityEvent ItemLocationEvent;
        }

        public Transform player;
        public Switch configureMode;

        [Range(1, 20)] 
        public float arrowTargetSmooth;             // 화살표 방향 전환 부드럽기 조정.
        [Range(1, 100)] 
        public int TotalWaypoints;                  // 위치 갯수 조정.

        public ItemComponents[] itemComponents;
        private GameObject newItem;
        private string newItemName;
        private int nextItem;
        private Transform ItemArrow; //Transform used to reference the Waypoint Arrow
        private Transform currentItemPoint; //Transforms used to identify the Waypoint Arrow's target
        private Transform arrowTarget;

        void Start()
        {
            if (Application.isPlaying)
            {
                GameObject newObject = new GameObject();
                newObject.name = "Arrow Target";
                newObject.transform.parent = gameObject.transform;
                arrowTarget = newObject.transform;
                newObject = null;
            }
            nextItem = 0;
            changeTarget();
        }
        [ContextMenu("Reset")]
        public void Reset()
        {
            nextItem = 0;
            changeTarget();
        }

        void Update()
        {
            if (configureMode == Switch.Off)
            {
                TotalWaypoints = itemComponents.Length;
            }
    #if UNITY_EDITOR
            if (configureMode == Switch.On)
            {
                CalculateWaypoints();
            }
    #endif
            if (arrowTarget != null)
            {
                arrowTarget.localPosition = Vector3.Lerp(arrowTarget.localPosition, currentItemPoint.localPosition, arrowTargetSmooth * Time.deltaTime);
                arrowTarget.localRotation = Quaternion.Lerp(arrowTarget.localRotation, currentItemPoint.localRotation, arrowTargetSmooth * Time.deltaTime);
            }
            else
            {
                arrowTarget = currentItemPoint;
            }
            if (ItemArrow == null)
                findArrow();
            ItemArrow.LookAt(arrowTarget);
        }

        public void ItemEvent(int itemEvent)
        {
            itemComponents[itemEvent - 1].ItemLocationEvent.Invoke();
        }
        public void changeTarget()
        {
            int check = nextItem;
            if (check < TotalWaypoints)
            {
                if (currentItemPoint == null)
                    currentItemPoint = itemComponents[0].itemLocation.transform;
                currentItemPoint.gameObject.SetActive(false);
                currentItemPoint = itemComponents[nextItem].itemLocation.transform;
                currentItemPoint.gameObject.SetActive(true);
                nextItem += 1;
            }
            if (check == TotalWaypoints)
            {
                Destroy(ItemArrow.gameObject);
                Destroy(gameObject);
            }
        }
        public void createArrow()
        {
            GameObject instance = Instantiate(Resources.Load("Arrow", typeof(GameObject))) as GameObject;
            instance.name = "Arrow";
            instance = null;
        }
        public void findArrow()
        {
            GameObject Arrow = GameObject.Find("Arrow");
            if (Arrow == null)
            {
                createArrow();
                ItemArrow = GameObject.Find("Arrow").transform;
            }
            else
            {
                ItemArrow = Arrow.transform;
            }
        }
        public void CalculateWaypoints()
        {
            if (configureMode == Switch.On)
            {
                System.Array.Resize(ref itemComponents, TotalWaypoints);

                if (ItemArrow == null)
                {
                    findArrow();
                }
                for (var i = 0; i < TotalWaypoints; i++)
                {
                    if (itemComponents[i] != null && itemComponents[i].itemLocation == null)
                    {
                        newItemName = "ItemLocation " + (i + 1);
                        itemComponents[i].ItemName = newItemName;
                        //setup waypoint reference
                        foreach (Transform child in transform)
                        {
                            if (child.name == newItemName) { itemComponents[i].itemLocation = child.GetComponent<ItemLocation>(); }
                        }
                        if (itemComponents[i].itemLocation == null)
                        {
                            newItem = Instantiate(Resources.Load<GameObject>("ItemLocation")) as GameObject;
                            newItem.name = newItemName;
                            newItem.GetComponent<ItemLocation>().itemNum = i + 1;
                            newItem.transform.parent = gameObject.transform;
                            itemComponents[i].itemLocation = newItem.GetComponent<ItemLocation>();
                            itemComponents[i].itemLocation.TutoMgr = this;
                            Debug.Log("Next ItemLocation : " + newItemName);
                        }
                        currentItemPoint = itemComponents[0].itemLocation.transform;
                    }
                }
                cleanUpWaypoints();
            }
        }
        public void cleanUpWaypoints()
        {
            if (configureMode == Switch.On)
            {
                if (Application.isPlaying)
                {
                    Debug.LogWarning("ARROW WAYPOINTER: Turn Off 'Configure Mode' on the Waypoint Controller");
                }
                if (transform.childCount > itemComponents.Length)
                {
                    foreach (Transform oldChild in transform)
                    {
                        if (oldChild.GetComponent<ItemLocation>().itemNum > itemComponents.Length)
                        {
                            DestroyImmediate(oldChild.gameObject);
                        }
                    }
                }
            }
        }
        #if UNITY_EDITOR
        //Draws a Gizmo in the scene view window to show the Waypoints
        public void OnDrawGizmosSelected(int radius)
        {
            for (var i = 0; i < itemComponents.Length; i++)
            {
                if (itemComponents[i] != null)
                {
                    if (itemComponents[i].itemLocation != null)
                    {
                        Gizmos.DrawWireSphere(itemComponents[i].itemLocation.transform.position, itemComponents[i].itemLocation.radius);
                        //Gizmos.DrawCube(itemComponents[i].itemLocation.transform.position, itemComponents[i].itemLocation.radius);
                    }
                }
            }
        }
        #endif
    }
}