using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
    public GameObject PlayerController;

    void Start()
    {
        Connect();
    }

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("MultiPlayer v0.0.1");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {       
        SpawnMyPlayer();
    }

    void SpawnMyPlayer()
    {
        GameObject player = (GameObject)PhotonNetwork.Instantiate("PlayerController", Vector3.zero, this.gameObject.transform.rotation, 0);
        ((MonoBehaviour)player.GetComponent("CharacterMovement")).enabled = true;
        ((MonoBehaviour)player.GetComponent("CharacterAttacks")).enabled = true;
    }

    void SpawnPlayer()
    {
        GameObject player = (GameObject)PhotonNetwork.Instantiate("PlayerController", Vector3.zero, this.gameObject.transform.rotation, 0);
    }
}
