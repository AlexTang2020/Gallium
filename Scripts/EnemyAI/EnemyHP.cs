/* Author: Alexander Tang
 * Date Created: 3/31/2018
 * Date Modified: 4/5/2018
 * Modified By: Alexander Tang
 * Description: Initially used for enemy and boss HP
 *              Boss HP script covers new boss behavior
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour {
    [Header("Set in Inspector")]
    public static int bossHP = 7;   //Boss' starting HP
    [Header("Set Dynamically")]
    public static int bossForm = 0;  //Boss' initial form
}
