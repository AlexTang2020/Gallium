/* Author: Alvaro Gudiswitz
 * Date Created: 4-17-18
 * Date Modified: 
 * Modified By: 
 * Description: Navigate menus with joystick
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNav : MonoBehaviour
{
    [Header("Set in Inspector")]
    public string lastMenu;     //Name of last menu option

    public Button[] buttonsRight;   //Set of buttons on the right side
    public Button[] buttonsLeft;    //Set of buttons on the left side
    int selectedHeight = 0;
    int selectedSide = 0;
    Button[,] buttonsToSelect;
    DpadConversion buttonner;

    // Use this for initialization
    void Start()
    {
        buttonsToSelect = new Button[Mathf.Max(buttonsRight.Length, buttonsLeft.Length), 2];
        gameObject.AddComponent<DpadConversion>();

        //fill buttons
        for (int x = 0; x < buttonsRight.Length; x++)
        {
            buttonsToSelect[x, 0] = buttonsLeft[x];
            buttonsToSelect[x, 1] = buttonsRight[x];
        }
    }

    //Selecting b/t things
    void Update()
    {
        selectedHeight -= gameObject.GetComponent<DpadConversion>().upPress;
        if (selectedHeight == -1)
            selectedHeight += buttonsRight.Length;
        selectedHeight %= buttonsRight.Length;

        selectedSide -= gameObject.GetComponent<DpadConversion>().sidePress;
        if (selectedSide == -1)
            selectedSide += 2;
        selectedSide %= 2;

        buttonsToSelect[selectedHeight, selectedSide].Select();
        if (Input.GetButtonDown("MenuSelect"))
        {
            buttonsToSelect[selectedHeight, selectedSide].onClick.Invoke();
        }

        if (Input.GetButtonDown("MenuBack"))
        {
            if (lastMenu != "")
            {
                SceneManager.LoadScene(lastMenu);
            }
            else
            {
                print("exit");
                Application.Quit();
            }
        }
    }
}