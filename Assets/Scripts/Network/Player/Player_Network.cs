using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Network : NetworkBehaviour
{
    [SerializeField]
    private float speed = 5f;

    // Custom data for player to sync over network

    public struct PlayerInfo : INetworkSerializable
    {
        public int point;
        public FixedString32Bytes name;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref point);
            serializer.SerializeValue(ref name);
        }
    }


    public override void OnNetworkSpawn()
    {
        void value(PlayerInfo prevValue, PlayerInfo newValue)
        {
            Debug.Log(OwnerClientId + "Random variable changed from " + prevValue.point + " to " + newValue.point);
        }
        randomVariable.OnValueChanged += value;
    }

    private NetworkVariable<PlayerInfo> randomVariable = new(
        new PlayerInfo { point = 0, name = "Player"}, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    //private GameObject playerCamera;
    private void Update()
    {
        //Debug.Log( OwnerClientId +":" + randomVariable.Value);
        if (!IsOwner)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            randomVariable.Value = new PlayerInfo{
                point = 1
            };
        }

        // player movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += speed * Time.deltaTime * move;
    }

    // ***********************-------RPC-------***********************************
    /*
     * Function Name must end with the RPC call name
     * ex:     [ClientRpc] -> TestClientRpc()
     * 
     * Server RPC call will only run on server but any client can call the function
     * 
     * TO access networking every game object need to have NetworkGameObject script attached.
     * And should be added to network prefab list in Network manager.
     * 
     */

    [ClientRpc]
    public void TestClientRpc()
    {
        Debug.Log("ClientRpcTest");
    }

    //[ServerRpc]
    



}
