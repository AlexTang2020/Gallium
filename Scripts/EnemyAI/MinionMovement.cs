/* Author: Alexander Tang
 * Date Created: 3/31/2018
 * Date Modified: 4/5/2018
 * Modified By: Alexander Tang
 * Description: Enemy Minion Behavior
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour {
    [Header("Set in Inspector")]
    public static int enemyHP = 0;  //Enemy HP
    public float moveForce = 0f;    //Speed of the object 
    public LayerMask wall;          //Layer for object to move away from
    public float distFromWall = 0f;     //Distance from layer before object changes direction

    [Header("Set Dynamically")]
    public Vector3 moveDir;         //Direction for object to move towards

    private Rigidbody rbody;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody>();
        moveDir = ChooseDirection();
        transform.rotation = Quaternion.LookRotation(moveDir);
	}
	
	// Update is called once per frame
	void Update () {
        rbody.velocity = moveDir * moveForce;
        if (Physics.Raycast(transform.position, transform.forward, distFromWall, wall))
        {
            moveDir = ChooseDirection();
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
	}

    //Sets the direction for Gallium to follow till close to a specified layer
    Vector3 ChooseDirection()
    {
        System.Random ran = new System.Random();
        int i = ran.Next(0,3);
        Vector3 temp = new Vector3();

        if (i == 0)
        {
            temp = transform.forward;
        }
        else if (i == 1)
        {
            temp = -transform.forward;
        }
        else if (i == 2)
        {
            temp = transform.right;
        }
        else if(i == 3)
        {
            temp = -transform.right;
        }
        return temp;
    }
}
