using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public GameObject playerGameObject;

	public float maxHealth = 100f;
	public float curHealth = 100f;

	public Transform attackSpawnPos;
	public Transform blockSpawnPos;
	public GameObject attact1;
	public GameObject attact2;
	public GameObject block1;
	public GameObject block2;

	public int enemysChosenAttack = 0; // 1 is attack1, 2 is attack2
	public int enemysChosenBlock = 0; // 1 is block1, 2 is block2

	public bool wasEffective = false;

	public Slider playerSlider;
	public Slider enemySlider;

	public float attack1Damage = 20f;
	public float attack2Damage = 30f;

	[SerializeField]
	private float timerLength = 3f;
	[SerializeField]
	private Text hitEffectiveText;

	private bool startTimer = true;
	private bool won = false;
	private bool lost = false;

	public void Attack1()
	{
		Instantiate(attact1, attackSpawnPos);

		GameObject attack = GameObject.FindGameObjectWithTag("Attack");
		attack.transform.localPosition = Vector3.zero;
		attack.transform.localRotation = new Quaternion();

		Enemy enemy = GetComponent<Enemy>();
		enemy.playersChosenAttack = 1;
		enemy.ChooseBlock();

		startTimer = true;
		StartCoroutine("Timer");

		if (enemy.wasEffective)
		{
			float randomisedDamage = Random.Range(-2.5f, 2.5f);
			enemy.curHealth -= randomisedDamage + attack1Damage;

			hitEffectiveText.color = Color.green;
			hitEffectiveText.text = "Attack 1 was effective!";
			Debug.Log("Hit was effective");
		}
		else
		{
			float randomisedDamage = Random.Range(-1f, 1f);
			enemy.curHealth -= randomisedDamage + (attack1Damage / 3);

			hitEffectiveText.color = Color.red;
			hitEffectiveText.text = "Attack 1 was not effective!";
			Debug.Log("Hit was not effective");
		}

		enemySlider.value = enemy.curHealth;

		if (enemy.curHealth <= 0)
		{
			enemy.enemyGameObject.GetComponent<Rigidbody>().isKinematic = false;
			enemy.enemyGameObject.GetComponent<Rigidbody>().WakeUp();
			enemy.enemyGameObject.GetComponent<Rigidbody>().sleepThreshold = 0f;
			enemy.enemyGameObject.transform.rotation = Quaternion.Euler(1, 45, 0);

			StopCoroutine("Timer");
			won = true;
			startTimer = true;
			StartCoroutine("Timer");
		}
	}

	public void Attack2()
	{
		Instantiate(attact2, attackSpawnPos);
		GameObject attack = GameObject.FindGameObjectWithTag("Attack");
		attack.transform.localPosition = Vector3.zero;
		attack.transform.localRotation = new Quaternion();

		Enemy enemy = GetComponent<Enemy>();
		enemy.playersChosenAttack = 2;
		enemy.ChooseBlock();

		if (!won && !lost)
		{
			startTimer = true;
			StartCoroutine("Timer");
		}		

		if (enemy.wasEffective)
		{
			float randomisedDamage = Random.Range(-5f, 5f);
			enemy.curHealth -= randomisedDamage + attack2Damage;

			hitEffectiveText.color = Color.green;
			hitEffectiveText.text = "Attack 2 was effective!";
			Debug.Log("Hit was effective");
		}
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

		enemySlider.value = enemy.curHealth;

		if (enemy.curHealth <= 0)
		{
			enemy.enemyGameObject.GetComponent<Rigidbody>().isKinematic = false;
			enemy.enemyGameObject.GetComponent<Rigidbody>().WakeUp();
			enemy.enemyGameObject.GetComponent<Rigidbody>().sleepThreshold = 0f;
			enemy.enemyGameObject.transform.rotation = Quaternion.Euler(1, 45, 0);

			StopCoroutine("Timer");
			won = true;
			startTimer = true;
			StartCoroutine("Timer");
		}
	}

	public void Block1()
	{
		Instantiate(block1, blockSpawnPos);

		GameObject block = GameObject.FindGameObjectWithTag("Block");
		block.transform.localPosition = Vector3.zero;
		block.transform.localRotation = new Quaternion();

		Enemy enemy = GetComponent<Enemy>();
		enemy.playersChosenBlock = 1;
		enemy.ChooseAttack();

		if (enemysChosenAttack == 1)
		{
			hitEffectiveText.color = Color.red;
			hitEffectiveText.text = "Block 1 was not effective!";

			wasEffective = true; // Enemy's attack was effective
			Camera.main.GetComponent<Animator>().SetBool("Animate", true);
		}
		else
		{
			hitEffectiveText.color = Color.green;
			hitEffectiveText.text = "Block 1 was effective!";

			wasEffective = false;
		}
	}

	public void Block2()
	{
		Instantiate(block2, blockSpawnPos);

		GameObject block = GameObject.FindGameObjectWithTag("Block");
		block.transform.localPosition = Vector3.zero;
		block.transform.localRotation = new Quaternion();

		Enemy enemy = GetComponent<Enemy>();
		enemy.playersChosenBlock = 2;
		enemy.ChooseAttack();

		if (enemysChosenAttack == 2)
		{
			hitEffectiveText.color = Color.red;
			hitEffectiveText.text = "Block 2 was not effective!";

			wasEffective = true;
			Camera.main.GetComponent<Animator>().SetBool("Animate", true);
		}
		else
		{
			hitEffectiveText.color = Color.red;
			hitEffectiveText.text = "Block 2 was effective!";

			wasEffective = false;
		}
	}

	IEnumerator Timer()
	{
		while (true)
		{
			if (startTimer)
			{
				startTimer = false;
				if (!won && !lost)
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
				if (!won && !lost)
				{
					GetComponent<UIGame>().Defending();
					Destroy(GameObject.FindGameObjectWithTag("Attack"));
					Destroy(GameObject.FindGameObjectWithTag("Block"));
					StopCoroutine("Timer");
					yield return null;
				}

				if (won)
				{
					GetComponent<UIGame>().Win();
					yield return null;
				}

				if (lost)
				{
					GetComponent<UIGame>().Lose();
					yield return null;
				}
			}
		}
	}
}
