using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : InteractableBehaviour
{
	public Item targetItem;
	public ItemSurface targetSurface;


	public override void Interact(PlayerCharacter character)
	{
		if (!targetSurface.holdingItem)
		{
			Item item = Instantiate(targetItem.gameObject).GetComponent<Item>();
			if (!targetSurface.AttemptPlace(item))
				Destroy(item.gameObject);
		}
	}
}
