/* Author: Aric Hasting
 * Date Created: 3/29/2018
 * Date Modified: 
 * Modified By: 
 * Description: Bullet update
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBullet : MonoBehaviour {
    [Header("Set Dynamically")]
	public Vector3 movement;         //Bullet movement translated

    // Update is called once per frame
    void Update () {
		transform.Translate(movement * Time.deltaTime);
	}

    //Object destroyed on collision
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Obstacle") {
			Destroy(gameObject);
		}
	}
}
