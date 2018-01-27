using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	public bool flip = false;


	void Update ()
	{
		transform.rotation = Camera.main.transform.rotation;

		if (flip)
			transform.rotation = Quaternion.AngleAxis(180.0f, transform.up) * transform.rotation;
	}
}
