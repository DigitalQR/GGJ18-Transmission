using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
	[SerializeField]
	private int _ID = -1;
	public int ID { get { return _ID; } }

	private Rigidbody _mBody;
	private Rigidbody mBody
	{
		get
		{
			if (_mBody == null)
				_mBody = GetComponent<Rigidbody>();
			return _mBody;
		}
	}

	private bool _isDropped = true;
	public bool isDropped { get { return _isDropped; } }

	public bool physicsEnabled { get { return mBody.detectCollisions; } }


	public Item[] SmeltOutput;
	public Item[] HammerOutput;
	public Item[] ChiselOutput;

	
	void Start()
	{
		if(ID == -1)
			Debug.LogWarning("Item " + name + " has no ID assigned");
	}

	/// <summary>
	/// Drops this item, so that is may freely interact
	/// </summary>
	public void Drop()
	{
		if (!_isDropped)
		{
			mBody.detectCollisions = true;
			mBody.isKinematic = false;
			transform.parent = null;
			_isDropped = true;
		}
	}

	/// <summary>
	/// Place the item under this transform
	/// </summary>
	public void Place(Transform trans)
	{
		mBody.detectCollisions = false;
		mBody.isKinematic = true;

		transform.parent = trans;
		transform.localPosition = new Vector3(0, 0, 0);
		transform.localRotation = Quaternion.identity;
		_isDropped = false;
	}

}
