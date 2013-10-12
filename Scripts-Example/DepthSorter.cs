using UnityEngine;
using System.Collections;

public class DepthSorter : MonoBehaviour {
	public static float MAX_Y;
	public float Offset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, (transform.position.y+Offset)/5);
	}
}
