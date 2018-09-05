using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralEmitter : EnemyShot {

	public float rotationSpeed;
	public float bulletSpeed;
	public float emissionRate;

	private float rotation = 0f;

	//controls rotation over time.
    public float FirePattern()
    {
        return Time.deltaTime * rotationSpeed;
    }

    //fireRate doesn't have to be constant.
    override
    public bool FireRate()
    {
        count += Time.deltaTime;
        if (count > emissionRate && firing)
        {
            count = 0;
            return true;
        }
        return false;
    }

	private void Update() {
		rotation += FirePattern();

		if (FireRate()) {
			Fire();
		}
	}

	//actually shoots it. Will send vector3 over too.
	override
    public void Fire()
    {
        Transform newBull = Instantiate(bullet);
        newBull.transform.position = transform.position;
        newBull.transform.rotation = Quaternion.identity;

        //rotates in the proper direction, gets where it's facing, then rotates it.
        newBull.transform.Rotate(0, rotation, 0);
        Vector3 movement = newBull.transform.forward;
        newBull.Translate(0, 0, 1);

        newBull.transform.Rotate(0, -rotation, 0);

        
        newBull.GetComponent<UpdateBullet>().movement = movement * bulletSpeed;
    }
}
