using UnityEngine;
using System.Collections;

[ExecuteInEditMode ()]
public class DebugFPS : SingletonBase<DebugFPS> 
{
	public bool show = true;
	public bool showInEditor = false;
	public bool showAlert = true;
	public float lowLimitFPS = 28f;

	void Start () 
	{
		useGUILayout = false;
	}

#if DEBUG_MODE
	void OnGUI () 
	{
		if ( !show || (!Application.isPlaying && !showInEditor) )
			return;

		float fps = (1F / Time.deltaTime );
		string fpsLabel = fps.ToString ("0.0")+" FPS";

		if ( showAlert ) 
		{
			if(fps < lowLimitFPS) 
			{
				GUI.contentColor = Color.red;
			}
			else 
			{
				GUI.contentColor = Color.white;
			}
		}
		GUI.Label (new Rect (Screen.width-60, 0,100,200),fpsLabel);
	}
#endif
}