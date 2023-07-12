using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    private Transform playerObj;
	public PlayerManager PlayerManager => playerManager;
    public Canvas canvas;
    public Vector3 vectorOfView;
    public float degreesOfView;

	void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerObj = playerManager.playerObj;
        canvas = playerManager.canvas;
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

        dirMove *= playerManager.playerSpeed;
        dirMove += playerManager.player.position;
        //Debug.Log(dirMove);


        //rotation
        vectorOfView = (Input.mousePosition - (Vector3)canvas.renderingDisplaySize / 2) / 108 - transform.position;
        vectorOfView.Normalize();
        float angle_rad = Mathf.Atan2(vectorOfView.y, vectorOfView.x);
        degreesOfView = angle_rad / Mathf.PI * 180;

		transform.position = dirMove;
		playerObj.transform.rotation = Quaternion.Euler(0, 0, degreesOfView);
		//playerManager.PositionServerRpc(dirMove, degreesOfView);
		//another
		//playerManager.networkPositionVariable.Value = new PlayerManager.PlayerPosition
		//{
		//    x = dirMove.x,
		//    y = dirMove.y,
		//    z = dirMove.z,
		//    degZ = degreesOfView,
		//};
	}
}
