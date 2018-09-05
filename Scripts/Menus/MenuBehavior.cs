/* Author: Alvaro Gudiswitz
 * Date Created: 4/3/2018 
 * Date Modified: 4/19/2018
 * Modified By: Alvaro Gudiswitz
 * Description: Easy script for buttons to jump between scenes
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuBehavior : MonoBehaviour
{
    //Changing Scenes using index in build settings
    public void LoadByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    //Changing Scenes using the scene name
    public void LoadByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    //Quit Application
    public void quit()
    {
        print("Quitting Game");
        Application.Quit();
    }
}
