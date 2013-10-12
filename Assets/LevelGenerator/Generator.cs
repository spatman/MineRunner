using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {
	
	[SerializeField] private GameObject[] segmentPrefabs;
	
	public List<GameObject> GenerateLevel(int segmentAmount)
	{
		Vector2 lastPos = Vector2.zero;
		List<GameObject> level = new List<GameObject>();
		
		for(int i = 0; i < segmentAmount; i++)
		{
			int random = Random.Range(0, segmentPrefabs.Length);
			GameObject newSegment = (GameObject)Instantiate(segmentPrefabs[random], lastPos, Quaternion.identity);
			lastPos += newSegment.GetComponent<Segment>().GetEndPoint();
			
			level.Add(newSegment);
		}
		
		return level;
	}
}
