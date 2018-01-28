using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayer : MonoBehaviour
{
	[SerializeField]
	private Text text;
	private string defaultText;

	[SerializeField]
	private int playerId;


	void Start ()
	{
		defaultText = text.text;
	}
	
	void Update ()
	{
		if (PlayerController.players[playerId].isInUse)
			text.text = "Player " + playerId + " Joined";
		else
			text.text = defaultText;

	}
}
