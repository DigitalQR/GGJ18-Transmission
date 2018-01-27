﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceSurface : InteractableBehaviour
{
	private Item currentRecipe;
	private Queue<Item> outputItems = new Queue<Item>();

	[SerializeField]
	private float totalCookTime = 3.0f;
	private float workTime;


	public override void Interact(PlayerCharacter character)
	{
		// Attempt to smelt item
		if (character.heldItem.SmeltOutput.Length != 0 && currentRecipe == null && outputItems.Count == 0)
		{
			currentRecipe = character.heldItem;
			currentRecipe.gameObject.SetActive(false);
			character.heldItem = null;
			workTime = totalCookTime;
		}
	}

	void Update()
	{
		// Work on current recipe if active
		if (currentRecipe != null)
		{
			workTime -= Time.deltaTime;

			// Enque outputs
			if (workTime <= 0.0f)
			{
				foreach (Item item in currentRecipe.SmeltOutput)
					outputItems.Enqueue(item);

				Destroy(currentRecipe.gameObject);
				currentRecipe = null;
			}
		}

		// Post output
		// TODO - Put on conveyor
		if (outputItems.Count != 0)
		{
			Instantiate(outputItems.Dequeue().gameObject);
		}
	}
}
