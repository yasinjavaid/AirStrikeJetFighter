using UnityEngine;
using System.Collections;

public class PlayerDead : FlightOnDead
{
	public delegate void PlayerDie();
	public static event PlayerDie OnPlayerDie;
	void Start (){}
	
	// if player dead 
	public override void OnDead (GameObject killer)
	{
		// if player dead call GameOver in GameManager
		OnPlayerDie();
		base.OnDead (killer);
	}
}
