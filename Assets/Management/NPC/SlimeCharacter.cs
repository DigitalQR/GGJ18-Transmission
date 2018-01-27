using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCharacter : MonoBehaviour
{
	public Transform mDisplayLocation;

	[SerializeField]
	private float moveSpeed = 1.0f;

	private Item target;
	private enum AiState
	{
		Wandering, Hunting, Fleeing,
	}
	private AiState aiState;

	
	void Start ()
	{
		AquireTarget();
	}
	

	void Update ()
	{
		switch (aiState)
		{
			case AiState.Wandering:
				{
					break;
				}

			case AiState.Hunting:
				{
					Vector3 dir = target.transform.position - transform.position;
					dir.y = 0;

					// Check if close enough to grab
					if (dir.sqrMagnitude <= 1.0f)
					{
						target.Place(mDisplayLocation);
						aiState = AiState.Fleeing;
						break;
					}

					dir.Normalize();
					transform.Translate(dir * Time.deltaTime * moveSpeed);
					break;
				}

			case AiState.Fleeing:
				{
					break;
				}
		}
	}

	private bool AquireTarget()
	{
		List<Item> targets = new List<Item>();

		foreach (Item item in FindObjectsOfType<Item>())
		{
			Debug.Log(item.transform.parent.tag);
			if (item.isDropped || !item.transform.parent.CompareTag("Enemy"))
				targets.Add(item);
		}

		if (targets.Count == 0)
			return false;


		target = targets[Random.Range(0, targets.Count)];
		aiState = AiState.Hunting;
		return true;
	}
}
