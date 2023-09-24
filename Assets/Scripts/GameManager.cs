using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Chat.UtilityScripts;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]

    //public Transform spawnLocation; 
    private GameObject playerPrefab;
    private bool isPlayerSpawned = false; 

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (playerPrefab != null && !isPlayerSpawned)
            {
                /*int xRandomPoint = Random.Range(-15, 15);
                int zRandomPoint = Random.Range(-15, 15);
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(xRandomPoint, 0, zRandomPoint), Quaternion.identity);
                isPlayerSpawned = true;*/

                StartCoroutine(DelayedPlayerSpawn());
            }
        }
    }

    public IEnumerator DelayedPlayerSpawn()
    {
        yield return new WaitForSeconds(3); 
        int xRandomPoint = Random.Range(-15, 15);
        int zRandomPoint = Random.Range(-15, 15);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(xRandomPoint, 0, zRandomPoint), Quaternion.identity);
        isPlayerSpawned = true;
        //isPlayerSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " has joined the room!"); 
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " has joined the room " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Room has now " + PhotonNetwork.CurrentRoom.PlayerCount + "/20"); 
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("01_GameLauncherScene");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        this.isPlayerSpawned = false;
    }
}
