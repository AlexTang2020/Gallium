/* Author: Alvaro Gudiswitz
 * Date Created: 3/29/2018
 * Date Modified: 4/17/2018
 * Modified By: Alvaro Gudiswitz
 * Description: Uses correct controller for joystick inputs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour {

    //type of controller:
    private string controllerType = "keyboard";

    //Get the type of controller being used
    void Start ()
    {
        foreach (string joystick in Input.GetJoystickNames())
        {
            if (joystick != "")
            {
                switch (joystick)
                {
                    case "Wireless Controller":
                        controllerType = "ps4";
                        break;
                    default:
                        controllerType = "xbox";
                        break;
                }
                break;
            }
        }
        print(controllerType);
    }
	
	// Returns axis given axis.
	public float GetAxis(string axis)
    {
        switch(axis)
        {
            case "RightHorizontal":
                switch(controllerType)
                {
                    case "xbox":
                        return Input.GetAxis("RightHorizontalXbox");
                    case "ps4":
                        return Input.GetAxis("RightHorizontalPS4");
                    case "keyboard":
                        return Input.GetAxis("RightHorizontalPC");
                    default:
                        print("err");
                        return 0;
                }
            case "RightVertical":
                switch (controllerType)
                {
                    case "xbox":
                        return Input.GetAxis("RightVerticalXbox");
                    case "ps4":
                        return Input.GetAxis("RightVerticalPS4");
                    case "keyboard":
                        return Input.GetAxis("RightVerticalPC");
                    default:
                        print("err");
                        return 0;
                }
            default:
                return Input.GetAxisRaw(axis);
        }
    }
}
