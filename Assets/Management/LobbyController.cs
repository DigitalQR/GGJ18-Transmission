using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
	[SerializeField]
	private PlayerController defaultPlayerController;
	[SerializeField]
	private int maxPlayerCount = 4;

	private static bool staticInit = false;


	void Start ()
	{
		if (!staticInit)
		{
			for (int i = 0; i < maxPlayerCount; ++i)
				Instantiate(defaultPlayerController.gameObject);

			staticInit = true;
		}
	}

	public void SwitchLevel(string level)
	{
		SceneManager.LoadScene(level);
	}
	
}
