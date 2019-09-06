using System;
using UnityEngine;
using WebSocketSharp;

public class WebSocketClient : MonoBehaviour
{
    WebSocketSharp.WebSocket m_ws;
    public string m_Message;
    bool m_isWSConnected = false;

    public RenderPoints m_Renderer = null;
    private Quaternion m_ParentCamRot;
    private Vector3 m_ParentCamPos;
    private Vector3 m_ParentCamScale;

    private bool m_isKFWMode = true;
    private bool m_isRotateCamera = false;
    private runLive m_AnyMethod;
    private Vector3 m_Offset;

    void ConnectWS()
    {
        if (m_isWSConnected)
            return;

        m_Message = "No data.";

        using (m_ws = new WebSocket("ws://a9dd09be.ngrok.io"))
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
        }
    }

    public void Start()
    {
        //Screen.SetResolution(1920, 1080, true);
        Application.runInBackground = true;

        //Screen.SetResolution(1920, 1080, true);
        Application.runInBackground = true;

        ConnectWS();

        if (m_Renderer == null)
            Debug.LogWarning("Set renderer in inspector.");
    }

    public void Update()
    {
        //Debug.Log(m_Message);
        m_Renderer.Render(m_Message);
    }

    public void OnApplicationQuit()
    {
        if (m_ws != null && m_ws.ReadyState == WebSocketState.Open)
            m_ws.Close();
    }

   
}