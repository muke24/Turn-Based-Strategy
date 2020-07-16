using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[Header("Player Health")]
	public float maxHealth = 100f;
	public float curHealth = 100f;

	[Header("Player GameObject")]
	[Space(10)]
	public GameObject playerGameObject;

	[Header("Transforms To Parent Attacks/Blocks to")]
	[Space(10)]
	public Transform attackSpawnPos;
	public Transform blockSpawnPos;

	[Header("Attack/Block GameObject Prefabs")]
	[Space(10)]
	public GameObject attact1;
	public GameObject attact2;
	public GameObject block1;
	public GameObject block2;

	[Header("The Attack/Block That The Player Has Chosen")]
	[Space(10)]
	//* 1 is attack1, 2 is attack2
	public int enemysChosenAttack = 0;
	//* 1 is block1, 2 is block2
	public int enemysChosenBlock = 0;

	[Header("If The Attack The Enemy Just Used Was Effective")]
	[Space(10)]
	public bool wasEffective = false;

	[Header("Enemy's Health Slider")]
	[Space(10)]
	public Slider enemySlider;

	[Header("Damage Dealt From Each Attack")]
	[Space(10)]
	public float attack1Damage = 20f;
	public float attack2Damage = 30f;

	[Header("Time Between Each Play")]
	[Space(10)]
	[SerializeField]
	private float timerLength = 3f;

	[Header("Text To Display Whether The Attack/Block Was Effective")]
	[Space(10)]
	[SerializeField]
	private Text hitEffectiveText = null;

	//* Bool to check if the coroutine timer was just started
	private bool startTimer = true;
	//* Bool to check if player has won the game
	private bool won = false;

	/// <summary>
	/// Attacks the enemy with "Attack 1"
	/// </summary>
	public void Attack1()
	{
		//* Creates the attack prefab within the scene and parents it to the transform which is 
		//* already in the scene that has its position/rotation set to face the enemy
		Instantiate(attact1, attackSpawnPos);

		//* Assigns the enemy variable
		Enemy enemy = GetComponent<Enemy>();
		//* Sets the attack that the player just chose in the Enemy script
		enemy.playersChosenAttack = 1;
		//* Makes the enemy choose a random block
		enemy.ChooseBlock();

		//* Starts the coroutine timer
		startTimer = true;
		StartCoroutine("Timer");

		//* If the attack was effective then deal set damage
		if (enemy.wasEffective)
		{
			float randomisedDamage = Random.Range(-2.5f, 2.5f);
			enemy.curHealth -= randomisedDamage + attack1Damage;

			hitEffectiveText.color = Color.green;
			hitEffectiveText.text = "Attack 1 was effective!";
			Debug.Log("Hit was effective");
		}
		//* If the attack was not effective then deal a small amount of damage
		else
		{
			float randomisedDamage = Random.Range(-1f, 1f);
			enemy.curHealth -= randomisedDamage + (attack1Damage / 3);

			hitEffectiveText.color = Color.red;
			hitEffectiveText.text = "Attack 1 was not effective!";
			Debug.Log("Hit was not effective");
		}

		//* Sets the enemy healthbar to its current health
		enemySlider.value = enemy.curHealth;

		//* If the attack the player just did killed the enemy
		if (enemy.curHealth <= 0)
		{
			enemy.enemyGameObject.GetComponent<Rigidbody>().isKinematic = false;
			//* The rigidbody would not wake up, so I had to wake it up manually
			enemy.enemyGameObject.GetComponent<Rigidbody>().WakeUp();
			enemy.enemyGameObject.GetComponent<Rigidbody>().sleepThreshold = 0f;
			enemy.enemyGameObject.transform.rotation = Quaternion.Euler(1, 45, 0);

			//* Stops the timer and restarts it with the "won" bool set to true
			StopCoroutine("Timer");
			won = true;
			startTimer = true;
			StartCoroutine("Timer");
		}
	}

	/// <summary>
	/// Attacks the enemy with "Attack 2"
	/// </summary>
	public void Attack2()
	{
		//* Creates the attack prefab within the scene and parents it to the transform which is 
		//* already in the scene that has its position/rotation set to face the enemy
		Instantiate(attact2, attackSpawnPos);

		//* Assigns the enemy variable
		Enemy enemy = GetComponent<Enemy>();
		//* Sets the attack that the player just chose in the Enemy script
		enemy.playersChosenAttack = 2;
		//* Makes the enemy choose a random block
		enemy.ChooseBlock();

		//* Starts the coroutine timer
		startTimer = true;
		StartCoroutine("Timer");

		//* If the attack was effective then deal set damage
		if (enemy.wasEffective)
		{
			float randomisedDamage = Random.Range(-5f, 5f);
			enemy.curHealth -= randomisedDamage + attack2Damage;

			hitEffectiveText.color = Color.green;
			hitEffectiveText.text = "Attack 2 was effective!";
			Debug.Log("Hit was effective");
		}
		//* If the attack was not effective then deal a small amount of damage
		else
		{
			enemy.curHealth += 5f;
			if (enemy.curHealth > enemy.maxHealth)
			{
				enemy.curHealth = enemy.maxHealth;
			}

			hitEffectiveText.color = Color.red;
			hitEffectiveText.text = "Attack 2 was not effective!";
			Debug.Log("Hit was not effective");
		}

		//* Sets the enemy healthbar to its current health
		enemySlider.value = enemy.curHealth;

		//* If the attack the player just did killed the enemy
		if (enemy.curHealth <= 0)
		{
			enemy.enemyGameObject.GetComponent<Rigidbody>().isKinematic = false;
			//* The rigidbody would not wake up, so I had to wake it up manually
			enemy.enemyGameObject.GetComponent<Rigidbody>().WakeUp();
			enemy.enemyGameObject.GetComponent<Rigidbody>().sleepThreshold = 0f;
			enemy.enemyGameObject.transform.rotation = Quaternion.Euler(1, 45, 0);

			//* Stops the timer and restarts it with the "won" bool set to true
			StopCoroutine("Timer");
			won = true;
			startTimer = true;
			StartCoroutine("Timer");
		}
	}

	/// <summary>
	/// Blocks the enemy attack with "Block 1"
	/// </summary>
	public void Block1()
	{
		Instantiate(block1, blockSpawnPos);

		GameObject block = GameObject.FindGameObjectWithTag("Block");
		block.transform.localPosition = Vector3.zero;
		block.transform.localRotation = new Quaternion();

		Enemy enemy = GetComponent<Enemy>();
		enemy.playersChosenBlock = 1;
		enemy.ChooseAttack();

		if (wasEffective)
		{
			hitEffectiveText.color = Color.green;
			hitEffectiveText.text = "Block 1 was effective!";
		}
		else
		{
			hitEffectiveText.color = Color.red;
			hitEffectiveText.text = "Block 1 was not effective!";

			Camera.main.GetComponent<Animator>().SetBool("Animate", true);
		}
	}

	/// <summary>
	/// Blocks the enemy attack with "Block 2"
	/// </summary>
	public void Block2()
	{
		Instantiate(block2, blockSpawnPos);

		GameObject block = GameObject.FindGameObjectWithTag("Block");
		block.transform.localPosition = Vector3.zero;
		block.transform.localRotation = new Quaternion();

		Enemy enemy = GetComponent<Enemy>();
		enemy.playersChosenBlock = 2;
		enemy.ChooseAttack();

		if (wasEffective)
		{
			hitEffectiveText.color = Color.green;
			hitEffectiveText.text = "Block 2 was effective!";
		}
		else
		{
			hitEffectiveText.color = Color.red;
			hitEffectiveText.text = "Block 2 was not effective!";

			Camera.main.GetComponent<Animator>().SetBool("Animate", true);
		}
	}

	/// <summary>
	/// Runs a timer, when the timer is finished it is the enemy's turn
	/// </summary>
	/// <returns></returns>
	IEnumerator Timer()
	{
		while (true)
		{
			if (startTimer)
			{
				startTimer = false;
				if (!won)
				{
					yield return new WaitForSeconds(timerLength);
				}
				else
				{
					yield return new WaitForSeconds(5);
				}
			}
			else
			{
				if (!won)
				{
					GetComponent<UIGame>().Defending();
					Destroy(GameObject.FindGameObjectWithTag("Attack"));
					Destroy(GameObject.FindGameObjectWithTag("Block"));
					StopCoroutine("Timer");
					yield return null;
				}
				else
				{
					GetComponent<UIGame>().Win();
					yield return null;
				}
			}
		}
	}
}
