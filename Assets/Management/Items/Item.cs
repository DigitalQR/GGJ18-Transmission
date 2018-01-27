using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
	private Rigidbody mBody;
	private bool _isDropped = true;
	public bool isDropped { get { return _isDropped; } }

	public bool physicsEnabled { get { return mBody.detectCollisions; } }


	void Start ()
	{
		mBody = GetComponent<Rigidbody>();	
	}

	/// <summary>
	/// Drops this item, so that is may freely interact
	/// </summary>
	public void Drop()
	{
		if (!_isDropped)
		{
			mBody.detectCollisions = true;
			transform.parent = null;
			_isDropped = true;
		}
	}

	/// <summary>
	/// Place the item under this transform
	/// </summary>
	public void Place(Transform trans)
	{
		transform.parent = trans;
		transform.localPosition = new Vector3(0, 0, 0);
		transform.localRotation = Quaternion.identity;
		_isDropped = false;
	}

}
