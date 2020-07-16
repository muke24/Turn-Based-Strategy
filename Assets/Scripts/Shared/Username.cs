using UnityEngine;
using UnityEngine.UI;

public class Username : MonoBehaviour
{
    public static string username;

	private static Username _username;

	private void Awake()
	{
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

	public void ChangeName()
	{
		username = GameObject.Find("PlayerNameInputField").GetComponent<InputField>().text;
	}
}
