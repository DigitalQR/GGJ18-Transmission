using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputProfile))]
public class PlayerController : MonoBehaviour
{
	private static List<PlayerController> _players = new List<PlayerController>();
	public static List<PlayerController> players { get { return _players; } private set { _players = value; } }


	public InputProfile inputProfile { get { return _inputProfile; } }
	private InputProfile _inputProfile;
	public bool isInUse { get { return inputProfile.isBound; } }

	/// <summary>
	/// The player prefab that will be spawned for this controller by default
	/// </summary>
	[SerializeField]
	private PlayerCharacter defaultPlayerPrefab;
	
	public PlayerCharacter character { get { return _character; } }
	private PlayerCharacter _character;


	void Start ()
	{
		_inputProfile = GetComponent<InputProfile>();
		DontDestroyOnLoad(gameObject);

		players.Add(this);
	}

	void OnDestroy()
	{
		players.Remove(this);
	}
	

	/// <summary>
	/// Attempt to spawn in a new character for this controller
	/// </summary>
	/// <param name="location">Where to spawn the controller in</param>
	/// <returns>False if failed to spawn in the character</returns>
	public bool Spawn(Vector3 location)
	{
		if (_character != null)
			return false;

		_character = Instantiate(defaultPlayerPrefab.gameObject).GetComponent<PlayerCharacter>();
		_character.transform.position = location;
		_character.OnSpawn(this);
		return true;
	}

}
