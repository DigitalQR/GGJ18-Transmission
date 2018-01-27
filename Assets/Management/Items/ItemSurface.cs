using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSurface : InteractableBehaviour
{
	public Transform mDisplayLocation;
	private Item mHeldItem;


	public override void Interact(PlayerCharacter character)
	{
		// Pickup item off surface
		if (character.heldItem == null)
		{
			if (mHeldItem != null)
			{
				character.heldItem = mHeldItem;
				character.heldItem.transform.parent = character.itemHoldLocation;
				character.heldItem.transform.localPosition = new Vector3(0, 0, 0);
				character.heldItem.transform.localRotation = Quaternion.identity;
				mHeldItem = null;
			}
		}
		// Place item on surface
		else if(mHeldItem == null)
		{
			mHeldItem = character.heldItem;
			character.heldItem = null;

			mHeldItem.transform.parent = mDisplayLocation;
			mHeldItem.transform.localPosition = new Vector3(0, 0, 0);
			mHeldItem.transform.rotation = Quaternion.identity;
		}
	}
}
