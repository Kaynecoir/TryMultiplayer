using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Fire : NetworkBehaviour
{
	public GameObject fireObj;
	public Transform posGenerate;
	//[SerializeField] private Joystick joysticToView;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private Canvas canvas;

	public float fireSpeed;
	float curFireSpeed = 0;
	public float bulletSpeed;

	private void Start()
	{
		playerController = GetComponent<PlayerController>();
		//joysticToView = playerController.joysticToView;
	}

	private void Update()
	{
		if (!playerController.PlayerManager.IsOwner) return;
		curFireSpeed -= Time.deltaTime;
		if (Input.GetKey(KeyCode.Space))
		{
			if (curFireSpeed <= 0)
			{
				Debug.Log("Fire");
				FireBulletServerRpc(playerController.PlayerManager.networkVariable.Value.deg);

				curFireSpeed = fireSpeed;
			}
		}
		//if (joysticToView != null && joysticToView.Direction != Vector2.zero)
		//{
		//	curFireSpeed -= Time.deltaTime;
		//	if (curFireSpeed <= 0)
		//	{
		//		Bullet bullet = Instantiate(fireObj, posGenerate.position, Quaternion.identity).GetComponent<Bullet>();

		//		bullet.speed = joysticToView.Direction.normalized * bulletSpeed;

		//		curFireSpeed = fireSpeed;
		//	}
		//}
		//else curFireSpeed = 0;
	}

	[ServerRpc]
	private void FireBulletServerRpc(float deg)
	{
		GameObject go = Instantiate(fireObj, posGenerate.position, Quaternion.identity);
		Bullet bullet = go.GetComponent<Bullet>();

		go.GetComponent<NetworkObject>().Spawn();

		Vector3 dir = new Vector3(Mathf.Cos(deg / 180 * Mathf.PI), Mathf.Sin(deg / 180 * Mathf.PI));
		bullet.speed = dir.normalized * bulletSpeed;
	}
}
