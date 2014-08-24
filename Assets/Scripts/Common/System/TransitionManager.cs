using UnityEngine;
using System.Collections;

public class TransitionManager : SingletonBase<TransitionManager>
{
	[SerializeField]
	float fadeOutInterval = 0f;
	[SerializeField]
	float fadeInInterval = 0f;

	Texture2D blackTexture;
	float fadeAlpha = 0;
	bool isFading = false;

	public static void Transit(Scene scene)
	{
		Instance.StartCoroutine ( Instance.TransitWithFade (scene) );
	}
	
	protected override void OnAwake ()
	{
		// Make Black Texture.
		this.blackTexture = new Texture2D (32, 32, TextureFormat.RGB24, false);
		this.blackTexture.ReadPixels (new Rect (0, 0, 32, 32), 0, 0, false);
		this.blackTexture.SetPixel (0, 0, Color.white);
		this.blackTexture.Apply ();
	}
	
	void OnGUI ()
	{
		if (!this.isFading) return;

		GUI.color = new Color (0, 0, 0, this.fadeAlpha);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), this.blackTexture);
	}
	
	IEnumerator TransitWithFade (Scene scene)
	{
		this.isFading = true;
		float time = 0;
		while (time <= fadeOutInterval) {
			this.fadeAlpha = Mathf.Lerp (0f, 1f, time / fadeOutInterval);      
			time += Time.deltaTime;
			yield return 0;
		}

		Application.LoadLevel ((int)scene);

		time = 0;
		while (time <= fadeInInterval) {
			this.fadeAlpha = Mathf.Lerp (1f, 0f, time / fadeInInterval);
			time += Time.deltaTime;
			yield return 0;
		}
		
		this.isFading = false;
	}
	
}