using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : InteractableBehaviour
{
	public Item targetItem;
	public ItemSurface targetSurface;


	public override void Interact(PlayerCharacter character)
	{
		Debug.Log("A");
		if (!targetSurface.holdingItem)
		{
			Debug.Log("B");
			Item item = Instantiate(targetItem.gameObject).GetComponent<Item>();
			if (!targetSurface.AttemptPlace(item))
			{
				Debug.Log("C");
				Destroy(item.gameObject);
			}
			else

				Debug.Log("D");
		}
	}
}
