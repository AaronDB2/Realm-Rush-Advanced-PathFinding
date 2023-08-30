using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Cost of the tower")]
    int cost = 75;

	[SerializeField]
	[Tooltip("Build timer")]
	float buildDelay = 1f;

	void Start()
	{
        StartCoroutine(Build());
	}

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

    // Function that implements a build time for the tower
    IEnumerator Build()
    {
        foreach(Transform child in transform) 
        { 
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
			foreach (Transform grandchild in child)
			{
				grandchild.gameObject.SetActive(true);
			}
		}
	}
}
