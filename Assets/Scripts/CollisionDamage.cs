using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CollisionDamage : MonoBehaviour
{
	public float damage;
	public string collisionTag;
	public delegate void SimpleFunc(GameObject go);
	public event SimpleFunc DamageObject;

	private void Start()
	{
		DamageObject += Damage;
		//DamageObject += (GameObject go) => { Debug.Log(go.GetComponent<Health>().CurrentHealth); };
	}

	public void Damage(GameObject go)
	{
			Health health = go.GetComponent<Health>();
			health.TakeHealth(-damage);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == collisionTag)
		{
			DamageObject?.Invoke(collision.gameObject);
		}
	}
}
