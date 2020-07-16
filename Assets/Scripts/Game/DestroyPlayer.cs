using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name + " has been destroyed");
		Destroy(other.gameObject);
	}
}