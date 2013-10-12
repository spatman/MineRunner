using UnityEngine;
using System.Collections;

public class Segment : MonoBehaviour {
	
	[SerializeField] private Vector2 start;
	[SerializeField] private Vector2 end;

	public Segment (Vector2 startPoint, Vector2 endPoint)
	{
		start = startPoint;
		end = endPoint;
	}
	
	public Vector2 GetStartPoint()
	{ 
		return start;	
	}
	
	public Vector2 GetEndPoint()
	{
		return end;	
	}
}
