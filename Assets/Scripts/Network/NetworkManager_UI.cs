using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager_UI : MonoBehaviour
{
    [SerializeField]
    private Button serverBtn;
    [SerializeField]
    private Button clientBtn;
    [SerializeField]
    private Button hostBtn;

    private void Awake()
    {
        serverBtn.onClick.AddListener(StartServer);
        hostBtn.onClick.AddListener(StartHost);
        clientBtn.onClick.AddListener(StartClient);
    }

    private void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    private void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
