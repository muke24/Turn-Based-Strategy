using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public GameObject enemyGameObject;

	public float maxHealth = 100f;
	public float curHealth = 100f;

	public Transform attackSpawnPos;
	public Transform blockSpawnPos;
	public GameObject attact1;
	public GameObject attact2;
	public GameObject block1;
	public GameObject block2;

	public int playersChosenAttack = 0; // 1 is attack1, 2 is attack2
	public int playersChosenBlock = 0; // 1 is block1, 2 is block2

	public bool wasEffective = false;

	public Slider playerSlider;

	public float attack1Damage = 20f;
	public float attack2Damage = 30f;

	[SerializeField]
	private float timerLength = 3f;

	private bool lost = false;
	private bool startTimer = true;

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

		if (player.wasEffective)
		{
			float randomisedDamage = Random.Range(-2.5f, 2.5f);
			player.curHealth -= randomisedDamage + attack1Damage;
		}
		else
		{
			float randomisedDamage = Random.Range(-1f, 1f);
			player.curHealth -= randomisedDamage + (attack1Damage / 3);
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

		if (player.wasEffective)
		{
			float randomisedDamage = Random.Range(-5f, 5f);
			player.curHealth -= randomisedDamage + attack2Damage;
		}
		else
		{
			player.curHealth += 5f;
			if (player.curHealth > player.maxHealth)
			{
				player.curHealth = player.maxHealth;
			}
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
