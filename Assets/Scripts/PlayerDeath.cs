using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public throwhook hookThrowScript;
    public Animator anim;
    // Start is called before the first frame update
    public void Respawn() {
		hookThrowScript.Respawn();
        anim.SetBool("isDead", false);
	}
}
