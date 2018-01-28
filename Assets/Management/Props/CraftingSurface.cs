using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSurface : InteractableBehaviour
{
	public enum CraftTool
	{
		Hammer, Chisel
	}

	[SerializeField]
	private CraftTool mToolType;
	private float hitCooldown;
	private int craftProgress;

	public Transform mDisplayLocation;
	private Item mHeldItem;
	public bool holdingItem { get { return mHeldItem != null; } }

	
	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (hitCooldown > 0.0f)
		{
			hitCooldown -= Time.deltaTime;
			if (hitCooldown < 0.0f)
				hitCooldown = 0.0f;
		}
	}

	public override void Interact(PlayerCharacter character)
	{
		// Place item on surface
		if (mHeldItem == null)
		{
			if (
				character.heldItem != null && (
				(mToolType == CraftTool.Chisel && character.heldItem.ChiselOutput.Length != 0) ||
				(mToolType == CraftTool.Hammer && character.heldItem.HammerOutput.Length != 0)
				)
			)
			{
				mHeldItem = character.heldItem;
				character.heldItem = null;
				mHeldItem.Place(mDisplayLocation);
				mHeldItem.enabled = false;
				craftProgress = 0;
			}
		}

		// Craft current item
		else
		{
			// Cannot craft if holding item
			if (character.heldItem != null)
				return;

			if (hitCooldown == 0.0f)
			{
				// Working on crafting
				if (craftProgress < 4)
				{
					hitCooldown = 0.5f;
					++craftProgress;
				}
				// Give result
				else
				{
					Item item = Instantiate(mToolType == CraftTool.Chisel ? mHeldItem.ChiselOutput[0] : mHeldItem.HammerOutput[0]).GetComponent<Item>();
					character.heldItem = item;
					character.heldItem.Place(character.itemHoldLocation);

					Destroy(mHeldItem.gameObject);
					mHeldItem = null;
				}
			}
		}

	}
}
