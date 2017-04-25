using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class enemyGunManager : MonoBehaviour {
	public float MinimumShootPeriod;
	public float muzzleShowPeriod;
	private float shootCounter = 0;
	private float muzzleCounter = 0;
	public GameObject muzzleFlash;
	public GameObject bulletCandidate;
	private AudioSource gunShootSound;
	public CollisionListScript AttackSensor;

	public void Start(){
		gunShootSound = this.GetComponent<AudioSource> ();
	}

	public void TryToTriggerGun()
	{
		if (shootCounter <= 0) {
			Debug.Log ("Hi");
			/*
			gunShootSound.Stop ();
			gunShootSound.pitch = Random.Range (0.8f, 1);
			gunShootSound.Play ();
			*/

			this.transform.DOShakeRotation (MinimumShootPeriod * 0.8f, 3f);

			muzzleCounter = muzzleShowPeriod;
			muzzleFlash.transform.localEulerAngles = new Vector3 (0, -6 ,Random.Range(0,360));
			shootCounter = MinimumShootPeriod;

			GameObject newBullet =  GameObject.Instantiate (bulletCandidate);
			BulletScript bullet = newBullet.GetComponent<BulletScript> ();
			bullet.transform.position = muzzleFlash.transform.position;
			bullet.transform.rotation = muzzleFlash.transform.rotation;
			bullet.InitAndShoot (muzzleFlash.transform.forward);
			AttackSensor.CollisionObjects [0].transform.GetChild(0).GetChild(0).SendMessage("Hit",1);
			Debug.Log ("bye"); 
		}
	}
	public void Update()
	{
		if (shootCounter > 0) {
			shootCounter -= Time.deltaTime;
		}
		if (muzzleCounter > 0) {
			muzzleFlash.gameObject.SetActive (true);
			muzzleCounter -= Time.deltaTime;
		} else {
			muzzleFlash.gameObject.SetActive (false);
		}
	}
}
