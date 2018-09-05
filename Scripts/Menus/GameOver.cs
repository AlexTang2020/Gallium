/* Author: Alexander Tang
 * Date Created: 4/5/2018
 * Date Modified: 4/28/2018
 * Modified By: Alexander Tang
 * Description: Change in-game text between "Game over" or "You Win"
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    [Header("Set in Inspector")]
    public Text endText;    //Text for Game Over Scene

    // Use this for initialization
    void Start () {
        endText = GetComponent<Text>();
        if (EnemyHP.bossHP == 0)
        {
            endText.text = "You Win";
        }
        else
        {
            endText.text = "Game Over";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
