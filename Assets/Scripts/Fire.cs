using System.Collections;
using System.Collections.Generic;
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

	private void FixedUpdate()
	{
		if (!playerController.PlayerManager.IsOwner) return;

		if (Input.GetKey(KeyCode.Space))
		{
			curFireSpeed -= Time.deltaTime;
			if (curFireSpeed <= 0)
			{
				Bullet bullet = Instantiate(fireObj, posGenerate.position, Quaternion.identity).GetComponent<Bullet>();

				Vector3 dir = new Vector3(playerController.transform.position.x - canvas.renderingDisplaySize.x / canvas.scaleFactor, playerController.transform.position.y - canvas.renderingDisplaySize.y / canvas.scaleFactor, 0);
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
}
