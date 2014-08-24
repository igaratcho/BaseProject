using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods
{
	public static class TransformExtension {
		
		public static void SetPositionX(this Transform transform, float posX, bool isLocal=false ) {
			Vector3 pos;
			if (isLocal) {
				pos = transform.localPosition;
				pos.x = posX;
				transform.localPosition = pos;
			} else {
				pos = transform.position;
				pos.x = posX;
				transform.position = pos;
			}
		}
		
		public static void SetPositionY(this Transform transform, float posY, bool isLocal=false ) {
			Vector3 pos;
			if (isLocal) {
				pos = transform.localPosition;
				pos.y = posY;
				transform.localPosition = pos;
			} else {
				pos = transform.position;
				pos.y = posY;
				transform.position = pos;
			}
		}
		
		public static void SetPositionZ(this Transform transform, float posZ, bool isLocal=false ) {
			Vector3 pos;
			if (isLocal) {
				pos = transform.localPosition;
				pos.z = posZ;
				transform.localPosition = pos;
			} else {
				pos = transform.position;
				pos.z = posZ;
				transform.position = pos;
			}
		}
		
		public static void SetParent(this Transform transform, Transform parent) {
			transform.parent = parent;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}
	}
	
	public static class EnumerableExtension {
		
		public static bool IsAny<T>(this IEnumerable<T> enumerable) {
			
			if (enumerable == null)
				return false;
			
			var collection = enumerable as ICollection<T>;
			if (collection != null)
				return collection.Count > 0;
			
			return enumerable.Any(); 
		}
	}


}