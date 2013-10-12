using UnityEngine;
using System.Collections;

public class Player : MineBehaviour {
	
	[SerializeField] private float speed;
	
	// Use this for initialization
	void Start () {
	// initial speed and positioning
	}
	
	// Update is called once per frame
	void Update () {
		playerControls();
	// check for collision with rock -> slow down / stun
	// react if being hit by rock from other player	
	}
	
	public Vector2 GetPosition()
	{
		return transform.position;	
	}
	
	public float GetSpeed()
	{ 
		return speed; 
	}
	
	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}
	
	private void playerControls()
	{
		// automove
		// keyboard input
	}
	
}
