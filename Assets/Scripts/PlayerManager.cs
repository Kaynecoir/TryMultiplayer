using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : NetworkBehaviour
{
    public struct PlayerPosition
	{
        public float x;
        public float y;
        public float z;
        public float degZ;
	}
    public struct PlayerHealth
	{
        public float maxHealth;
        public float curHealth;
	}

    public Health health;
    public Transform player;
    public Transform playerObj;
    public Canvas canvas;
    public Color playerColor;
    public string playerName;
    public float playerSpeed;
    public int coinCount;


	private void Start()
	{
        health.Death += MeDestroy;
        //health.ChangeHealth += () =>
        //{
        //    networkHealthVariable.Value = health.CurrentHealth;
        //};
	}

	private void FixedUpdate()
	{
        if (!IsOwner) return;
        if(Input.GetKey(KeyCode.Space))
		{
            TestServerRpc();
            TestClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
		}
        //PositionClientRpc();
	}

    [ServerRpc]
    private void TestServerRpc()
	{
        Debug.Log(OwnerClientId + " TestServerRpc");
        //transform.position = new Vector3(networkPositionVariable.Value.x, networkPositionVariable.Value.y, networkPositionVariable.Value.z);
        //playerObj.transform.rotation = Quaternion.Euler(0, 0, networkPositionVariable.Value.degZ);
    }
    [ClientRpc]
    private void TestClientRpc(ClientRpcParams clientRpcParams)
    {
        Debug.Log(OwnerClientId + " TestClientRpc");
    }

 //   [ServerRpc]
 //   public void PositionServerRpc(Vector3 pos, float degree)
	//{
 //       //transform.position = pos;
 //       //playerObj.transform.rotation = Quaternion.Euler(0, 0, degree);
 //       PositionClientRpc(pos, degree);
 //   }
    [ClientRpc]
    public void PositionClientRpc(Vector3 pos, float degree)
    {
        transform.position = pos;
        playerObj.transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    public void MeDestroy()
	{
        Destroy(gameObject);
	}
}
