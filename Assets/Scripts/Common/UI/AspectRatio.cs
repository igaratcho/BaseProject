using UnityEngine;
using System.Collections;

public class AspectRatio : MonoBehaviour 
{
	public float width;
	public float height;

	void Awake () 
	{
		float baseAspect = height/width;
		float nowAspect = (float)Screen.height/(float)Screen.width;
		float changeAspect;
		
		if( baseAspect > nowAspect )
		{   
			changeAspect = nowAspect/baseAspect;
			camera.rect=new Rect((1-changeAspect)*0.5f,0,changeAspect,1);
		} 
		else
		{
			changeAspect = baseAspect/nowAspect;
			camera.rect=new Rect(0,(1-changeAspect)*0.5f,1,changeAspect);
		}

		Destroy(this);
	}
}
