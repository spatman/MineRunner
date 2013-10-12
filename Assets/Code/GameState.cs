using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

	static public GameState _instance;

	[SerializeField] private int segmentAmount = 10;
	[SerializeField] private GameObject generator;
	[SerializeField] private GameObject rockPrefab;
	[SerializeField] private GameObject playerPrefab;

	public List<Player> _players;

	/*private Player player1;
	private Player player2;*/
	private List<GameObject> rocks;
	private List<GameObject> level;

	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		// generate level
		level = generator.GetComponent<Generator>().GenerateLevel(segmentAmount);
		// spawn players
		
		// start game
	}
	
	// Update is called once per frame
	void Update () {
	// handle common controls (pause, menu etc)	
	// spawn random rocks
	// keep track of players and game states	
	}
	
	private GameObject dropRock()
	{
		GameObject newRock = null;
		// spawn a rock at random location in front of players
		
		return newRock;
	}
	
	public void DestroyRock(GameObject rock)
	{
		// remove rock from game
		// maybe spawn little rocks for other player to throw
	}
}
