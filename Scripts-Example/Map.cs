using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map {
	
	public const int TILE_SIZE = 64;
	
	int width;
	int height;
	GameObject tilePrefab;
	GameObject worldObj;
	GameObject boxPrefab;
	GameObject groundPrefab;
	GameObject enemyPrefab;
	World world;
	List<Box> boxes;
	List<Vector2> boxPositions;
	
	List<Vector2> openTiles;
	
	List<GameObject> groundTiles;
	
	List<GameObject> enemies;
	
	Tile[,] tiles;
	
	public static Vector2 TileToWorldPos(Vector2 _tile) {
		return _tile * TILE_SIZE;
	}
	
	public static Vector2 WorldToTilePos(Vector2 _world) {
		return _world / TILE_SIZE;	
	}
	
	public Map(int _width, int _height, GameObject _worldObj, World _world) {
		width = _width;
		height = _height;
		tilePrefab = _world.TilePrefab;
		worldObj = _worldObj;
		groundPrefab = _world.GroundPrefab;
		boxPrefab = _world.BoxPrefab;
		enemyPrefab = _world.EnemyPrefab;
		DepthSorter.MAX_Y = height * TILE_SIZE;
		groundTiles = new List<GameObject>();
		world = _world;
		enemies = new List<GameObject>();
	}
	
	public bool IsTileFree(Vector2 _pos) {
		int x = (int)_pos.x;
		int y = (int)_pos.y;
		if( x < 0 || x >= width || y < 0 || y >= height)
			return false;
		
		return tiles[x, y] == null;	
	}
	
	public Vector2 GetFirstFreeTile() {
		int index = (int)(Random.value * openTiles.Count);
		return openTiles[index];
	}
	
	public void Reset(int _seed) {
		if(tiles == null)
			tiles = new Tile[width, height];
		
		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				if(tiles[x,y] != null) {
					GameObject.Destroy(tiles[x,y].gameObject);
					tiles[x,y] = null;
				}
			}
		}
		
		foreach(GameObject groundTile in groundTiles)
			GameObject.Destroy(groundTile);
		groundTiles.Clear();
		
		foreach(GameObject enemy in enemies)
			GameObject.Destroy(enemy);
		enemies.Clear();
		
		if(boxes == null)
			boxes = new List<Box>();
		foreach(Box box in boxes)
			GameObject.Destroy(box.gameObject);
		
		if(boxPositions == null)
			boxPositions = new List<Vector2>();
		boxPositions.Clear();
		boxes.Clear();
		FillRandom(_seed);
	}
	
	public Box IsBoxPos(Vector2 _pos) {
		for( int i = 0; i < boxPositions.Count; i++)
			if(_pos == boxPositions[i])
				return boxes[i];
		return null;
	}
	
	public void Step() {
		foreach(GameObject enemy in enemies)
			enemy.GetComponent<Enemy>().Step();
	}
	
	void FillRandom(int _seed) {
		PerlinNoise noise = new PerlinNoise(_seed);
		
		float widthDivisor = 10.0f / (float)width;
    	float heightDivisor = 10.0f / (float)height;
		
		openTiles = new List<Vector2>();

		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				
				float val =
	                // First octave
	                (noise.Noise(2 * x * widthDivisor, 2 * y * heightDivisor, -0.5f) + 1) / 2 * 0.7f +
	                // Second octave
	                (noise.Noise(4 * x * widthDivisor, 4 * y * heightDivisor, 0) + 1) / 2 * 0.2f +
	                // Third octave
	                (noise.Noise(8 * x * widthDivisor, 8 * y * heightDivisor, +0.5f) + 1) / 2 * 0.1f;
					
				if(val < 0.47f || x == 0 || y == 0 || x == width - 1 || y == height - 1) {
					Vector2 pos = new Vector2(x, y) * TILE_SIZE;
					GameObject newTileObj = (GameObject)GameObject.Instantiate(tilePrefab);
					newTileObj.transform.position = pos;
					newTileObj.transform.parent = worldObj.transform;
					Tile newTile = newTileObj.GetComponent<Tile>();
					newTile.SetValues(world.Stage);
					tiles[x,y] = newTile;
				} else {
				
					openTiles.Add(new Vector2(x, y));
					tiles[x,y] = null;	
				}
				
				GameObject groundObj = (GameObject)GameObject.Instantiate(groundPrefab);
				groundObj.transform.position = new Vector2(x, y) * TILE_SIZE;
				groundObj.transform.parent = worldObj.transform;
				groundTiles.Add(groundObj);
				groundObj.GetComponent<Tile>().SetGround(world.Stage);
			
				
			}
		}
		
		int boxCount = 10;
		boxes = new List<Box>();
		
		for(int i = 0; i < boxCount; i++) {
			int index = (int)(Random.value * openTiles.Count);
			Vector2 boxPos = openTiles[index];
			openTiles.RemoveAt(index);
			boxPositions.Add(boxPos);
			GameObject newBoxObj = (GameObject)GameObject.Instantiate(boxPrefab);
			newBoxObj.transform.position = boxPos * TILE_SIZE;
			newBoxObj.transform.parent = worldObj.transform;
			Box newBox = newBoxObj.GetComponent<Box>();
			boxes.Add(newBox);
		}
		
		int enemyCount = 40;
		
		for(int i = 0; i < enemyCount && openTiles.Count > 0; i++) {
			int index = (int)(Random.value * openTiles.Count);
			Vector2 boxPos = openTiles[index];
			openTiles.RemoveAt(index);
			GameObject newBoxObj = (GameObject)GameObject.Instantiate(enemyPrefab);
			newBoxObj.transform.position = boxPos * TILE_SIZE;
			newBoxObj.transform.parent = worldObj.transform;
			enemies.Add(newBoxObj);
			newBoxObj.GetComponent<Enemy>().SetValues(world.Stage, world);
			newBoxObj.GetComponent<Enemy>().SetTilePos(boxPos);
		}
		
	}
	
}
