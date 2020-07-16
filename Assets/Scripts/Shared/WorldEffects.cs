using UnityEngine;

public class WorldEffects : MonoBehaviour
{
	[SerializeField]
	private bool lerpColour = false;
	[SerializeField]
	private float rotationSpeed = 2f;

	[SerializeField]
	[Range(0.700f, 0.708f)]
	private float skyboxColour = 0.708f;

	private float lastFloat;
	private float yRot = 0f;
	private readonly float minRange = 0.700f;
	private readonly float maxRange = 0.708f;

	private bool increaseValue = true;

	void Awake()
	{
		lastFloat = skyboxColour;
	}

	// Update is called once per frame
	void Update()
	{
		// Rotate skybox camera
		yRot += Time.deltaTime * rotationSpeed;
		transform.rotation = Quaternion.Euler(0, yRot, 0);

		// Change skybox colour (this works as the far clip plane can only see certain colours if its set lower than 1)
		if (!lerpColour)
		{
			if (lastFloat != skyboxColour)
			{
				ChangeColour();
				lastFloat = skyboxColour;
			}
		}
		else
		{
			ChangeColour();

			if (increaseValue)
			{
				skyboxColour += 0.01f * Time.deltaTime;

				if (skyboxColour >= maxRange)
				{
					increaseValue = false;
				}
			}
			else
			{
				skyboxColour -= 0.01f * Time.deltaTime;

				if (skyboxColour <= minRange)
				{
					increaseValue = true;
				}
			}
		}
	}

	void ChangeColour()
	{
		GetComponent<Camera>().farClipPlane = skyboxColour;
	}
}
