using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class LaserMirror : LaserInteraction
{
	private bool hitThisFrame = false;
	private LineRenderer mLineRenderer;

	private Vector3 laserStart;
	private Vector3 laserDirection;


	void Start()
	{
		mLineRenderer = GetComponent<LineRenderer>();
	}

	void Update()
	{
		Debug.DrawLine(transform.position, transform.position + transform.right);

		mLineRenderer.enabled = hitThisFrame;

		// Update whether it's hit or not
		if (hitThisFrame)
		{
			// Fire beam
			mLineRenderer.SetPosition(0, laserStart);
			RaycastHit hit;
			Ray ray = new Ray(laserStart, laserDirection);


			if (Physics.Raycast(ray, out hit, 100.0f))
			{
				LaserInteraction interaction = hit.collider.GetComponent<LaserInteraction>();
				if (interaction != null)
					interaction.LaserInteract(gameObject, ray, hit);

				mLineRenderer.SetPosition(1, hit.point);
			}
			else
				mLineRenderer.SetPosition(1, laserStart + laserDirection * 100);


			hitThisFrame = false;
		}
	}

	public override void LaserInteract(GameObject source, Ray ray, RaycastHit hit)
	{
		if (Vector3.Dot(-transform.right, hit.normal) > 0.5f)
		{
			hitThisFrame = true;

			laserStart = hit.point;
			laserDirection = Vector3.Reflect(ray.direction, hit.normal);
		}
	}
}
