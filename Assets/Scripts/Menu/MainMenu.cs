#region This code has been written by Peter Thompson
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}
	public void QuitGame()
	{
		#if UNITY_EDITOR
			Debug.Log("Play mode has been exited");
			UnityEditor.EditorApplication.isPlaying = false;			
		#else
			Application.Quit();
		#endif
	}
}
//* This code has been written by Peter Thompson
#endregion