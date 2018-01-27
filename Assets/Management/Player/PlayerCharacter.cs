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
	public InteractableBehaviour currentInteraction;
	[System.NonSerialized]
	public Item heldItem;


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
			if (mVelocity.sqrMagnitude > mMaxVelocity)
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
			if (currentInteraction != null)
			{
				if (inputProfile.GetKeyPressed(InputProfile.Button.A))
					currentInteraction.Interact(this);

				currentInteraction = null; // Clear current interaction (Will get set each frame if valid)
			}
			else
			{
				// Attempt to pickup nearby object/drop held
				if (inputProfile.GetKeyPressed(InputProfile.Button.A))
				{
					// Pickup
					if (heldItem == null)
					{
						foreach (Item item in FindObjectsOfType<Item>())
						{
							// Only check items on ground
							if (item.physicsEnabled)
							{
								float sqrDistance = (item.transform.position - transform.position).sqrMagnitude;

								if (sqrDistance <= 1.5f * 1.5f)
								{
									heldItem = item;
									item.DisablePhysics();
									item.transform.parent = transform;
									// TODO - Put in players hands
									break;
								}
							}
						}
					}
					// Drop
					else
					{
						heldItem.EnablePhysics();
						heldItem.transform.parent = null;
						heldItem = null;
					}
				}
			}
		}

	}
}
