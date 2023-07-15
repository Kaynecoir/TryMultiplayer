using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : NetworkBehaviour
{

	public NetworkVariable<positionStruct> networkVariable = new NetworkVariable<positionStruct>(new positionStruct { x = 0, y = 0, deg = 0 },
		NetworkVariableReadPermission.Everyone,
		NetworkVariableWritePermission.Owner);

	public struct positionStruct : INetworkSerializable
	{
		public float x;
        public float y;
        public float deg;

		public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
		{
            serializer.SerializeValue(ref x);
            serializer.SerializeValue(ref y);
            serializer.SerializeValue(ref deg);
        }
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
            Debug.Log(OwnerClientId + " = { x: " + newVal.x + ", y: " + newVal.y + " }");
        };
        //networkVariable.OnValueChanged += (positionStruct prevVal, positionStruct newVal) =>
        //{
        //    Debug.Log(OwnerClientId + ": value {x: " + networkVariable.Value.x + ", y: " + networkVariable.Value.y + "}");

        //};

    }

	private void Update()
	{
        player.transform.position = new Vector3(networkVariable.Value.x, networkVariable.Value.y);
        playerObj.transform.rotation = Quaternion.Euler(0, 0, networkVariable.Value.deg);
    }
}
