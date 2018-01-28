using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCharacter : MonoBehaviour
{
	public Transform mDisplayLocation;

	[SerializeField]
	private float moveSpeed = 1.0f;
	
	private enum AiState
	{
		Wandering, Hunting, Fleeing,
	}
	private AiState aiState = AiState.Wandering;

	private float wanderCooldown = 0.0f;
	private Vector2 wanderTarget;

	private static HashSet<Item> reservedItems = new HashSet<Item>();
	private Item target;


	void Start ()
	{
	}

	void OnDestroy()
	{
		if(target != null)
			reservedItems.Remove(target);
	}
	

	void Update ()
	{
		switch (aiState)
		{
			case AiState.Wandering:
				{
					wanderCooldown -= Time.deltaTime;

					// Attempt to aquire new target, if not just wander
					if (wanderCooldown <= 0)
					{
						if (AquireTarget())
							return;
						else
						{
							// Wander some more
							wanderTarget = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-5.0f, 5.0f)); // TODO - Remove hard code
							wanderCooldown = 5.0f + Random.Range(0.0f, 5.0f);
						}
					}

					// Walk to location
					Vector3 dir = new Vector3(wanderTarget.x, 0, wanderTarget.y) - transform.position;
					dir.y = 0;

					// Don't move if too close
					if (dir.sqrMagnitude > 1.0f)
					{
						dir.Normalize();
						transform.Translate(dir * Time.deltaTime * moveSpeed);
					}
					break;
				}

			case AiState.Hunting:
				{
					// Item can no longer be reached
					if (target == null || !target.isActiveAndEnabled)
					{
						aiState = AiState.Wandering;
						return;
					}

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
			if ((item.isDropped || !item.transform.parent.CompareTag("Enemy")) && !reservedItems.Contains(item))
				targets.Add(item);
		}

		if (targets.Count == 0)
			return false;


		target = targets[Random.Range(0, targets.Count)];
		reservedItems.Add(target);
		aiState = AiState.Hunting;
		return true;
	}
}
