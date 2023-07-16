using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Fire : MonoBehaviour
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
		//if ()
		curFireSpeed -= Time.deltaTime;
		if (Input.GetKey(KeyCode.Space))
		{
			if (curFireSpeed <= 0)
			{
				Debug.Log("Fire");
				Bullet bullet = Instantiate(fireObj, posGenerate.position, Quaternion.identity).GetComponent<Bullet>();
				//bullet = FireBulletServerRpc(bullet);

				Vector3 dir = (Input.mousePosition - (Vector3)canvas.renderingDisplaySize / 2) / 108 - playerController.playerObj.transform.position;
				bullet.speed = dir.normalized * bulletSpeed;

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
	private Bullet FireBulletServerRpc(Bullet bullet)
	{

		bullet.GetComponent<NetworkObject>().Spawn();

		return bullet;
	}
}
