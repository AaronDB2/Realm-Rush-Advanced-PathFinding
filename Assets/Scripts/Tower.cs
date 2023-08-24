using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Cost of the tower")]
    int cost = 75;

    // Function for instantiating the tower game object
    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        // Check if currentBalance of the bank is higher than the tower cost
        if(bank.CurrentBalance >= cost)
        {
			Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(cost);
			return true;
		}

        return false;
    }
}
