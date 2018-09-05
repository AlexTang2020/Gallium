﻿/* Author: Javier Bernal
 * Date Created: 4/10/2018
 * Date Modified: 
 * Modified By: 
 * Description: Bullets will emit in different patterns
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBurstDirected : EnemyShot {

    [Header("Set in Inspector")]
	public float bulletSpeed;       //Speed of the bullet
    public int perCircle;           //Number of bullets per circle burst
    public float shotDelay;         //Delay between circle bursts
    public int wise;

	public int spiraling;           //How much bullets will emit in a spiral

	private int alt = 1;

    //Bullets fire out 
    public override void Fire() {
		Transform[] bullets = new Transform[perCircle];

		for (int i = 0; i < perCircle; i++) {
			bullets[i] = Instantiate(bullet);

			float altAngle = (2 * Mathf.PI) / (float)perCircle / (float)spiraling * alt;

			Vector3 bulletVec = AngleMath.AngleToVector3(wise*(((2 * Mathf.PI) / (float)perCircle) * i + altAngle));
			bulletVec = bulletVec * bulletSpeed;

			bullets[i].transform.position = transform.position;
			bullets[i].GetComponent<UpdateBullet>().movement = bulletVec;
		}

		alt++;
		if (alt > spiraling) {
			alt = 1;
		}
	}

    //Checks when to fire bullets
    public override bool FireRate() {
		count += Time.deltaTime;
		if (count >= shotDelay && firing) {
			count = 0;
			return true;
		}
		return false;
	}

	// Update is called once per frame
	void Update () {
		if (FireRate()) {
			Fire();
		}
	}
}
