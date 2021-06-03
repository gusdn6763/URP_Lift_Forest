using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathBuilder_FS))]
public class PathBuilderEditor_FS : Editor {


	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		PathBuilder_FS myScript = (PathBuilder_FS)target;



		if(GUILayout.Button("Build Path Point"))
		{
			myScript.BuildPath();
		}

		if (GUILayout.Button ("Clear All Paths")) 
		{
			myScript.ClearPath ();

		}



	}



}
