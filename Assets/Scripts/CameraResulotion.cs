using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResulotion : MonoBehaviour {
	public int xx = 1280;
	public int yy = 800;
	public Camera Cam;
	// Use this for initialization
	void Start () {
		Screen.SetResolution (xx,yy,true);
		//Cam.aspect = 16f / 9f;
	}
	

}
