using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    public Transform playerObj;
	public PlayerManager PlayerManager => playerManager;
    public Canvas canvas;


    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        canvas = playerManager.canvas;
        playerObj = playerManager.playerObj;
    }

    void Update()
    {
        
        if (!playerManager.IsOwner) return;

        //moving
        Vector3 dirMove = new Vector2();
        if (Input.GetKey(KeyCode.W)) dirMove += Vector3.up;
        else if (Input.GetKey(KeyCode.S)) dirMove += Vector3.down;
        if (Input.GetKey(KeyCode.D)) dirMove += Vector3.right;
        else if (Input.GetKey(KeyCode.A)) dirMove += Vector3.left;

        dirMove *= playerManager.playerSpeed * Time.deltaTime;
        dirMove += transform.position;  


        //rotation
        Vector3 dirView = (Input.mousePosition - (Vector3)canvas.renderingDisplaySize / 2) / 108 - playerObj.transform.position;

        float angle_rad = Mathf.Atan2(dirView.y, dirView.x);
        float angle_del = angle_rad / Mathf.PI * 180;
        //player.transform.rotation = Quaternion.Euler(0, 0, angle_del);


        //another
        playerManager.networkVariable.Value = new PlayerManager.positionStruct { x = dirMove.x, y = dirMove.y, deg = angle_del };

        // if (Input.GetKey(KeyCode.Q)) playerManager.networkVariable.Value = new PlayerManager.positionStruct { x = player.position.x, y = player.position.y };
        // playerManager.networkVariable.Value = new PlayerManager.positionStruct { x = player.position.x, y = player.position.y };

    }
}
