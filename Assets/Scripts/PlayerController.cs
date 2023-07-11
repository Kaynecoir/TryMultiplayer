using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    private GameObject player;
	public PlayerManager PlayerManager => playerManager;
    public Canvas canvas;

	void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        player = playerManager.playerObj;
    }

    void FixedUpdate()
    {
        
        if (!playerManager.IsOwner) return;

        //moving
        Vector3 dirMove = new Vector2();
        if (Input.GetKey(KeyCode.W)) dirMove += Vector3.up;
        else if (Input.GetKey(KeyCode.S)) dirMove += Vector3.down;
        if (Input.GetKey(KeyCode.D)) dirMove += Vector3.right;
        else if (Input.GetKey(KeyCode.A)) dirMove += Vector3.left;

        transform.position += dirMove * playerManager.playerSpeed;


        //rotation
        Vector3 dirView = (Input.mousePosition - (Vector3)canvas.renderingDisplaySize / 2) / 108 - player.transform.position;

        float angle_rad = Mathf.Atan2(dirView.y, dirView.x);
        float angle_del = angle_rad / Mathf.PI * 180;
        player.transform.rotation = Quaternion.Euler(0, 0, angle_del);


        //another
        if (Input.GetKey(KeyCode.Q)) playerManager.networkVariable.Value = Random.Range(0, 20);

    }
}
