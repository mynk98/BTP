using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player_Network : NetworkBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private NetworkVariable<int> randomVariable = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    //private GameObject playerCamera;
    private void Update()
    {
        Debug.Log( OwnerClientId +":" + randomVariable.Value);
        if (!IsOwner)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            randomVariable.Value += 2;
        }

        // player movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += speed * Time.deltaTime * move;
    }
}
