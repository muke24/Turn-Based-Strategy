#region This code has been written by Peter Thompson
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
	#region Variables
	[Header("UI GameObjects")]
	public GameObject playerPanel;
	public GameObject enemyPanel;
	public GameObject playerCanv;
	public GameObject cpuCanv;
	public GameObject statsCanv;
	public GameObject win;
	public GameObject lose;

	[SerializeField]
	private Text playerDoText = null;
	#endregion

	/// <summary>
	/// Displays the attack buttons
	/// </summary>
	public void Attacking()
	{
		enemyPanel.SetActive(false);
		playerPanel.SetActive(true);
		playerDoText.text = "Choose an attack";
	}

	/// <summary>
	/// Changes the "PlayerDoText" while attacking the enemy
	/// </summary>
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

	/// <summary>
	/// Displays the block buttons
	/// </summary>
	public void Defending()
	{
		playerPanel.SetActive(false);
		enemyPanel.SetActive(true);
		playerDoText.text = "Choose a block";
	}

	/// <summary>
	/// Changes the "PlayerDoText" while blocking the enemy's attack
	/// </summary>
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

	/// <summary>
	/// Wins the game
	/// </summary>
	public void Win()
	{
		playerCanv.SetActive(false);
		cpuCanv.SetActive(false);
		statsCanv.SetActive(false);
		win.SetActive(true);
	}

	/// <summary>
	/// Loses the game
	/// </summary>
	public void Lose()
	{
		playerCanv.SetActive(false);
		cpuCanv.SetActive(false);
		statsCanv.SetActive(false);
		lose.SetActive(true);
	}
}
//* This code has been written by Peter Thompson
#endregion