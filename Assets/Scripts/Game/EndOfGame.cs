#region This code has been written by Peter Thompson
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{
	/// <summary>
	/// Reloads the scene to play from start
	/// </summary>
    public void Replay()
	{
		SceneManager.LoadScene(1);
	}

	/// <summary>
	/// Goes back to the main menu
	/// </summary>
	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
//* This code has been written by Peter Thompson
#endregion