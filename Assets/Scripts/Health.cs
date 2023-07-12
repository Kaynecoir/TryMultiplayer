using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health :MonoBehaviour
{
	[SerializeField] private float maxHealth, curHealth;
	public delegate void VoidFunc();
	public event VoidFunc Death;
	public event VoidFunc ChangeHealth;
	public delegate float FloatFunc(float value);

	public float CurrentHealth { get => curHealth; }
	public float MaximumHealth { get => maxHealth; }
	public float AmountHealth { get => curHealth / maxHealth; }

	public Health()
	{
		
	}
	public float TakeHealth(float val)
	{
		curHealth += val;
		if (curHealth > maxHealth) curHealth = maxHealth;
		if (curHealth <= 0) curHealth = 0;
		ChangeHealth?.Invoke();
		if (curHealth == 0) Death?.Invoke();
		return curHealth;
	}
}
