using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartPoint : MonoBehaviour {
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(transform.position, new Vector3(5, 5, 5));
	}
	

}
