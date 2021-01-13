using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	public int x,y,z;
	
	// Update is called once per frame
	void LateUpdate () {
		gameObject.transform.eulerAngles = new Vector3 (gameObject.transform.eulerAngles.x + x, gameObject.transform.eulerAngles.y + y, gameObject.transform.eulerAngles.z + z);
	}
}
