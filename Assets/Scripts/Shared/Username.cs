#region This code has been written by Peter Thompson
using UnityEngine;
using UnityEngine.UI;

public class Username : MonoBehaviour
{
    public static string username;

	private static Username _username;

	private void Awake()
	{
		//* Singleton

		if (_username == null)
		{
			_username = this;
		}
		else
		{
			Destroy(this);
		}

		DontDestroyOnLoad(gameObject);
	}

	/// <summary>
	/// Changes the player's username to the inputfield's text
	/// </summary>
	public void ChangeName()
	{
		username = GameObject.Find("PlayerNameInputField").GetComponent<InputField>().text;
	}
}
//* This code has been written by Peter Thompson
#endregion