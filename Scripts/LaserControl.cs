using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
	public enum LaserStates
    {
		TRAVEL,
		RETURNING,
		RETURNED
	}

	public GameObject player;

	//see player control. Default state is TRAVEL, set it for stuff like "returning" etc.
	public LaserStates laserState = LaserStates.TRAVEL;

    public float speed;

	public float breakAway = 0.3f;

	public float breakAwayDelay = 0.4f;

	public float radius = 10f;

	public float radiusSpeed = 10f;

	public float rotationSpeed = 720;

	private float timeSinceReturn;
    private Vector3 _moveVec;
    private Rigidbody rigid;

	private Transform center;
    private InputManager inputs;

	// Use this for initialization
	void Start ()
    {
        moveVector = new Vector3(speed, 0);
        rigid = GetComponent<Rigidbody>();
		center = player.GetComponent<Transform>();
        inputs = player.GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (laserState == LaserStates.TRAVEL && Input.GetButtonDown("Fire1")) {
			timeSinceReturn = 0f;
			laserState = LaserStates.RETURNED;
			enableLaser(false);
		}
		if (laserState == LaserStates.RETURNED && timeSinceReturn > breakAwayDelay && new Vector3(inputs.GetAxis("RightHorizontal"), 0, inputs.GetAxis("RightVertical")).normalized.magnitude > breakAway) {
			laserState = LaserStates.TRAVEL;
			enableLaser(true);
		}
		switch (laserState)
        {
            case LaserStates.TRAVEL:
                Move();
                break;
			case LaserStates.RETURNED:
				Orbit();
				break;
            default:
                print("error - invalid input");
                break;
        }
		timeSinceReturn += Time.deltaTime;
	}

	private void enableLaser(bool enable) {
		GetComponent<TrailRenderer>().enabled = enable;
		if (!enable) {
			GetComponent<TrailRenderer>().Clear();
		}
	}

	void Orbit() {
		transform.RotateAround(player.GetComponent<Transform>().position, Vector3.up, rotationSpeed * Time.deltaTime);
		var desiredPosition = (transform.position - center.position).normalized * radius + center.position;
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
	}

    void Move()
    {
        rigid.velocity = moveVector;
    }

    Vector3 moveVector
    {
        get
        {
            Vector3 move = new Vector3(inputs.GetAxis("RightHorizontal"), 0, inputs.GetAxis("RightVertical")).normalized * speed;
            if(move.magnitude < speed)
            {
                return _moveVec;
            }
            else
            {
                _moveVec = move;
                return move;
            }
        }

        set
        {
            _moveVec = value;
        }
    }
}
