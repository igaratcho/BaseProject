using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;

[Serializable()]
public class PoolData
{
	public string name;
	public GameObject prefab;
	public int maxCount;
	public int prepareCount;
}

public class ObjectPool : MonoBehaviour
{	
	[SerializeField]
	PoolData[] poolData;
	
	[SerializeField]
	float refleshInterval = 0f;
	
	Dictionary<int,List<GameObject>> poolObj;
	
	void Awake()
	{
		Load ();
	}
	
	void Load()
	{
		poolObj = new Dictionary<int,List<GameObject>> (poolData.Length); 
		
		for (int i=0; i<poolData.Length; i++) 
		{
			GameObject prefab = poolData[i].prefab;
			int prepareCount = poolData[i].prepareCount;
			int maxCount = poolData[i].maxCount;
			
			List<GameObject> objList = new List<GameObject>(maxCount);
			for(int k=0; k<prepareCount; k++) {
				GameObject obj = Instantiate(prefab) as GameObject;
				{
					obj.SetActive(false);
					obj.transform.SetParent(this.transform);
				}
				objList.Add(obj);
			}
			poolObj.Add(i, objList);
		}
	}
	
	void OnEnable ()
	{
		if (refleshInterval > 0f)
			RefleshAll ();
	}
	
	void OnDisable ()
	{
		StopAllCoroutines ();
	}
	
	public GameObject GetPooledObject (int index)
	{
		List<GameObject> objList = poolObj [ index ];
		
		if (!objList.IsAny ())
			return null;
		
		foreach (GameObject obj in objList) 
		{
			if (obj.activeSelf == false) 
			{
				obj.SetActive (true);
				return obj;	
			}
		}

		GameObject prefab = poolData [ index ].prefab;
		int maxCount = poolData [ index ].maxCount;
		
		if (objList.Count < maxCount) {
			GameObject obj = GameObject.Instantiate (prefab) as GameObject;
			{
				obj.SetActive (true);
				obj.transform.SetParent(this.transform);
			}
			objList.Add (obj);
			return obj;
		}
		
		return null;
	}

	public void RelaseAll()
	{
		foreach(var list in poolObj.Values)
		{
			foreach(var obj in list)
			{
				obj.SetActive(false);
			}
		}
	}
	
	void RefleshAll ()
	{
		for (int i=0; i<poolData.Length; i++) 
		{
			int prepareCount = poolData[i].prepareCount;
			StartCoroutine(Reflesh(i, prepareCount));
		}
	}
	
	IEnumerator Reflesh (int index, int prepareCount)
	{
		while (true) 
		{
			RemoveObject (index, prepareCount);
			yield return new WaitForSeconds (refleshInterval);
		}
	}

	void RemoveObject (int index, int threshold)
	{
		List<GameObject> objList = poolObj[ index ];
		
		if (objList.IsAny ()) 
		{
			if (objList.Count > threshold) 
			{
				int needRemoveCount = objList.Count - threshold;
				foreach (GameObject obj in objList) 
				{
					if (needRemoveCount == 0)
						break;

					if (obj.activeSelf == false) 
					{
						objList.Remove (obj);
						Destroy (obj);
						needRemoveCount --;
					}
				}
			}
		}
	}
}