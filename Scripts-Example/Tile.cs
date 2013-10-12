using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	int type;
	Vector2 pos;
	tk2dSpriteAnimator anim;
	
	public void SetValues(int _type) {
		type = _type;
		anim = gameObject.GetComponent<tk2dSpriteAnimator>();
		if(_type > 0) {
			anim.Play(anim.GetClipByName("S2W1"), (float)Random.value * 2, 0);
		} else {
			anim.Play(anim.GetClipByName("S1W1"), (float)Random.value * 2, 0);
		}
	}
	
	public void SetGround(int _type) {
		type = _type;
		anim = gameObject.GetComponent<tk2dSpriteAnimator>();
		if(_type > 0) {
			anim.Play(anim.GetClipByName("S2G1"), (float)Random.value * 2, 0);
		} else {
			anim.Play(anim.GetClipByName("S1G1"), (float)Random.value * 2, 0);
		}
	}
	
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<tk2dSpriteAnimator>();
		anim.PlayFromFrame((int)(Random.value * anim.CurrentClip.frames.Length));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
