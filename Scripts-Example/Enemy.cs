using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	tk2dSpriteAnimator anim;
	Vector2 tilePos;
	Vector2 nextTilePos;
	
	string enemyString = "";
	
	float walkTimer = 0;
	const float WALK_TIME = 0.5f;
	World world;
	
	enum State {
		Standing,
		Walking,
		Teleporting
	}
	
	State state;
	// Use this for initialization
	void Start () {
	
	}
	
	public void Step() {
		int dir = (int)(Random.value * 4);
		switch(dir) {
		case 0: MoveUp(); break;
		case 1: MoveDown(); break;
		case 2: MoveRight(); break;
		case 3: MoveLeft(); break;
		}
	}
	
	public void SetTilePos(Vector2 _pos) {
		tilePos = _pos;
		transform.position = Map.TileToWorldPos(tilePos);
	}
	
	public void MoveTo(Vector2 _tile) {
		nextTilePos = _tile;
		walkTimer = WALK_TIME;
		state = State.Walking;
		anim.Play(enemyString + "Walk");
	}
	
	public void SetValues(int _stage, World _world) {
		world = _world;
		anim = gameObject.GetComponent<tk2dSpriteAnimator>();
		if(_stage > 0) {
			enemyString = "E1S2 ";
			anim.Play(anim.GetClipByName("E1S2 Idle"), (float)Random.value * 2, 0);
		} else {
			anim.Play(anim.GetClipByName("E1S1 Idle"), (float)Random.value * 2, 0);
			enemyString = "E1S1 ";
		}
	}
	// Update is called once per frame
	void Update () {
		if(world.Player.GetTilePos() == tilePos)
			Application.LoadLevel("menu");
		if(state == State.Standing) {
			anim.AnimationCompleted = null;
		} else if(state == State.Walking) {
			walkTimer -= Time.deltaTime;
			transform.position = Vector2.Lerp(tilePos * Map.TILE_SIZE, nextTilePos * Map.TILE_SIZE, (WALK_TIME - walkTimer)/WALK_TIME);
			anim.AnimationCompleted = null;
			if(walkTimer <= 0)
			{
				bool movedX = nextTilePos.x - tilePos.x != 0 || nextTilePos.y > tilePos.y;
				SetTilePos(nextTilePos);
				anim.Play(enemyString + "Idle");				
			}
		}
	}
		
	void MoveUp() {
		Vector2 newPos = new Vector2(tilePos.x, tilePos.y + 1);
		if(world.Map.IsTileFree(newPos)) {
			MoveTo(newPos);	
		}
	}
	
	void MoveRight() {
		Vector2 newPos = new Vector2(tilePos.x + 1, tilePos.y);
		if(world.Map.IsTileFree(newPos)) {
			MoveTo(newPos);
		}	
	}
	
	void MoveLeft() {
		Vector2 newPos = new Vector2(tilePos.x - 1, tilePos.y);
		if(world.Map.IsTileFree(newPos)) {
			MoveTo(newPos);
		}
	}
	
	void MoveDown() {
		Vector2 newPos = new Vector2(tilePos.x, tilePos.y - 1);
		if(world.Map.IsTileFree(newPos)) {
			MoveTo(newPos);
		}
	}
}
