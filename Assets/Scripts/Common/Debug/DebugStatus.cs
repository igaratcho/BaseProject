using UnityEngine;
using System.Collections;
using System.Text;

[ExecuteInEditMode ()]
public class DebugStatus : SingletonBase<DebugStatus> 
{
	public bool show = true;
	public bool showInEditor = false;

	float lastCollect = 0;
	float lastCollectNum = 0;
	float delta = 0;
	float lastDeltaTime = 0;
	int allocRate = 0;
	int lastAllocMemory = 0;
	float lastAllocSet = -9999;
	int allocMem = 0;
	int collectAlloc = 0;
	int peakAlloc = 0;
	
	void Start () 
	{
		useGUILayout = false;
	}

#if DEBUG_MODE
	void OnGUI ()
	{

		if (!show || (!Application.isPlaying && !showInEditor))
			return;
		
		int collCount = System.GC.CollectionCount (0);
		
		if (lastCollectNum != collCount) {
			lastCollectNum = collCount;
			delta = Time.realtimeSinceStartup-lastCollect;
			lastCollect = Time.realtimeSinceStartup;
			lastDeltaTime = Time.deltaTime;
			collectAlloc = allocMem;
		}
		
		allocMem = (int)System.GC.GetTotalMemory (false);
		
		peakAlloc = allocMem > peakAlloc ? allocMem : peakAlloc;
		
		if (Time.realtimeSinceStartup - lastAllocSet > 0.3F) {
			int diff = allocMem - lastAllocMemory;
			lastAllocMemory = allocMem;
			lastAllocSet = Time.realtimeSinceStartup;
			
			if (diff >= 0) {
				allocRate = diff;
			}
		}
		
		StringBuilder text = new StringBuilder ();

		text.Append ("Currently allocated			");
		text.Append ((allocMem/1000000F).ToString ("0"));
		text.Append ("mb\n");
			
		text.Append ("Peak allocated				");
		text.Append ((peakAlloc/1000000F).ToString ("0"));
		text.Append ("mb (last	collect ");
		text.Append ((collectAlloc/1000000F).ToString ("0"));
		text.Append (" mb)\n");
			
			
		text.Append ("Allocation rate				");
		text.Append ((allocRate/1000000F).ToString ("0.0"));
		text.Append ("mb\n");
			
		text.Append ("Collection frequency		");
		text.Append (delta.ToString ("0.00"));
		text.Append ("s\n");
			
		text.Append ("Last collect delta			");
		text.Append (lastDeltaTime.ToString ("0.000"));
		text.Append ("s (");
		text.Append ((1F/lastDeltaTime).ToString ("0.0"));

		GUI.Box (new Rect (5,5,310,80),"");
		GUI.Label (new Rect (10,5,1000,200),text.ToString ());
	}
#endif
}
