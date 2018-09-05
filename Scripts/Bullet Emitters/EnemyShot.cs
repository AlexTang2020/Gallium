/* Author: Aric Hasting
 * Date Created: 3/29/2018
 * Date Modified: 
 * Modified By: 
 * Description: Abstract class for enemy and bullet behavior
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyShot : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform bullet;    //bullet prefab

    protected float count;

	public bool firing = true;

	public abstract bool FireRate();    //Returns true when firing on this frame

    public abstract void Fire();        //actually shoots it. Will send vector3 over too.

}