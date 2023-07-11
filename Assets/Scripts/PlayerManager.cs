using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : NetworkBehaviour
{
    public NetworkVariable<int> networkVariable = new NetworkVariable<int>(1, 
        NetworkVariableReadPermission.Everyone, 
        NetworkVariableWritePermission.Owner);

    public float playerSpeed;
    public GameObject playerObj;
    public int coinCount;
    public override void OnNetworkSpawn()
    {
        networkVariable.OnValueChanged += (int prevVal, int newVal) =>
        {
            Debug.Log(OwnerClientId + ": value = " + networkVariable.Value);

        };

    }
}
