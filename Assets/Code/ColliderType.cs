using UnityEngine;
using System.Collections;

public class ColliderType : MonoBehaviour {

	public enum Type {
		None,
		Ground,
		Obstacle
	}

	public Type _type;

}
