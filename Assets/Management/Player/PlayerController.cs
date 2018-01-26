using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputProfile))]
public class PlayerController : MonoBehaviour
{
	public InputProfile inputProfile { get { return _inputProfile; } }
	private InputProfile _inputProfile;

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

		Spawn(new Vector3(0, 6, 0));
	}
	

	/// <summary>
	/// Attempt to spawn in a new character for this controller
	/// </summary>
	/// <param name="location">Where to spawn the controller in</param>
	/// <returns>False if failed to spawn in the character</returns>
	bool Spawn(Vector3 location)
	{
		if (_character != null)
			return false;

		_character = Instantiate(defaultPlayerPrefab.gameObject).GetComponent<PlayerCharacter>();
		_character.transform.position = location;
		_character.OnSpawn(this);
		return true;
	}

}
