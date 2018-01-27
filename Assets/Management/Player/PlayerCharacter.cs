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

	public float mVelocityPersistance = 0.9f;
	public float mMaxVelocity = 15.0f;
	private Vector3 mVelocity;


	[System.NonSerialized]
	public InteractableBehaviour currentInteraction;


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
			mVelocity *= mVelocityPersistance;
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
		}
	}
}
