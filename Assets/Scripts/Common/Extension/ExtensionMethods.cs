using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods
{
	public static class MonoBehaviourExtension 
	{	

	}

	public static class TransformExtension 
	{	
		public static void SetLocalPositionX(this Transform transform, float x) 
		{
			Vector3 pos = transform.localPosition;
			pos.x = x;
			transform.localPosition = pos;
		}

		public static void SetLocalPositionY(this Transform transform, float y) 
		{
			Vector3 pos = transform.localPosition;
			pos.y = y;
			transform.localPosition = pos;
		}

		public static void SetLocalPositionZ(this Transform transform, float z) 
		{
			Vector3 pos = transform.localPosition;
			pos.z = z;
			transform.localPosition = pos;
		}

		public static void SetPositionX(this Transform transform, float x) 
		{
			Vector3 pos = transform.position;
			pos.x = x;
			transform.position = pos;
		}

		public static void SetPositionY(this Transform transform, float y) 
		{
			Vector3 pos = transform.position;
			pos.y = y;
			transform.position = pos;
		}
		
		public static void SetPositionZ(this Transform transform, float z) 
		{
			Vector3 pos = transform.position;
			pos.z = z;
			transform.position = pos;
		}

		public static void SetParent(this Transform transform, Transform parent) {
			transform.parent = parent;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}
	}

	public static class EnumerableExtension 
	{
		public static bool IsAny<T>(this IEnumerable<T> enumerable) {
			
			if (enumerable == null)
				return false;
			
			var collection = enumerable as ICollection<T>;
			if (collection != null)
				return collection.Count > 0;
			
			return enumerable.Any(); 
		}
	}

	public class MathfExtension
	{
		public static float Rad2Deg( float rad )
		{
			return rad * Mathf.Rad2Deg;
		}
		
		public static float Deg2Rad( float deg )
		{
			return deg * Mathf.Deg2Rad;
		}
		
		public static float GetDirAngle(Vector2 from, Vector2 to)
		{
			Vector2 dir = to - from;
			return Rad2Deg(Mathf.Atan2(dir.y, dir.x));
		}
		
		public static Vector2 TransformAffine(Vector2 pos, float deg)
		{
			float sin = Mathf.Sin (Deg2Rad (deg));
			float cos = Mathf.Cos (Deg2Rad (deg));
			
			Vector2 transPos = Vector2.zero;
			transPos.x = (pos.x * cos) - (pos.y * sin);
			transPos.y = (pos.x * sin) + (pos.y * cos);
			
			return transPos;
		}
		
		public static Vector3 BezierCurve(Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			return BezierCurve (p1, p2, p2, p3, t);
		}
		
		public static Vector3 BezierCurve(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
		{
			return new Vector3 (
				BezierCurve(p1.x, p2.x, p3.x, p4.x, t),
				BezierCurve(p1.y, p2.y, p3.y, p4.y, t),
				BezierCurve(p1.z, p2.z, p3.z, p4.z, t)
				);
		}
		
		static float BezierCurve(float x1, float x2, float x3, float x4, float t)
		{
			return Mathf.Pow(1 - t, 3) * x1 + 3 * Mathf.Pow(1 - t, 2) * t * x2 + 3 * (1 - t) * Mathf.Pow(t, 2) * x3 + Mathf.Pow(t, 3) * x4;
		}
	}
}