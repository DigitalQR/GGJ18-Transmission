using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHandles : InteractableBehaviour
{
	[SerializeField]
	private Transform laserBase;
	[SerializeField]
	private Transform laserBody;
	private PlayerCharacter currentGrabbed;

	private Quaternion bodyStartRotation;


	void Start()
	{
		bodyStartRotation = laserBody.rotation;
	}

	void Update ()
	{
		if (currentGrabbed != null)
		{
			Vector3 direction = laserBase.position - currentGrabbed.transform.position;
			direction.y = 0;
			laserBody.forward = direction;
			laserBody.rotation = laserBody.rotation * bodyStartRotation;
		}
	}

	public override void Interact(PlayerCharacter character)
	{
		currentGrabbed = character;
	}

	void OnTriggerExit(Collider coll)
	{
		if (currentGrabbed != null && coll.gameObject == currentGrabbed.gameObject)
			currentGrabbed = null;
	}
}
