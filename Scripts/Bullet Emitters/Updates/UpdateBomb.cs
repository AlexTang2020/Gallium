/* Author:Javier Bernal 
 * Date Created: 4/3/18
 * Date Modified: 
 * Description: 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdateBomb : MonoBehaviour {
    [Header("Set Dynamically")]
    public Vector3 movement;  //Bomb movement being translated
   // public GameObject explosion;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement * Time.deltaTime);
    }

    //Object destroyed on collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}

