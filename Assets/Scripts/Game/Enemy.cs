#region This code has been written by Peter Thompson
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//* Check the Player script for the code comments. 
//* This script is very similar to the player script so it should include most of the same comments.
public class Enemy : MonoBehaviour
{
	#region Variables
	[Header("Enemy Health")]
	public float maxHealth = 100f;
	public float curHealth = 100f;

	[Header("Enemy GameObject")]
	[Space(10)]
	public GameObject enemyGameObject;

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
	//* 1 is attack1, 2 is attack2, 0 is null
	public int playersChosenAttack = 0;
	//* 1 is block1, 2 is block2, 0 is null
	public int playersChosenBlock = 0;

	[Header("If The Attack The Player Just Used Was Effective")]
	[Space(10)]
	public bool wasEffective = false;

	[Header("Player's Health Slider")]
	[Space(10)]
	public Slider playerSlider;

	[Header("Damage Dealt From Each Attack")]
	[Space(10)]
	public float attack1Damage = 20f;
	public float attack2Damage = 30f;

	[Header("Time Between Each Play")]
	[Space(10)]
	[SerializeField]
	private float timerLength = 3f;

	private bool lost = false;
	private bool startTimer = true;
	#endregion

	/// <summary>
	/// Chooses a random attack then attacks with selected attack
	/// </summary>
	public void ChooseAttack()
	{
		int value = Random.Range(1, 3);
		if (value == 1)
		{
			Attack1();
		}
		else
		{
			Attack2();
		}
	}

	/// <summary>
	/// Attacks the player with "Attack 1"
	/// </summary>
	private void Attack1()
	{
		Instantiate(attact1, attackSpawnPos);
		GameObject attack = GameObject.FindGameObjectWithTag("Attack");
		attack.transform.localPosition = Vector3.zero;
		attack.transform.localRotation = new Quaternion();

		Player player = GetComponent<Player>();
		player.enemysChosenAttack = 1;

		if (!lost)
		{
			startTimer = true;
			StartCoroutine("Timer");
		}

		if (playersChosenBlock == 1)
		{
			float randomisedDamage = Random.Range(-2.5f, 2.5f);
			player.curHealth -= randomisedDamage + attack1Damage;

			player.wasEffective = true;
		}
		else
		{
			float randomisedDamage = Random.Range(-1f, 1f);
			player.curHealth -= randomisedDamage + (attack1Damage / 3);

			player.wasEffective = false;
		}

		playerSlider.value = player.curHealth;

		if (player.curHealth <= 0)
		{
			player.playerGameObject.GetComponent<Rigidbody>().isKinematic = false;
			player.playerGameObject.GetComponent<Rigidbody>().WakeUp();
			player.playerGameObject.GetComponent<Rigidbody>().sleepThreshold = 0f;
			player.playerGameObject.transform.rotation = Quaternion.Euler(-1, 45, 0);

			StopCoroutine("Timer");
			lost = true;
			startTimer = true;
			StartCoroutine("Timer");
		}
	}

	/// <summary>
	/// Attacks the player with "Attack 2"
	/// </summary>
	private void Attack2()
	{
		Instantiate(attact2, attackSpawnPos);
		GameObject attack = GameObject.FindGameObjectWithTag("Attack");
		attack.transform.localPosition = Vector3.zero;
		attack.transform.localRotation = new Quaternion();

		Player player = GetComponent<Player>();
		player.enemysChosenAttack = 2;

		if (!lost)
		{
			startTimer = true;
			StartCoroutine("Timer");
		}

		if (playersChosenBlock == 2)
		{
			float randomisedDamage = Random.Range(-5f, 5f);
			player.curHealth -= randomisedDamage + attack2Damage;

			player.wasEffective = true;
		}
		else
		{
			player.curHealth += 5f;
			if (player.curHealth > player.maxHealth)
			{
				player.curHealth = player.maxHealth;
			}

			player.wasEffective = false;
		}

		playerSlider.value = player.curHealth;

		if (player.curHealth <= 0)
		{
			player.playerGameObject.GetComponent<Rigidbody>().isKinematic = false;
			player.playerGameObject.GetComponent<Rigidbody>().WakeUp();
			player.playerGameObject.GetComponent<Rigidbody>().sleepThreshold = 0f;
			player.playerGameObject.transform.rotation = Quaternion.Euler(-1, 45, 0);

			StopCoroutine("Timer");
			lost = true;
			startTimer = true;
			StartCoroutine("Timer");
		}
	}

	/// <summary>
	/// Chooses a random block then blocks with selected block
	/// </summary>
	public void ChooseBlock()
	{
		int value = Random.Range(1, 3);
		if (value == 1)
		{
			Block1();
		}
		else
		{
			Block2();
		}
	}

	/// <summary>
	/// Blocks the player's attack with "Block 1"
	/// </summary>
	private void Block1()
	{
		Instantiate(block1, blockSpawnPos);

		GameObject block = GameObject.FindGameObjectWithTag("Block");
		block.transform.localPosition = Vector3.zero;
		block.transform.localRotation = new Quaternion();

		if (playersChosenAttack == 1)
		{
			wasEffective = false;
		}
		else
		{
			wasEffective = true;
		}
	}

	/// <summary>
	/// Blocks the player's attack with "Block 2"
	/// </summary>
	private void Block2()
	{
		Instantiate(block2, blockSpawnPos);

		GameObject block = GameObject.FindGameObjectWithTag("Block");
		block.transform.localPosition = Vector3.zero;
		block.transform.localRotation = new Quaternion();

		if (playersChosenAttack == 2)
		{
			wasEffective = false;
		}
		else
		{
			wasEffective = true;
		}
	}

	/// <summary>
	/// Runs a timer, when the timer is finished it is the player's turn (or if the player is dead, player will lose the game)
	/// </summary>
	/// <returns></returns>
	IEnumerator Timer()
	{
		while (true)
		{
			if (startTimer)
			{
				startTimer = false;
				if (!lost)
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
				if (!lost)
				{
					GetComponent<UIGame>().Attacking();
					Destroy(GameObject.FindGameObjectWithTag("Attack"));
					Destroy(GameObject.FindGameObjectWithTag("Block"));
					StopCoroutine("Timer");
					yield return null;
				}
				else
				{
					GetComponent<UIGame>().Lose();
					yield return null;
				}
			}
		}
	}
}
//* This code has been written by Peter Thompson
#endregion