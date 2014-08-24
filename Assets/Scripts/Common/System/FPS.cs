using UnityEngine;
using System.Collections;

public class FPS : SingletonBase<FPS> 
{
	[SerializeField]
	int frameRate = 60;

	protected override void OnAwake() 
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = frameRate;
	}
}
