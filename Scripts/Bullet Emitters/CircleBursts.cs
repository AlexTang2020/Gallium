using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBursts : EnemyShot {

	public float bulletSpeed;
	public int perCircle;
	public float shotDelay;

	public int spiraling;

	private int alt = 1;

	public override void Fire() {
		Transform[] bullets = new Transform[perCircle];

		for (int i = 0; i < perCircle; i++) {
			bullets[i] = Instantiate(bullet);

			float altAngle = (2 * Mathf.PI) / (float)perCircle / (float)spiraling * alt;

			Vector3 bulletVec = AngleMath.AngleToVector3(((2 * Mathf.PI) / (float)perCircle) * i + altAngle);
			bulletVec = bulletVec * bulletSpeed;

			bullets[i].transform.position = transform.position;
			bullets[i].GetComponent<UpdateBullet>().movement = bulletVec;
		}

		alt++;
		if (alt > spiraling) {
			alt = 1;
		}
	}

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
