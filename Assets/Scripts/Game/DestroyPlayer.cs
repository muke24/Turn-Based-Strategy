#region This code has been written by Peter Thompson
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		//* Destroys whichever player loses the game and enters trigger
		//* incase an error comes up saying the gameobject is outside of the scene bounds
		Debug.Log(other.name + " has been destroyed");
		Destroy(other.gameObject);
	}
}
//* This code has been written by Peter Thompson
#endregion