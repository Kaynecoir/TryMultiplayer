using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
	public Vector3 speed;
	public float lifeTime;
	public CollisionDamage collisionDamage;
	private void Start()
	{
		collisionDamage = GetComponent<CollisionDamage>();
		collisionDamage.DamageObject += MeDestroy;
	}
	private void Update()
	{
		transform.position += speed * Time.deltaTime;
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0) MeDestroy(gameObject);
	}

	private void MeDestroy(GameObject go)
	{
		DespawnServerRpc();
		Destroy(this.gameObject);
	}

	[ServerRpc]
	private void DespawnServerRpc()
	{
		if (!IsOwner) return;
		GetComponent<NetworkObject>().Despawn();
	}
}
