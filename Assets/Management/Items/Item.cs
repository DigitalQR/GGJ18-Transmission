using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
	private Rigidbody mBody;
	public bool physicsEnabled { get { return mBody.detectCollisions; } }


	void Start ()
	{
		mBody = GetComponent<Rigidbody>();	
	}

	public void EnablePhysics()
	{
		mBody.isKinematic = false;
	}
	public void DisablePhysics()
	{
		mBody.isKinematic = true;
	}

}
