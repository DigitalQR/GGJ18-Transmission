using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSurface : InteractableBehaviour
{
	public override void Interact(PlayerCharacter character)
	{
		Debug.Log("Woot " + gameObject.name);
	}
}
