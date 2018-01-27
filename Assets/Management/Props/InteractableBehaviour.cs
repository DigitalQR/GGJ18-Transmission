using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehaviour : MonoBehaviour
{
	/// <summary>
	/// Character has attempted to interact with this object
	/// </summary>
	public virtual void Interact(PlayerCharacter character)
	{
	}
}
