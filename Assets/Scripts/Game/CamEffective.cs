#region This code has been written by Peter Thompson
using UnityEngine;

public class CamEffective : MonoBehaviour
{
	//* Disabled the warning as the script is on the main camera, 
	//* and the camera animation calls the void as an animation event
	#pragma warning disable IDE0051 // Remove unused private members
	void StopCamAnim()
	#pragma warning restore IDE0051 // Remove unused private members
	{
		GetComponent<Animator>().SetBool("Animate", false);
	}
}
//* This code has been written by Peter Thompson
#endregion