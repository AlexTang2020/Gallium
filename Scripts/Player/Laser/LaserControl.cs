/* Author: Alvaro Gudiswitz
 * Date Created: 3/13/2018
 * Date Modified: 4/17/2018
 * Modified By: Alexander Tang
 * Description: Laser Controls and Behavior
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LaserControl : MonoBehaviour
{
    //Different State of the laser
	public enum LaserStates
    {
		TRAVEL,
		RETURNING,
		RETURNED
	}
    [Header("Set in Inspector")]
	public GameObject player;                               //Player object laser returns to
    public LaserStates laserState = LaserStates.TRAVEL;     //see player control. Default state is TRAVEL, set it for stuff like "returning" etc.
    public float speed;                                     //Speed of the laser
    public float breakAway = 0.3f;                      //How much you need to move joystick before the laser will move
	public float breakAwayDelay = 0.4f;                 //How long laser needs to stay by player before it can move away again
    public float radius = 10f;                          //Radius of laser orbitting player
    public float radiusSpeed = 10f;                     //Speed of laser while in orbit  
    public float rotationSpeed = 720;                    //Laser speed of rotation

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
		if (laserState == LaserStates.TRAVEL && Input.GetButtonDown("Return")) {
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

    //Enables laser and creates line trail
	private void enableLaser(bool enable) {
		GetComponent<TrailRenderer>().enabled = enable;
		if (!enable) {
			GetComponent<TrailRenderer>().Clear();
		}
	}

    //Laser will orbit player object
	void Orbit() {
		transform.RotateAround(player.GetComponent<Transform>().position, Vector3.up, rotationSpeed * Time.deltaTime);
		var desiredPosition = (transform.position - center.position).normalized * radius + center.position;
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
	}

    //Laser will move in direction give using controls
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

    //Laser destroys objects tagged as an "Enemy" or "Boss"
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            if (MinionMovement.enemyHP <= 0)
            {
                Destroy(other.gameObject);
            }
            else
            {
                MinionMovement.enemyHP--;
            }
        }
        else if (other.transform.tag == "Boss")
        {
            if (EnemyHP.bossHP <= 0)
            {
                Destroy(other.gameObject);
                if (EnemyHP.bossForm == 0)
                {
                    print("This isn't even my final form, I will return");
                    SceneManager.LoadScene(2);
                    EnemyHP.bossForm++;
                    EnemyHP.bossHP = 7;
                }
                else
                {
                    print("You Beat Gallium");
                    EnemyHP.bossForm--;
                    SceneManager.LoadScene(2);
                }
            }
            else
            {
                EnemyHP.bossHP--;
            }
        }
		if (other.transform.tag == "Boss")
		{
			BossHP.HP--;
			print ("Boss HP is: "+ BossHP.HP);
		}
    }
}
