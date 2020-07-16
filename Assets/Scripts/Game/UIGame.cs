using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
	public GameObject playerPanel;
	public GameObject enemyPanel;
	public GameObject playerCanv;
	public GameObject cpuCanv;
	public GameObject statsCanv;
	public GameObject win;
	public GameObject lose;

	[SerializeField]
	private Text playerDoText = null;

	public void Attacking()
	{
		enemyPanel.SetActive(false);
		playerPanel.SetActive(true);
		playerDoText.text = "Choose an attack";
	}

	public void IsAttacking()
	{
		if (GetComponent<Enemy>().playersChosenAttack == 1)
		{
			playerDoText.text = "Attacking Player 2 with Attack 1"; 
		}
		else
		{
			playerDoText.text = "Attacking Player 2 with Attack 2";
		}
	}

	public void Defending()
	{
		playerPanel.SetActive(false);
		enemyPanel.SetActive(true);
		playerDoText.text = "Choose a block";
	}

	public void IsDefending()
	{
		if (GetComponent<Enemy>().playersChosenBlock == 1)
		{
			playerDoText.text = "Blocking Player 2's attack with Block 1";
		}
		else
		{
			playerDoText.text = "Blocking Player 2's attack with Block 2";
		}
	}

	public void Win()
	{
		playerCanv.SetActive(false);
		cpuCanv.SetActive(false);
		statsCanv.SetActive(false);
		win.SetActive(true);
	}

	public void Lose()
	{
		playerCanv.SetActive(false);
		cpuCanv.SetActive(false);
		statsCanv.SetActive(false);
		lose.SetActive(true);
	}
}
