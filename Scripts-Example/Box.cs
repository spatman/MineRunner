using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
	tk2dSpriteAnimator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<tk2dSpriteAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayTele() {
		anim.Play("Box Open");
	}
}
