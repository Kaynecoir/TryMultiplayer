using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private float maxHealth, curHealth;
	public delegate void SimpleFunc();
	public event SimpleFunc Death;

	public float CurrentHealth { get => curHealth; }
	public float MaximumHealth { get => maxHealth; }
	public float AmountHealth { get => curHealth / maxHealth; }

	public void TakeHealth(float val)
	{
		curHealth += val;
		if (curHealth > maxHealth) curHealth = maxHealth;
		if (curHealth <= 0) curHealth = 0;
		if (curHealth == 0) Death?.Invoke();
	}
}
