using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyScript : MonoBehaviour {
	private Animator animator;
	public float MoveSpeed;
	private Rigidbody rigidBody;
	public GameObject FollowTarget;
	public CollisionListScript PlayerSensor;
	public CollisionListScript AttackSensor;
	public enemyGunManager enemyGunManager;
	public float CurrentHP = 100;

	public void AttackPlayer()
	{
		if (AttackSensor.CollisionObjects.Count > 0) {
			Debug.Log ("Attack player");
			Debug.Log (AttackSensor.CollisionObjects [0].transform.GetChild (0));
			AttackSensor.CollisionObjects [0].transform.GetChild(0).GetChild(0).SendMessage("Hit",10);
		}
	}

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		rigidBody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerSensor.CollisionObjects.Count > 0) {
			FollowTarget = PlayerSensor.CollisionObjects [0].gameObject;
			if (FollowTarget != null) {
				Vector3 lookAt = FollowTarget.gameObject.transform.position;
				//lookAt.y = this.gameObject.transform.position.y;

				this.transform.LookAt (lookAt);
				animator.SetBool ("Run", true);
				if (AttackSensor.CollisionObjects.Count > 0) {
					animator.SetBool ("Attack", true);
					this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
					enemyGunManager.TryToTriggerGun ();
					//AttackSensor.CollisionObjects [0].transform.GetChild(0).GetChild(0).SendMessage("Hit",1);
				} else {
					animator.SetBool ("Attack", false);
					rigidBody.velocity = this.transform.forward * MoveSpeed;
				}
			}
		} else {
			this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
	}

	public void Hit(float value){
		CurrentHP -= value;
		Debug.Log ("Enemy");
		Debug.Log (CurrentHP);
		if (CurrentHP <= 0) {BuryTheBody ();}
	}


	void BuryTheBody(){
		this.GetComponent<Rigidbody> ().useGravity = false;
		this.GetComponent<Collider> ().enabled = false;
		this.transform.DOMoveY (-0.8f, 1f).SetRelative(true).SetDelay(1).OnComplete (()=>
			{
				GameObject.Destroy(this.gameObject);
			});
	}
}
