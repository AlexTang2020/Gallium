/* Author: Aric Hasting
 * Date Created: 3/29/2018
 * Date Modified: 4/17/2018
 * Modified By: Alvaro Gudiswitz
 * Description: Player and Laser camera movement
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour {
    [Header("Set in Inspector")]
	public Transform player;    //Player Object camera follows
    public Transform laser;     //Laser camera follows
    public Camera cam;          //Camera that follows the player and laser

	private float camY;

    private float minSize = 12;
    private float maxSize = 20;

	// Use this for initialization
	void Start () {
		camY = transform.position.y;
        cam = transform.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        float magnitude = (player.position - laser.position).magnitude;
        Vector3 pos;
        if (magnitude > 8)
        {
            pos = (player.position * 3 + laser.position) / 4;
        }
        else if(magnitude > 2)
        {
            Vector3 pos1 = (player.position * 3 + laser.position) / 4;
            Vector3 pos2 = (player.position);

            float weight = (magnitude - 2) / 6 ;
            pos = pos1 * weight + pos2 * (1 - weight);
        }
        else
        {
            pos = player.position;
        }

		pos.y = camY;
		transform.position = pos;

        float xDist = Mathf.Abs(player.position.x - laser.position.x);
        float zDist = Mathf.Abs(player.position.z - laser.position.z);


        float targetSize = minSize;

        //if the camera is out of the range of effect
        if(zDist > 8 || xDist > 20)
        {
            targetSize = Mathf.Max(zDist * 7 / 8, xDist * 7/16);

            //targetSize = Mathf.Sqrt((player.position - laser.position).magnitude) + minSize;
            if(targetSize < minSize)
            {
                targetSize = minSize;
            }
        }
        else
        {
            targetSize = minSize;
        }

        if (xDist > 20)
        {
            if (targetSize > cam.orthographicSize)
            {
                cam.orthographicSize += .15f;
            }
            if (targetSize < cam.orthographicSize)
            {
                cam.orthographicSize -= .15f;
            }
        }
        else if(zDist > 8)
        {
            if (targetSize > cam.orthographicSize)
            {
                cam.orthographicSize += .25f;
            }
            if (targetSize < cam.orthographicSize)
            {
                cam.orthographicSize -= .25f;
            }
        }

	}
}
