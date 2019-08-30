using System;
using UnityEngine;
using WebSocketSharp;

public class OpenPoseWebSocket : MonoBehaviour
{
    WebSocketSharp.WebSocket m_ws;
    public string m_Message;
    bool m_isWSConnected = false;
    public string m_URL = "localhost";
    public string m_Port = "8080";

    public RenderPoints m_Renderer = null;

    void ConnectWS()
    {
        if (m_isWSConnected)
            return;

        m_Message = "No data.";

        var URL = "ws://bede6bd8.ngrok.io";
        //var URL = "ws://" + m_URL + ":" + m_Port;
        using (m_ws = new WebSocket(URL))
        {
            //m_ws.Log.Level = WebSocketSharp.LogLevel.TRACE;
            //m_ws.Log.File = "D:\\ws_log.txt";

            m_ws.OnOpen += (sender, e) =>
            {
                m_ws.Send(String.Format("Hello server."));
                Debug.Log("Connection opened.");
                m_isWSConnected = true;
            };
            m_ws.OnMessage += (sender, e) =>
            {
                m_Message = e.Data;
                //Debug.Log(m_Message);
            };
            m_ws.OnClose += (sender, e) =>
            {
                m_ws.Connect(); // This is a hack, but whatever
                m_isWSConnected = false;
            };
            m_ws.OnError += (sender, e) =>
            {
                // NOT PRINTING ERRORS
                //Debug.LogError(e.Message);
                m_isWSConnected = false;
            };

            m_ws.Connect();

            if (m_isWSConnected != true)
                Debug.LogWarning("Unable to connect to:" + URL);
        }
    }

    public void Start()
    {
        //Screen.SetResolution(1920, 1080, true);
        Application.runInBackground = true;

        //ConnectWS();

        if (m_Renderer == null)
            Debug.LogWarning("Set renderer in inspector.");
    } 

    void Update()
    {
        //Debug.Log(m_Message);
        m_Renderer.Render(m_Message);
    }

    public void OnApplicationQuit()
    {
        //if (m_ws != null && m_ws.ReadyState == WebSocketState.OPEN)
          //  m_ws.Close();
    }
}
