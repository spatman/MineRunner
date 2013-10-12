using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject PlayerObj;
	
	Player player;
	Vector2 tilePos = Vector2.zero;
	// Use this forinitialization
	void Start () {
		player = PlayerObj.GetComponent<Player>();
		tilePos = player.GetTilePos();
		SetTilePos(tilePos);
	}
	
	void SetTilePos (Vector2 _pos) {
		tilePos = _pos;
		Vector2 worldPos = Map.TileToWorldPos(tilePos);
		transform.position = new Vector3(worldPos.x, worldPos.y, -10) - 
			new Vector3(Map.TILE_SIZE + Screen.width, Map.TILE_SIZE + Screen.height, 0)/2;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 worldPos = player.transform.position;
		transform.position = new Vector3(worldPos.x, worldPos.y, -10) - 
			new Vector3(Map.TILE_SIZE + Screen.width, Map.TILE_SIZE + Screen.height, 0)/2;
	}
}
