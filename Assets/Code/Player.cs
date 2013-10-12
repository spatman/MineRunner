using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float _speed_back = 10;
	public float _speed_front = 7.5f;

	Rigidbody _rb;

	public bool _first;

	void IsFirst() {

	}

	float GetSpeed() {
		return (_first) ? _speed_front : _speed_back;
	}

	void Awake() {
		_rb = GetComponent<Rigidbody>();
	}
	
	
	void Update () {
		IsFirst();
		_rb.MovePosition( _rb.position + new Vector3( GetSpeed()*Time.deltaTime, 0, 0 ) );
	}


}
