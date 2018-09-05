/* Author: Alvaro Gudiswitz
 * Date Created: 3/13/2018
 * Date Modified: 4/3/2018
 * Modified By: Alvaro Gudiswitz
 * Description: Player Control Behavior
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //Different states player can be in
	public enum PlayerState {
		DEFAULT,
        DODGESTART,
        DODGING,
        DODGEREC,
        DAMAGED,
	}

    [Header("Set Dynamically")]
    //Character state used for dodges and invulnerability
    public PlayerState charState = PlayerState.DEFAULT;

    [Header("Set in Inspector")]
    public float speed;     //Speed of player
    public float dodgeSpeed = 10f;  //Speed of player while dodging
    public float statelock = 0;     //Length of time player stays in one state
    public float dodgeTime = .3f;   //Starting dodge
    public float dodgeRecovery = 1f;    //Length of dodge
    public float damageInvul = .5f; //Length of invulnerability after taking damage

    public int playerHP = 3;    //Health of the Player

    public Material playerMat;  //Player Material

    public Color glowColor;     //Player default color 
    public Color dodgeColor;    //Player color when dodging
    public Color invulColor;    //Player color when taking damage and is invulnerable

    Rigidbody rigid;
    InputManager inputs;

    //dodge vector
    private Vector3 _dodgeVec;


    // Use this for initialization
    void Start ()
    {
        playerMat.color = glowColor;
        rigid = GetComponent<Rigidbody>();
        inputs = GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        dodgeVector = dodgeVector;
        switch(charState)
        {
            case PlayerState.DEFAULT:
                Move();
                break;
            case PlayerState.DODGESTART:
                DodgeInit();
                break;
            case PlayerState.DODGING:
                Dodge();
                break;
            case PlayerState.DODGEREC:
                DodgeRec();
                break;
            case PlayerState.DAMAGED:
                TakeDamage();
                break;
        }
		
	}

    //Set up movement for dodge. 
    void DodgeInit()
    {
        playerMat.color = dodgeColor;
        charState = PlayerState.DODGING;
        statelock = dodgeTime;

        rigid.velocity = dodgeVector ;
    }

    //dodge animation and state
    void Dodge()
    {
        statelock -= Time.deltaTime;
        if (statelock < 0f)
        {
            charState = PlayerState.DODGEREC;
            statelock = dodgeRecovery;
        }
    }

    //startup, recovery and dodge. If I were doing this in a project with a larger moveset, a spreadsheet would be doing the thinking here. Rn recover and start is 1f, but adding in those is pretty easy.
    void DodgeRec()
    {
        playerMat.color = glowColor;
        statelock -= Time.deltaTime;
        Move();
        if (statelock < 0f)
        {
            charState = PlayerState.DEFAULT;
        }
    }

    //Damage taking and statelock
    void TakeDamage()
    {
        playerMat.color = invulColor;
        statelock -= Time.deltaTime;
        Move();
        if (statelock < 0f)
        {
            playerMat.color = glowColor;
            charState = PlayerState.DEFAULT;
        }
    }

    //Standard movement
    void Move()
    {
        rigid.velocity = (new Vector3(inputs.GetAxis("Horizontal"), 0, inputs.GetAxis("Vertical"))) * speed;

        if(Input.GetButtonDown("Dodge") && charState != PlayerState.DODGEREC)
        {
            charState = PlayerState.DODGESTART;
        }
    }

    //"handling" damage
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Bullet")
        {
            if(charState != PlayerState.DODGING && charState != PlayerState.DAMAGED)
            {
                if (playerHP <= 0)
                {
                    print("You are already dead");
                    SceneManager.LoadScene(2);
                    EnemyHP.bossHP = 7;
                }
                else
                {
                    print("Damaged");
                    statelock = damageInvul;
                    charState = PlayerState.DAMAGED;
                    playerHP--;
                }
            }
            else
            {
                print("Dodged/Invul'd");
            }
        }
    }


    Vector3 dodgeVector
    {
        get
        {
            Vector3 move = new Vector3(inputs.GetAxis("Horizontal"), 0, inputs.GetAxis("Vertical")).normalized * dodgeSpeed;
            if (move.magnitude < speed)
            {
                return _dodgeVec;
            }
            else
            {
                _dodgeVec = move;
                return move;
            }
        }

        set
        {
            _dodgeVec = value;
        }
    }
}
