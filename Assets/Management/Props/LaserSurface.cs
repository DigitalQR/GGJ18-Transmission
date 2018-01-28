using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class LaserSurface : InteractableBehaviour
{
	[Header("Visual Parts")]
	[SerializeField]
	private Transform BeamStart;
	[SerializeField]
	private GameObject[] BodyParts;
	[SerializeField]
	private GameObject[] ConeParts;

	[Header("Item Recipe")]
	[SerializeField]
	private Item[] RequiredBodyParts;
	[SerializeField]
	private Item[] RequiredConeParts;
	[SerializeField]
	private Item[] RequiredHiddenParts;

	private List<Item> remainingBodyParts = new List<Item>();
	private List<Item> remainingConeParts = new List<Item>();
	private List<Item> remainingHiddenParts = new List<Item>();
	public bool isBuilt { get { return remainingBodyParts.Count == 0 && remainingConeParts.Count == 0 && remainingHiddenParts.Count == 0; } }

	private LineRenderer mLineRenderer;


	void Start ()
	{
		remainingBodyParts.AddRange(RequiredBodyParts);
		remainingConeParts.AddRange(RequiredConeParts);
		remainingHiddenParts.AddRange(RequiredHiddenParts);
		mLineRenderer = GetComponent<LineRenderer>();

		UpdateModel();
	}
	
	void Update ()
	{
		if (isBuilt)
		{
			// Fire beam
			mLineRenderer.SetPosition(0, BeamStart.position);
			RaycastHit hit;

			if (Physics.Raycast(new Ray(BeamStart.position, BeamStart.forward), out hit, 100.0f))
			{
				LaserInteraction interaction = hit.collider.GetComponent<LaserInteraction>();
				if (interaction != null)
					interaction.LaserInteract(gameObject);

				mLineRenderer.SetPosition(1, hit.point);
			}
			else
				mLineRenderer.SetPosition(1, BeamStart.position + BeamStart.forward * 100);
		}
	}


	/// <summary>
	/// Update what part of the model is currently active
	/// </summary>
	private void UpdateModel()
	{
		int visibleBodyParts = (RequiredBodyParts.Length - remainingBodyParts.Count) * BodyParts.Length / RequiredBodyParts.Length;
		int visibleConeParts = (RequiredConeParts.Length - remainingConeParts.Count) * ConeParts.Length / RequiredConeParts.Length;

		for (int i = 0; i < BodyParts.Length; ++i)
			BodyParts[i].SetActive(i < visibleBodyParts);

		for (int i = 0; i < ConeParts.Length; ++i)
			ConeParts[i].SetActive(i < visibleConeParts);
		
		mLineRenderer.enabled = isBuilt;
	}

	public override void Interact(PlayerCharacter character)
	{
		if (character.heldItem != null && TryAddItem(character.heldItem))
		{
			Destroy(character.heldItem.gameObject);
			character.heldItem = null;
			UpdateModel();
		}
	}

	/// <summary>
	/// Attempt to add this item to the shell
	/// </summary>
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
