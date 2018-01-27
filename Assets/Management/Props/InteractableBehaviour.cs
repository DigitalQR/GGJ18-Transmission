using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehaviour : MonoBehaviour
{
	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			PlayerCharacter player = collider.GetComponent<PlayerCharacter>();
			if (player != null)
				player.currentInteraction = this;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			PlayerCharacter player = collider.GetComponent<PlayerCharacter>();
			if (player != null && player.currentInteraction == this)
				player.currentInteraction = null;
		}
	}

	/// <summary>
	/// Character has attempted to interact with this object
	/// </summary>
	public virtual void Interact(PlayerCharacter character)
	{
	}
}
