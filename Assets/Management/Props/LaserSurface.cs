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

	private List<Item> remainingBodyParts;
	private List<Item> remainingConeParts;
	private List<Item> remainingHiddenParts;


	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	public override void Interact(PlayerCharacter character)
	{
	}

	private bool TryAddItem(Item item)
	{
		foreach (Item req in remainingBodyParts)
		{
			//if(item)
		}
		return false;
	}
}
