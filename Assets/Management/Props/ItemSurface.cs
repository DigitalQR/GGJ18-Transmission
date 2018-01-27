using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSurface : InteractableBehaviour
{
	public Transform mDisplayLocation;
	private Item mHeldItem;
	public bool holdingItem { get { return mHeldItem != null; } }


	public override void Interact(PlayerCharacter character)
	{
		// Pickup item off surface
		if (character.heldItem == null)
		{
			if (mHeldItem != null)
			{
				character.heldItem = mHeldItem;
				character.heldItem.Place(character.itemHoldLocation);
				mHeldItem = null;
			}
		}
		// Place item on surface
		else if(mHeldItem == null)
		{
			mHeldItem = character.heldItem;
			character.heldItem = null;
			mHeldItem.Place(mDisplayLocation);
		}
	}

	protected virtual void OnCollisionEnter(Collision coll)
	{
		// Attempt to place item on this surface
		if (coll.gameObject.CompareTag("Item") && mHeldItem == null)
		{
			mHeldItem = coll.gameObject.GetComponent<Item>();
			mHeldItem.Place(mDisplayLocation);
		}
	}

	/// <summary>
	/// Attempt to place the item on this surface
	/// </summary>
	/// <param name="item"></param>
	/// <returns>If item gets placed corrected</returns>
	public bool AttemptPlace(Item item)
	{
		if (mHeldItem == null)
		{
			mHeldItem = item;
			mHeldItem.Place(mDisplayLocation);
			return true;
		}

		return false;
	}
}
