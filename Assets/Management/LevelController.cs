using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

	void Start ()
	{
		foreach(PlayerController player in PlayerController.players)
			if (player.isInUse)
				player.Spawn(new Vector3(Random.Range(-3.0f, 3.0f), 5.0f, Random.Range(-3.0f, 3.0f)));
	}
	
	void Update ()
	{
		
	}
}
