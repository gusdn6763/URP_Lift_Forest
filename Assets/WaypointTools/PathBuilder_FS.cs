using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[AddComponentMenu("Filmstorm/WayPointManager")]
public class PathBuilder_FS : MonoBehaviour {
	[Header("Customise Look of Paths")]
	public Color gizmoColor = Color.black;

	//list to hold the gameobject path points
	[Header("Path Points Holder")]
	[Tooltip("This is the list that holds all the Path Points")]
	public List<GameObject> pathPoints = new List<GameObject>(); 
	//the main pathfinder object this script is attached to.
	GameObject pathFinder;
	//the new path point to create
	GameObject pathPoint;



	//Build One Point
	public void BuildPath() {
		
		pathFinder = this.gameObject;
		pathPoint = new GameObject ("pathPointUserCreated");
		pathPoint.transform.parent = pathFinder.transform;
		pathPoint.AddComponent<DrawFilmstormGUI> ();
		pathPoints.Add (pathPoint);

	}

	//Clear All Paths
	public void ClearPath() {

		foreach (GameObject go in pathPoints) {
			DestroyImmediate (go);
		}

		pathPoints.Clear ();
	}





	//What the AI system Accesses to get the paths.
	public List<GameObject> CreatedPaths 
	{
		get { return pathPoints; }
	}





	// Update is called once per frame
	void OnDrawGizmos () {

		for (var i = 1; i < pathPoints.Count; i++) {
			Gizmos.color = gizmoColor;
			Gizmos.DrawLine (pathPoints [i - 1].transform.position, pathPoints [i].transform.position);

		}
}



}
