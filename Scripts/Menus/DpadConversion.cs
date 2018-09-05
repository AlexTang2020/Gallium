/* Author: Alvaro Gudiswitz
 * Date Created: 4-17-18
 * Date Modified: 
 * Modified By: 
 * Description: Use joystick and dpad input.
 */
using UnityEngine;

public class DpadConversion : MonoBehaviour
{
    //so you can have multiple instances
    public DpadConversion() { }
    [Header("Set Dynamically")]
    public float vert;              
    int dpadUpState = 0;
    int dpadSideState = 0;

    int joyUpState = 0;
    int joySideState = 0;

    //press variables
    public bool up = false;         //Check if up is pressed
    public bool down = false;       //Check if down is pressed
    public bool left = false;       //Check if left is pressed
    public bool right = false;      //Check if right is pressed

    public int upPress = 0;         //Value of press on the y axis
    public int sidePress = 0;       //Value of press on the x axis

    InputManager joystick;

    private void Start()
    {
        joystick =  gameObject.AddComponent<InputManager>();

    }

    // Converts d-pad axises into button-type inputs.
    void Update()
    {
        // reset EVERYTHING
        upPress = 0;
        sidePress = 0;
        Reset();


        //joystick control part

        if (Mathf.RoundToInt(joystick.GetAxis("Vertical")) != joyUpState)
        {
            joyUpState = Mathf.RoundToInt(joystick.GetAxis("Vertical"));
            if (joyUpState == 1)
            {
                up = true;
            }
            else if (joyUpState == -1)
            {
                down = true;
            }
            upPress = joyUpState;
        }
        if (Mathf.RoundToInt(joystick.GetAxis("Horizontal")) != joySideState)
        {
            joySideState = Mathf.RoundToInt(joystick.GetAxis("Horizontal"));
            if (joySideState == 1)
            {
                right = true;
            }
            else if (joySideState == -1)
            {
                left = true;
            }
            sidePress = joySideState;
        }

        //Dpad control part
        if (!Continue(up, down, left, right))
        {
            if (Input.GetAxis("DpadDown") != dpadUpState)
            {
                dpadUpState = (int)(Input.GetAxis("DpadDown"));
                if (dpadUpState == 1)
                {
                    up = true;
                }
                else if (dpadUpState == -1)
                {
                    down = true;
                }
                upPress = dpadUpState;
            }
            if (Input.GetAxis("DpadLeft") != dpadSideState)
            {
                dpadSideState = (int)Input.GetAxis("DpadLeft");
                if (dpadSideState == 1)
                {
                    right = true;
                }
                else if (dpadSideState == -1)
                {
                    left = true;
                }
                sidePress = dpadSideState;
            }
        }

        //keyboard control part
        if (!Continue(up, down, left, right))
        {

            if (Input.GetKeyDown("up"))
            {
                down = false;
                up = true;
                upPress = 1;
            }
            else if (Input.GetKeyDown("down"))
            {
                up = false;
                down = true;
                upPress = -1;
            }
            if (Input.GetKeyDown("left"))
            {
                right = false;
                left = true;
                sidePress = -1;
            }
            else if (Input.GetKeyDown("right"))
            {
                left = false;
                right = true;
                sidePress = 1;
            }
        }
    }

    //Checks if an action should continue
    bool Continue(params bool[] args)
    {
        foreach (bool arg in args)
        {
            if (arg)
            {
                return true;
            }
        }
        return false;
    }

    //Resets all button presses
    public void Reset()
    {
        right = false;
        left = false;
        up = false;
        down = false;
    }
}