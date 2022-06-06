using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ChatManager : MonoBehaviourPun
{
    //    public GameObject log;
    //    public GameObject messagePrefab;

    //    //private string message;
    //    //private string previousMessage;

    //    //void Update()
    //    //{
    //    //    if(message != previousMessage)
    //    //    {
    //    //        photonView.RPC("SendMessage", RpcTarget.All, message, messagePrefab, log);
    //    //    }
    //    //    previousMessage = message;
    //    //}

    //    [PunRPC]
    //    void SendMessage(string message, GameObject messagePrefab, GameObject log)
    //    {
    //        GameObject messageInstance = Instantiate(messagePrefab, log.transform);
    //        messageInstance.GetComponent<TMP_Text>().text = message;
    //    }

    //    public void LogMessage(string message)
    //    {
    //        //message = newMessage;
    //        GameObject messageInstance = Instantiate(messagePrefab, log.transform);
    //        messageInstance.GetComponent<TMP_Text>().text = message;
    //    }
}
