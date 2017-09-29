﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;
using UnityEngine.UI;

public class Server : MonoBehaviour 
{
    private List<ServerClient> clients;
    private List<ServerClient> disconectedList;

    public int port = 6321;
    private TcpListener server;
    private bool serverStarted;

    public Text LoginText; 
    public static Vector3 RelativePosition;
    public static Quaternion Rotation;

    private Vector3 LastRelativePosition;

    public bool AllowBroadcastData;

    private void Start() {
        clients = new List<ServerClient>();
        disconectedList = new List<ServerClient>();

        try 
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            serverStarted = true;
            Debug.Log("Server has been started on port: " + port.ToString());
        }
        catch (Exception e) 
        {
            Debug.Log("Socket error: " + e.Message);
        }

    }

    private void FixedUpdate() {
        if (!serverStarted)
            return;

        foreach (ServerClient c in clients) {

            //Is the client still connected?
            if(!IsConnected(c.tcp)) {
                LoginText.enabled = false;
                c.tcp.Close();

                disconectedList.Add(c);
                continue;
            }
            //check for message from the client
            else {
                LoginText.enabled = true;
                NetworkStream s = c.tcp.GetStream();
                if(s.DataAvailable) {
					Debug.Log ("data available");
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if (data != null)
                        OnIncomingData(c, data);
                }
                else if (AllowBroadcastData) {
                    Vector3 relativePosition = Transformer.PositionRelativeToJaw;
                    if (LastRelativePosition != relativePosition) {
						Debug.Log ("sending data");
                        LastRelativePosition = relativePosition;
                        string message = relativePosition.ToString() + "1";
                        //Debug.Log("sentMessage: " + message);
                        Broadcast(message, clients);
                    }
                }
            }
        }
        for (int i = 0; i < disconectedList.Count - 1; i++) {

            Broadcast(disconectedList[i].clientName + " has disconnected", clients);

            clients.Remove(disconectedList[i]);
            disconectedList.RemoveAt(i);
        }
    }

    private void StartListening() {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }
    private bool IsConnected(TcpClient c) {
        try {
            if(c !=null && c.Client != null && c.Client.Connected) {
                if (c.Client.Poll(0, SelectMode.SelectRead)) {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }
                return true;
            } else
                return false;
        }
        catch {
            return false;
        }
    }
    private void AcceptTcpClient(IAsyncResult ar) {
        TcpListener listener = (TcpListener)ar.AsyncState;

        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        StartListening();

        //Send message to everyone, say someone has connected
        Broadcast(clients[clients.Count -1].clientName + " has connected", clients);
    }

    private void OnIncomingData(ServerClient c, string data) {
        //All data sent by the client
        if (TransformType(data) == 1) //postion
            if (data.StartsWith("(")) RelativePosition = StringToVector3(data);

        /*if (TransformType(data) == 2) //rotation 
            Rotation = StringToQuaternion(data);*/
        //Broadcast(data, clients);
    }

    private void Broadcast(string data, List<ServerClient> cl) {
        foreach (ServerClient c in cl) {
            try {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            } catch (Exception e) {
                Debug.Log("Write error: " + e.Message + " to client: " + c.clientName);
            }
        }
    }

    private int TransformType(string data) {
        data = data.Substring(data.Length - 1);
        return int.Parse(data);
    }

    private Vector3 StringToVector3(string sVector) {
        //Debug.Log("Before string: " + sVector);
        // Remove the parentheses
        sVector = sVector.Substring(1, sVector.Length - 3);
        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
    
    private Quaternion StringToQuaternion(string sQuaternion) {
        // Remove the parentheses
        if (sQuaternion.StartsWith("(")) {
            sQuaternion = sQuaternion.Substring(1, sQuaternion.Length - 3);
        }
        // split the items
        string[] sArray = sQuaternion.Split(',');

        // store as a Vector3
        Quaternion result = new Quaternion(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]), 
            float.Parse(sArray[3]));

        return result;
    }

}

public class ServerClient : MonoBehaviour 
{
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket) 
    {
        clientName = "Guest";
        tcp = clientSocket;
    }
}
