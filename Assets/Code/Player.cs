using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float _speed_back = 1;
	public float _speed_front = 0.75f;

	Rigidbody _rb;

	float GetSpeed() {
		return 0;
	}

	void Awake() {
		_rb = GetComponent<Rigidbody>();
	}
	
	
	void Update () {
		_rb.MovePosition( new Vector3( GetSpeed()*Time.deltaTime, 0, 0 ) );
	}


}
