using UnityEngine;
using System.Collections;

public class Segment : MonoBehaviour {
	
	[SerializeField] private GameObject end;
	
	public Vector2 GetStartPoint()
	{ 
		Vector2 result = new Vector2(transform.position.x, transform.position.y);
		return result;	
	}
	
	public Vector2 GetEndPoint()
	{
		Vector2 result = new Vector2(end.transform.localPosition.x, end.transform.localPosition.y);
		result *= end.transform.lossyScale.x;
		return result;
	}
}
