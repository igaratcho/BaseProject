using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
	static T instance;

	public static T Instance 
	{
		get 
		{
			if (instance == null) 
			{
				instance = (T)FindObjectOfType(typeof(T));
				
				if (instance == null) 
				{
					Debug.LogError (typeof(T) + "is nothing");
				}
			}
			
			return instance;
		}
	}

	void Awake()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}
		
		DontDestroyOnLoad (this.gameObject);

		OnAwake ();
	}

	protected virtual void OnAwake() {}
}