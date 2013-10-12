using UnityEngine;
using System.Collections;

public class MineBehaviour : MonoBehaviour {

	protected bool isPaused = false;
	
	public void Pause()
	{
		isPaused = true;	
	}
	
	public void Unpause()
	{
		isPaused = false;	
	}
}
