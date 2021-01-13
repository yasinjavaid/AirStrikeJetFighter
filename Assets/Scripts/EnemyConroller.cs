using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyConroller : MonoBehaviour {
	public bool isNotEscape;
	public Animator Anim;
	public NavMeshAgent EnemyAgent;
	public bool walk, run;
	public bool isDead;
	// Use this for initialization
	void OnEnable(){
		if (isDead) {
			Die ();
		}
		Anim = gameObject.GetComponent<Animator> ();
		EnemyAgent = gameObject.GetComponent<NavMeshAgent> ();
	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}
	public void SetDestination(){
		if (isNotEscape)
			return;
		EnemyAgent.SetDestination (GlobalVeriables.Instance.LevelsDestination[GlobalVeriables.Instance.CurrentLevel].transform.position);
		Run ();
	}
	public void Walk(){
		Anim.SetBool ("Die",false);
		Anim.SetBool ("Walk",true);
		Anim.SetBool ("Run", false);
	}

	public void Run(){
		Anim.SetBool ("Die",false);
		Anim.SetBool ("Walk",false);
		Anim.SetBool ("Run", true);
	}
	public void Die(){
		StartCoroutine (DoDie());
	//	Invoke ("Destroy",2);
	}
	IEnumerator DoDie(){
		yield return new WaitForSeconds (0f);
	
		Anim.SetBool ("Die",true);
		Anim.SetBool ("Walk",false);
		Anim.SetBool ("Run", false);
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "DestinationPoint") {
			FindObjectOfType<LevelManager> ().LevelFail ();
		}
	}
	void Destroy(){
		Destroy (gameObject);
	}
}
