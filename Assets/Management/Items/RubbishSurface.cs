using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishSurface : InteractableBehaviour
{
	public override void Interact(PlayerCharacter character)
	{
		if (character.heldItem != null)
		{
			Destroy(character.heldItem.gameObject);
			character.heldItem = null;
		}
	}
}
