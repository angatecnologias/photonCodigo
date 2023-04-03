using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameConnection : MonoBehaviourPunCallbacks
{

   
    private void Awake()
    {

        Debug.Log("conectando ao servidor...");
        
        PhotonNetwork.LocalPlayer.NickName = "User_" + Random.Range(1, 1000);
        PhotonNetwork.ConnectUsingSettings();

    }

    //------------------------------------------------------------------------------------------------
    public override void OnConnectedToMaster()
    {
        Debug.Log("servidor conectado");
        if(PhotonNetwork.InLobby == false)
        {
            Debug.Log("entrando no lobby");
            PhotonNetwork.JoinLobby();
        }
    }

    //------------------------------------------------------------------------------------------------
    public override void OnJoinedLobby()
    {
        Debug.Log("entrei no lobby");

        Debug.Log("entrando na sala VistaExplodida");
        PhotonNetwork.JoinRoom("VistaExplodida");
    }

    //------------------------------------------------------------------------------------------------
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Erro ao entrar na sala" + message + " / codigo: " + returnCode);

        if(returnCode == ErrorCode.GameDoesNotExist)
        {
            Debug.Log("criando sala VistaExplodida");

            RoomOptions roomOption = new RoomOptions { MaxPlayers = 20 };
            PhotonNetwork.CreateRoom("VistaExplodida", roomOption, null);
        }
    }

    //------------------------------------------------------------------------------------------------

    public override void OnLeftRoom()
    {
        Debug.Log("voce saiu da sala VistaExplodida! seu username é:" + PhotonNetwork.LocalPlayer.NickName);
    }

    //------------------------------------------------------------------------------------------------
    public override void OnJoinedRoom()
    {
        Debug.Log("voce entrou na sala VistaExplodida! seu username é:" +  PhotonNetwork.LocalPlayer.NickName);

        Vector3 pos = new Vector3(Random.Range(-3.0f, 3.0f), 1, Random.Range(-3.0f, 3.0f));
        Quaternion rot = Quaternion.Euler(Vector3.up * Random.Range(0.0f, 360.0f));

        PhotonNetwork.Instantiate("XR Origin", pos, rot);
   
    }

    //------------------------------------------------------------------------------------------------

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("um jogador entrou na sala VistaExplodida! seu username é:" + newPlayer.NickName);
    }

    //------------------------------------------------------------------------------------------------
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
         Debug.Log("um jogador saiu da sala VistaExplodida! seu username era:" + otherPlayer.NickName);

    }

    //------------------------------------------------------------------------------------------------

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        Debug.Log("Aconteceu um erro" + errorInfo.Info);
    }
}
