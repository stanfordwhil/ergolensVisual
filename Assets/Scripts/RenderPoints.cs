using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[l_lower_arm, r_lower_arm, l_upper_arm, r_upper_arm, l_upper_leg, r_upper_leg, l_lower_leg, r_lower_leg]
//[6, 3, 2, 5, 9, 12, 10, 13]

public class RenderPoints : MonoBehaviour
{
    // -51.2245,261.572,986.43,1,-54.6432,243.051,1164.42,1,-206.451,220.645,1110.4,1,-334.342,415.625,774.275,1,0,0,0,0,96.4856,256.573,1115.06,1,91.7183,521.088,1172.87,1,-19.0477,553.402,997.359,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5.10916,600.942,1064.61,1,125.199,586.948,753.594,1,0,0,0,0,-80.015,223.204,976.69,1,-15.8261,234.037,977.123,1,-223.245,154.978,623.824,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
    // 238.297,-217.648,678.516,1,429.97,-137.133,726.299,1,444.511,-165.009,895.523,1,0,0,0,0,109.565,215.482,714.319,1,420.147,-114.091,585.392,1,420.167,177.402,571.087,1,207.084,105.137,674.58,1,383.392,419.095,844.992,1,413.67,427.562,1042.36,1,0,0,0,0,0,0,0,0,360.727,412.529,675.347,1,0,0,0,0,0,0,0,0,0,0,0,0,253.588,-243.46,627.898,1,0,0,0,0,358.56,-271.608,606.193,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
    public Vector3[] m_Joints;
    public GameObject[] m_JointSpheres;
    public GameObject[] m_links;
    public GameObject[] m_texts;
    private Vector2[] links = { new Vector2(1, 8), new Vector2(1, 2), new Vector2(1, 5), new Vector2(2, 3), new Vector2(3, 4), new Vector2(5, 6), new Vector2(6, 7), new Vector2(8, 9), new Vector2(9, 10), new Vector2(10, 11), new Vector2(8, 12), new Vector2(12, 13), new Vector2(13, 14), new Vector2(1, 0), new Vector2(0, 15), new Vector2(15, 17), new Vector2(0, 16), new Vector2(16, 18), new Vector2(14, 19), new Vector2(19, 20), new Vector2(14, 21), new Vector2(11, 22), new Vector2(22, 23), new Vector2(11, 24) };
    private int[] calculatedAngles = { 6, 3, 2, 5, 9, 12, 10, 13 };

    public void Start()
    {
        m_Joints = new Vector3[25];

        m_JointSpheres = new GameObject[25];
        m_links = new GameObject[25];
        m_texts = new GameObject[25];

        for (int i = 0; i < 25; ++i)
        {
            m_JointSpheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            m_texts[i] = new GameObject();
            TextMesh textMesh = new TextMesh();
            m_texts[i].AddComponent<TextMesh>();
        
            textMesh = m_texts[i].GetComponent<TextMesh>();
            textMesh.text = "";//i.ToString();
            textMesh.color = Color.red;
            textMesh.fontSize = 10;
            textMesh.characterSize = 10;
            MeshRenderer m = m_texts[i].GetComponent<MeshRenderer>();
            m.enabled = true;
            m_JointSpheres[i].transform.localScale = new Vector3(5f, 5f, 5f);
            if (i < 24)
            {
                m_links[i] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                m_links[i].transform.localScale = new Vector3(2f, 2f, 2f);

            }
        }
    }

    public void Render(string JointsMsg)
    {
        string Dummy = "238297,-217648,678516,1,42997,-137133,726299,1,444511,-165009,895523,1,0,0,0,0,109.565,215482,714319,1,420147,-114091,585392,1,420167,177402,571087,1,207084,105137,67458,1,383392,419095,844992,1,41367,427562,104236,1,0,0,0,0,0,0,0,0,360727,412529,675347,1,0,0,0,0,0,0,0,0,0,0,0,0,253588,-24346,627898,1,0,0,0,0,35856,-271608,606193";
        //string Dummy = "0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0,0,0,0,100000,100000,100000,0,0,0,100000,100000,100000,0,0,0";
        //JointsMsg = Dummy;

        Debug.Log(JointsMsg.Length);
        string[] temp = JointsMsg.Split(';');
        string jointlist = temp[0];
        string anglesList = temp[1];
        string[] angles = anglesList.Split(',');
        string[] XYZs = jointlist.Split(',');
        //Debug.Log(XYZs.Length);
        if (XYZs.Length != 25*3)
        {
            Debug.Log(XYZs.Length);
            Debug.LogWarning("String mismatch");
            return;
        }

        

        for (int i = 0; i < 25; ++i)
        {
         
            Vector3 zero = new Vector3(0, 100, 0);
            Vector3 txtmove = new Vector3(0, 0, 0);
            m_Joints[i] = new Vector3(float.Parse(XYZs[3 * i + 0]) / 10000, -float.Parse(XYZs[3 * i + 1]) / 10000 + 100, float.Parse(XYZs[3 * i + 2]) / 10000);
            m_JointSpheres[i].transform.position = m_Joints[i];
            m_texts[i].transform.position = m_Joints[i] + txtmove;
            

            if (m_Joints[i] == zero)
            {
                m_JointSpheres[i].GetComponent<Renderer>().enabled = false;
                m_texts[i].GetComponent<Renderer>().enabled = false;
            }
            else
            {
                m_JointSpheres[i].GetComponent<Renderer>().enabled = true;
                m_texts[i].GetComponent<Renderer>().enabled = true;
                MeshRenderer m = m_texts[i].GetComponent<MeshRenderer>();
                m.enabled = true;
            }

        }

        for (int i = 0; i < 8; i++)
        {
            TextMesh textMesh = m_texts[calculatedAngles[i]].GetComponent<TextMesh>();
            textMesh.text = angles[i] + "°";
        }

        for (int i =  0; i < 24; i++)
        {
            Vector3 bondDistance = new Vector3(0, 0, 0);
            Vector3 tmp = m_links[i].transform.localScale;
            tmp.z = Vector3.Distance(m_Joints[(int)links[i].x], m_Joints[(int)links[i].y]);
            m_links[i].transform.localScale = tmp;
            m_links[i].transform.position = (m_Joints[(int)links[i].x] + m_Joints[(int)links[i].y])/2;
            m_links[i].transform.LookAt(m_Joints[(int)links[i].y]);

            if (m_JointSpheres[(int)links[i].y].GetComponent<Renderer>().enabled == false || m_JointSpheres[(int)links[i].x].GetComponent<Renderer>().enabled == false)
                m_links[i].GetComponent<Renderer>().enabled = false;
            else
                m_links[i].GetComponent<Renderer>().enabled = true;

        }

        

        //Debug.Log(m_Joints[0]);
    }
}

/*1,8,   1,2,   1,5,   2,3,   3,4,   5,6,   6,7,   8,9,   9,10,  10,11, 8,12,  12,13, 13,14,  1,0,   0,15, 15,17,  0,16, 16,18,   14,19,19,20,14,21, 11,22,22,23,11,24
var cylinderRef : Transform;
var aTarget : Transform;
var spawn : Transform;
 
function Start()
{
    // Find the distance between 2 points
    var bondDistance : Vector2;
    cylinderRef.localScale.z = bondDistance.Distance(spawn.position, aTarget.position) / 2;

    cylinderRef.position = spawn.position;        // place bond here
    cylinderRef.LookAt(aTarget);            // aim bond at atom
}

function Update()
{
}
*/
