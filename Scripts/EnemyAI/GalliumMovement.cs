/* Author: Alexander Tang
 * Date Created: 3/31/2018
 * Date Modified: 4/5/2018
 * Modified By: Alexander Tang
 * Description: Gallium Boss Movement Behavior
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GalliumMovement : MonoBehaviour {

    //Different states Gallium can be in
    public enum GalliumState
    {
        SEARCH,
        PURSUE,
        CHANGEFORM
    }

    [Header("Set in Inspector")]
    public GalliumState galliumState = GalliumState.SEARCH; //Gallium's inital state
    public Transform player;    //Transform that Gallium is searching for
    public float searchRadius = 50f;    //Search Radius of Gallium
    public float speed = 5f;    //Gallium's Speed
    public LayerMask wall;      //Layer for Gallium to move away from
    public float distFromWall = 0f; //Distance from layer before Gallium changes direction

    [Header("Set Dynamically")]
    public Vector3 moveDir;     //Direction for Gallium to move towards

    Rigidbody galliumRigid;
	// Use this for initialization
	void Start () {
        galliumRigid = GetComponent<Rigidbody>();
        moveDir = ChooseDirection();
        transform.rotation = Quaternion.LookRotation(moveDir);
    }
	
	// Update is called once per frame
	void Update () {
        switch (galliumState)
        {
            case GalliumState.SEARCH:
                Search();
                break;
            case GalliumState.PURSUE:
                Pursue();
                break;
            case GalliumState.CHANGEFORM:
                ChangeForm();
                break;
        }
    }
    
    //Gallium moves in a direction and checks for player Transform before changing states
    void Search()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance < searchRadius)
        {
            galliumState = GalliumState.PURSUE;
        }
        else
        {
            galliumRigid.velocity = moveDir * speed;
            if (Physics.Raycast(transform.position, transform.forward, distFromWall, wall))
            {
                moveDir = ChooseDirection();
                transform.rotation = Quaternion.LookRotation(moveDir);
            }
        }
    }

    //Gallium follows player Transform until it is outside the search radius
    void Pursue()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance < searchRadius)
        {
            Vector3 playerDir = player.position - transform.position;
            float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg - 90f;
            Quaternion turn = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, turn, 180);
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else
        {
            galliumState = GalliumState.SEARCH;
        }
    }

    //Gallium would change form and start back at search state
    void ChangeForm()
    {
        galliumState = GalliumState.SEARCH;
    }

    //Sets the direction for Gallium to follow till close to a specified layer
    Vector3 ChooseDirection()
    {
        System.Random ran = new System.Random();
        int i = ran.Next(0, 3);
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
        else if (i == 3)
        {
            temp = -transform.right;
        }
        return temp;
    }
}
