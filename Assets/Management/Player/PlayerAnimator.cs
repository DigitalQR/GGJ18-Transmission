using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    Animator anim;
    bool carrying;
    bool moving;
    int movingHash = Animator.StringToHash("Moving");
    int carryingHash = Animator.StringToHash("Carrying");

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetBool(carryingHash, carrying);
        if (GetComponentInParent<PlayerCharacter>().mVelocity.magnitude > 0.5f)
        {
            moving = true;
            Debug.Log("moving");
        }
        if (GetComponentInParent<PlayerCharacter>().mVelocity.magnitude < 0.5f)
        {
            moving = false;
            Debug.Log("idle");
        }
        {
            carrying = true;
        }
        if (GetComponentInParent<PlayerCharacter>().heldItem == null)
            carrying = false;
*/
        if (Input.GetKeyDown(KeyCode.J))        
            anim.SetBool(movingHash, true);
        if (Input.GetKeyDown(KeyCode.K))
            anim.SetBool(movingHash, false);
        if (Input.GetKeyDown(KeyCode.N))
            anim.SetBool(carryingHash, true);
        if (Input.GetKeyDown(KeyCode.M))
            anim.SetBool(carryingHash, false);        
	}
}
