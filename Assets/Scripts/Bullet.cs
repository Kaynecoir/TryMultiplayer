using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Vector3 speed;
	public float lifeTime;
	public CollisionDamage collisionDamage;
	private void Start()
	{
		collisionDamage.DamageObject += MeDestroy;
	}
	private void FixedUpdate()
	{
		transform.position += speed;
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0) MeDestroy(gameObject);
	}

	private void MeDestroy(GameObject go)
	{
		Destroy(this.gameObject);
	}
}
