using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Reward for destroying this enemy")]
	int goldReward = 25;

	[SerializeField]
	[Tooltip("Penalty for when the enemy makes it to the end")]
	int goldPenalty = 25;

	Bank bank;

	// Runs at the start of object initialisation
	void Start()
	{
		bank = FindObjectOfType<Bank>();
	}

	// Function for handeling reward gold logic
	public void RewardGold()
	{
		if(bank == null) return;
		bank.Deposit(goldReward);
	}

	// Function for handeling steal gold logic
	public void StealGold()
	{
		if (bank == null) return;
		bank.Withdraw(goldPenalty);
	}
}
