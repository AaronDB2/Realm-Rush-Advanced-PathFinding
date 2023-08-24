using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Starting currency balance")]
    int startingBalance = 150;

	[SerializeField]
	[Tooltip("Balance GUI element")]
	TextMeshProUGUI displayBalance;

	[SerializeField]
    int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

	// Runs at the beginning of object instantiation
	void Awake()
	{
		currentBalance = startingBalance;
		UpdateDisplay();
	}

	// Function for increasing the currentBalance
	public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
		UpdateDisplay();
	}

	// Function for withdrawing from the currentBalance
	public void Withdraw(int amount)
	{
		currentBalance -= Mathf.Abs(amount);
		UpdateDisplay();

		// Check if balance is less than 0
		if (currentBalance < 0 )
		{
			//Lose the game
			ReloadScene();
		}
	}

	// Function for Reloading the active scene
	void ReloadScene()
	{
		Scene currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(currentScene.buildIndex);
	}

	// Function that will update GUI balance
	void UpdateDisplay()
	{
		displayBalance.text = "Gold: " + currentBalance;
	}
}

