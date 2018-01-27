using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour
{
	public PlayerController controller { get { return _controller; } }
	private PlayerController _controller;

	private CharacterController mCharacterController;
	public InputProfile inputProfile { get { return controller.inputProfile; } }


	public float mMoveSpeed = 10.0f;
	public float mGravityScale = 20.0f;

	public float mVelocityDecay = 0.1f;
	public float mMaxVelocity = 15.0f;
	private Vector3 mVelocity;

	
	[System.NonSerialized]
	public Item heldItem;
	public Transform itemHoldLocation;


	public void OnSpawn(PlayerController parent)
	{
		_controller = parent;
	}

	void Start ()
	{
		mCharacterController = GetComponent<CharacterController>();
	}

	void Update ()
	{
		// Update movement
		{
			mVelocity += new Vector3(inputProfile.GetInputVector().x, 0, inputProfile.GetInputVector().y) * mMoveSpeed * Time.deltaTime;

			// Decay velocity
			mVelocity *= Mathf.Clamp01(1.0f - mVelocityDecay * Time.deltaTime);
			// Cap speed
			if (mVelocity.sqrMagnitude > mMaxVelocity * mMaxVelocity)
			{
				mVelocity.Normalize();
				mVelocity *= mMaxVelocity;
			}

			// Rotate in direction of velocity
			if (mVelocity.sqrMagnitude > 0.01)
				transform.rotation = Quaternion.LookRotation(mVelocity.normalized);


			mCharacterController.Move(mVelocity * Time.deltaTime);

			// Constant gravity
			mCharacterController.Move(Vector3.down * mGravityScale * Time.deltaTime);
		}

		// Update interaction
		{
			// Attempt to interact
			if (inputProfile.GetKeyPressed(InputProfile.Button.A))
			{
				bool hasInteracted = false;

				// Find interaction infront of player
				foreach (Collider collider in Physics.OverlapBox(transform.position, new Vector3(0.2f, 5.0f, 0.5f), transform.rotation))
				{
					// Check to see if it's an interaction
					InteractableBehaviour interaction = collider.GetComponent<InteractableBehaviour>();
					if (interaction != null)
					{
						interaction.Interact(this);
						hasInteracted = true;
						break;
					}

					// Check to see if it's an item to pickup
					if (heldItem == null)
					{
						Item item = collider.GetComponent<Item>();
						// Only check items on ground
						if (item != null && item.physicsEnabled)
						{
							heldItem = item;
							heldItem.Place(itemHoldLocation);
							hasInteracted = true;
							break;
						}
					}
				}

				// Attempt to drop item in hands
				if (!hasInteracted && heldItem != null)
				{
					heldItem.Drop();
					heldItem = null;
				}
				
			}
		}

	}
}
