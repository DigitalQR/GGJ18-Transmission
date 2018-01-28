using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public static LevelController main { get; private set; }
	public bool isLevelActive { get; private set; }


	void Start ()
	{
		if (main != null)
			Debug.Log("Multiple level controllers found");
		else
			main = this;

		foreach(PlayerController player in PlayerController.players)
			if (player.isInUse)
				player.Spawn(new Vector3(Random.Range(-3.0f, 3.0f), 5.0f, Random.Range(-3.0f, 3.0f)));

		isLevelActive = true;
	}

	void OnDestroy()
	{
		if (main == this)
			main = null;
	}

	void Update ()
	{
		
	}


	public void ReportWinCondition(GameObject source)
	{
		if (isLevelActive)
		{
			isLevelActive = false;
			OnLevelComplete();
		}
	}

	void OnLevelComplete()
	{
		Debug.Log("Level finished");
	}
}
