using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTarget : LaserInteraction
{
	private bool hitThisFrame = false;

	[SerializeField]
	private Transform rotateAnimTarget;
	[SerializeField]
	private float rotationRate = 1.0f;

	[SerializeField]
	private float completeTime = 4.0f;
	private float completeTimer;


	void Start ()
	{
		
	}
	
	void Update ()
	{
		// Update whether it's hit or not
		if (hitThisFrame)
		{
			completeTimer += Time.deltaTime;
			
			if (completeTimer >= completeTime)
			{
				completeTimer = completeTime;
				LevelController.main.ReportWinCondition(gameObject);
			}

			hitThisFrame = false;
		}
		else
		{
			completeTimer -= Time.deltaTime;

			if (completeTimer <= 0.0f)
				completeTimer = 0.0f;
		}

		// Animate
		if (rotateAnimTarget != null)
			rotateAnimTarget.localRotation = Quaternion.AngleAxis(completeTimer * rotationRate, Vector3.up) * rotateAnimTarget.localRotation;
	}

	public override void LaserInteract(GameObject source)
	{
		hitThisFrame = true;
	}
}
