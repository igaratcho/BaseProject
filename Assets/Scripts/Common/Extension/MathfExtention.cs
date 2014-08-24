using UnityEngine;
using System.Collections;

public class MathfExtension
{
	public static float GetAngle(Vector3 form, Vector3 to)
	{
		Vector3 nVector = Vector3.zero;
		nVector.x = to.x;
		nVector.y = form.y;
		float a = to.y - nVector.y;
		float b = nVector.x - form.x;
		float tan = a/b;
		return RadToDegree(Mathf.Atan(tan));
	}
	
	public static float RadToDegree( float radius )
	{
		return (radius * 180) / Mathf.PI;
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