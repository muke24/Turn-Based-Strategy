using UnityEngine;
using UnityEngine.UI;

public class PlayerNameText : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
		Text usernameText = GetComponent<Text>();

		if (Username.username != "")
		{
			usernameText.text = Username.username;
		}
		else
		{
			usernameText.text = "Player 1";
			Username.username = "Player 1";			
		}

		if (usernameText.text == "")
		{
			usernameText.text = "Player 1";
		}
    }
}
