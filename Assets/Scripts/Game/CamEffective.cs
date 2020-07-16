using UnityEngine;

public class CamEffective : MonoBehaviour
{
#pragma warning disable IDE0051 // Remove unused private members
    void StopCamAnim()
#pragma warning restore IDE0051 // Remove unused private members
	{
		GetComponent<Animator>().SetBool("Animate", false);
	}
}
