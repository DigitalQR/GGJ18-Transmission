using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSurface : InteractableBehaviour
{
	[SerializeField]
	private GameObject[] BodyParts;
	[SerializeField]
	private GameObject[] ConeParts;

	[SerializeField]
	private Item[] RequiredBodyParts;
	[SerializeField]
	private Item[] RequiredConeParts;
	[SerializeField]
	private Item[] RequiredHiddenParts;

	private List<Item> remainingBodyParts = new List<Item>();
	private List<Item> remainingConeParts = new List<Item>();
	private List<Item> remainingHiddenParts = new List<Item>();


	void Start ()
	{
		remainingBodyParts.AddRange(RequiredBodyParts);
		remainingConeParts.AddRange(RequiredConeParts);
		remainingHiddenParts.AddRange(RequiredHiddenParts);
	}
	
	void Update ()
	{
		int visibleBodyParts = (RequiredBodyParts.Length - remainingBodyParts.Count) * BodyParts.Length / RequiredBodyParts.Length;
		int visibleConeParts = (RequiredConeParts.Length - remainingConeParts.Count) * ConeParts.Length / RequiredConeParts.Length;

		for (int i = 0; i < BodyParts.Length; ++i)
			BodyParts[i].SetActive(i < visibleBodyParts);

		for (int i = 0; i < ConeParts.Length; ++i)
			ConeParts[i].SetActive(i < visibleConeParts);
	}

	public override void Interact(PlayerCharacter character)
	{
		if (character.heldItem != null && TryAddItem(character.heldItem))
		{
			Destroy(character.heldItem.gameObject);
			character.heldItem = null;
		}
	}

	private bool TryAddItem(Item item)
	{
		foreach (Item req in remainingBodyParts)
		{
			if (req.ID == item.ID)
			{
				remainingBodyParts.Remove(req);
				return true;
			}
		}

		foreach (Item req in remainingConeParts)
		{
			if (req.ID == item.ID)
			{
				remainingConeParts.Remove(req);
				return true;
			}
		}

		foreach (Item req in remainingHiddenParts)
		{
			if (req.ID == item.ID)
			{
				remainingHiddenParts.Remove(req);
				return true;
			}
		}
		return false;
	}
}
