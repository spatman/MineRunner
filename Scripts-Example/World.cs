using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {
	
	class MapNode {
		public int Seed;
		public List<MapNode> Children;
		public MapNode Parent;
		public Vector2 ExitPos;
	}
	
	public GameObject TilePrefab;
	public GameObject BoxPrefab;
	public GameObject PlayerObj;
	public GameObject GroundPrefab;
	public GameObject EnemyPrefab;
	
	public Player Player;
	public Map Map;
	Vector2 playerStart;
	
	public int Stage = 0;
	
	private MapNode current;
	
	// Use this for initialization
	void Start () {
		Map = new Map(50, 50, gameObject, this);
		GoDown();
	}
	
	public void GoDown() {
		Random.seed = (int)(Time.renderedFrameCount * System.DateTime.Now.Ticks);
		int newSeed = (int)(Random.value * int.MaxValue);
		
		MapNode last = current;
		current = new MapNode();
		current.Seed = newSeed;
		current.Parent = last;
		current.Children = new List<MapNode>();
		if(last != null) {
			last.Children.Add(current);
			last.ExitPos = Player.GetTilePos();	
		}
		
		Map.Reset(current.Seed);
		Stage++;
		Player = PlayerObj.GetComponent<Player>();
		playerStart = Map.GetFirstFreeTile();
		Player.SetValues(playerStart, this);
		
		
	}
	
	public void GoUp() {
		if(current.Parent != null) {
			current = current.Parent;
			Stage--;
			Map.Reset(current.Seed);	
			Player = PlayerObj.GetComponent<Player>();
			Player.SetValues(current.ExitPos, this);
			
		}
	}
	
	public void Step() {
		Map.Step();
	}
	
	void TeleCompleted(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip) {
        GoDown();
    }

	
	// Update is called once per frame
	void Update () {
		Box boxAt = Map.IsBoxPos(Player.GetNextTilePos());
		if(boxAt != null) {
			boxAt.PlayTele();
			Player.PlayTele(TeleCompleted);
		}
		
		if(playerStart == Player.GetNextTilePos())
			GoUp();
	}
}
