using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwivelHandles : InteractableBehaviour
{
	[SerializeField]
	private Transform baseModel;
	[SerializeField]
	private Transform bodyModel;
	private PlayerCharacter currentGrabbed;

	private Quaternion bodyStartRotation;
	[SerializeField]
	private Quaternion rotationOffset = Quaternion.identity;


	void Start()
	{
		bodyStartRotation = bodyModel.rotation;
	}

	void Update()
	{
		if (currentGrabbed != null)
		{
			Vector3 direction = baseModel.position - currentGrabbed.transform.position;
			direction.y = 0;
			bodyModel.forward = direction;
			bodyModel.rotation = bodyModel.rotation * bodyStartRotation * rotationOffset;
		}
	}

	public override void Interact(PlayerCharacter character)
	{
		if (currentGrabbed == character)
			currentGrabbed = null;
		else
			currentGrabbed = character;
	}

	void OnTriggerExit(Collider coll)
	{
		if (currentGrabbed != null && coll.gameObject == currentGrabbed.gameObject)
			currentGrabbed = null;
	}
}
