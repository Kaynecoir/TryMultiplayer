using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : NetworkBehaviour
{

	public NetworkVariable<positionStruct> networkVariable = new NetworkVariable<positionStruct>(new positionStruct { x = 0 },
		NetworkVariableReadPermission.Everyone,
		NetworkVariableWritePermission.Owner);

	public struct positionStruct
	{
		public float x;
	}

	public Canvas canvas;
    public Transform player;
    public Transform playerObj;

    public float playerSpeed;
    public int coinCount;
    public override void OnNetworkSpawn()
    {
        networkVariable.OnValueChanged += (positionStruct preVal, positionStruct newVal) =>
        {
            Debug.Log(OwnerClientId + " = { x: " + newVal.x + " }");
        };
        //networkVariable.OnValueChanged += (positionStruct prevVal, positionStruct newVal) =>
        //{
        //    Debug.Log(OwnerClientId + ": value {x: " + networkVariable.Value.x + ", y: " + networkVariable.Value.y + "}");

        //};

    }
}
