using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInteraction : MonoBehaviour
{
	/// <summary>
	/// This object has been hit by a laser
	/// </summary>
	/// <param name="source">The game object from which this laser came from</param>
	public virtual void LaserInteract(GameObject source)
	{
	}
}
