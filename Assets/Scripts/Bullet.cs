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
		//if (!NetworkManager.Singleton.IsServer) return;
		if (!IsOwner) return;
		//if (NetworkManager.Singleton.IsServer) Spawn(this.gameObject);
		//else SpawnServerRpc();

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
		if (!IsOwner) return;
		DespawnServerRpc();
		Destroy(this.gameObject);
	}
	
	private void Despawn(GameObject go)
	{
		go.GetComponent<NetworkObject>().Despawn();
	}
	[ServerRpc]
	private void DespawnServerRpc()
	{
		Despawn(this.gameObject);
	}
}
