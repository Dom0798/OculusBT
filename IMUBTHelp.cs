using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArduinoBluetoothAPI;
using System;

public class IMUBTHelp : MonoBehaviour
{
    BluetoothHelper bluetoothHelper;
    string deviceName;
    string payload;
    public GameObject CateterGuia;
    public GameObject CateterUS;
    //public GameObject CateterEmbrio;
    //public GameObject ultrasonido;
    string[] payloada;
    private float lerptime = 0.1f;
    private float currentlerptime = 0;
    private double Registro = 1;
    private Vector3 startPos; //start
    private Vector3 endPos; //end
    private Vector3 startPos2; //start
    private Vector3 endPos2; //end
    private Vector3 HallPos1;
    private Vector3 HallPos2;
    private Vector3 HallPos3;
    private Vector3 HallPos4;
    private Vector3 US1;
    private Vector3 US2;
    private Vector3 US3;
    private Vector3 US4;

    // Increase the speed/influence rotation
    public float factor = 1;
    // Start is called before the first frame update
    void Start()
    {
        deviceName = "VenusIMU";
        bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
        bluetoothHelper.OnConnected += OnConnected;
        bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
        bluetoothHelper.OnDataReceived += OnMessageReceived;
        bluetoothHelper.setTerminatorBasedStream("\n");
        bluetoothHelper.Connect();
        startPos = new Vector3(-0.15f, 60f, -10f);
        endPos = new Vector3(-0.15f, 60f, -10f);
        startPos2 = new Vector3(-80.9f, 90.5f, -118.24f);
        endPos2 = new Vector3(-80.9f, 90.5f, -118.24f);
        HallPos1 = new Vector3(-0.15f, 60f, -10f);
        HallPos2 = new Vector3(-0.15f, 60f, -7f);
        HallPos3 = new Vector3(-0.15f, 60f, -4f);
        HallPos4 = new Vector3(-0.15f, 60f, -1f);
        US1 = new Vector3(-80.9f, 90.5f, -118.24f);
        US2 = new Vector3(-81.57f, 91.05f, -117.14f);
        US3 = new Vector3(-82.24f, 91.68f, -115.87f);
        US4 = new Vector3(-83.12f, 92.5f, -114.21f);
        //Linea de codigo de prueba para referencia
        CateterGuia.transform.rotation = Quaternion.Euler(roll * factor, 0, pitch * factor);
    }
    void OnMessageReceived(BluetoothHelper helper)
    {
        payload = helper.Read();
        payloada = payload.Split(';');
        Debug.Log(payload);
    }

    void OnConnected(BluetoothHelper helper)
    {
        try
        {
            helper.StartListening();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    void OnConnectionFailed(BluetoothHelper helper)
    {
        Debug.Log("Connection Failed");
    }
    // Update is called once per frame
    void Update()
    {
        float roll = float.Parse(payloada[0]);
        float pitch = float.Parse(payloada[1]);
        float yaw = float.Parse(payloada[2]);
        byte position = byte.Parse(payloada[3]);
        CateterGuia.transform.rotation = Quaternion.Euler(roll * factor, yaw * factor, pitch * factor);
        //ultrasonido.transform.rotation = Quaternion.Euler(roll * factor - 47, yaw * factor + 180, pitch * factor);
        //CateterGuiaEmbrio.transform.rotation = Quaternion.Euler(curr_angle_roll2 * factor, curr_angle_yaw2 * factor, curr_angle_pitch2 * factor);
        if (payloada[3] == null)
        {

            return;
        }


        if (position == 1 && Registro == 2)
        {
            startPos = CateterGuia.transform.position = HallPos2;
            endPos = CateterGuia.transform.position = HallPos1;
            startPos2 = CateterUS.transform.position = US2;
            endPos2 = CateterUS.transform.position = US1;
            // For movement transform.Translate(Vector3.left * amountmove, Space.Self);
            //transform.position = new Vector3(0.14f, 1.206f, -4.267f);  //Versión 1.0
            // Para rotation transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Registro = 1;
            currentlerptime = 0;
        }
        if (position == 2 && Registro == 1)
        {
            startPos = CateterGuia.transform.position = HallPos1;//First position
            endPos = CateterGuia.transform.position = HallPos2;//Second position
            startPos2 = CateterUS.transform.position = US1;//First position
            endPos2 = CateterUS.transform.position = US2;//Second position
                                                           // For movement transform.Translate(Vector3.up * amountmove, Space.Self);
                                                           //transform.position = new Vector3(0.14f, 0.04f, 0.22f); //Versión1.0
            Registro = 2;
            currentlerptime = 0;
        }
        if (position == 2 && Registro == 3)
        {
            startPos = CateterGuia.transform.position = HallPos3;//First position
            endPos = CateterGuia.transform.position = HallPos2;//Second position
            startPos2 = CateterUS.transform.position = US3;//First position
            endPos2 = CateterUS.transform.position = US2;//Second position
                                                           // For movement transform.Translate(Vector3.up * amountmove, Space.Self);
                                                           //transform.position = new Vector3(0.14f, 0.04f, 0.22f); //Versión1.0
            Registro = 2;
            currentlerptime = 0;
        }
        if (position == 3 & Registro == 2)
        {
            startPos = CateterGuia.transform.position = HallPos2;//First position
            endPos = CateterGuia.transform.position = HallPos3; //Second position
            startPos2 = CateterUS.transform.position = US2;//First position
            endPos2 = CateterUS.transform.position = US3; //Second position
                                                            // For movement transform.Translate(Vector3.right * amountmove, Space.Self);
                                                            //transform.position = new Vector3(0.14f, -0.85f, 3.67f); //Versión1.0
            Registro = 3;
            currentlerptime = 0;
        }
        if  (position == 4 & Registro == 3)
        {
            startPos = CateterGuia.transform.position = HallPos3;//First position
            endPos = CateterGuia.transform.position = HallPos4; //Second position
            startPos2 = CateterUS.transform.position = US3;//First position
            endPos2 = CateterUS.transform.position = US4; //Second position
                                                            // For movement transform.Translate(Vector3.right * amountmove, Space.Self);
                                                            //transform.position = new Vector3(0.14f, -0.85f, 3.67f); //Versión1.0
            Registro = 4;
            currentlerptime = 0;
        }
        if (position == 3 & Registro == 4)
        {
            startPos = CateterGuia.transform.position = HallPos4;//First position
            endPos = CateterGuia.transform.position = HallPos3; //Second position
            startPos2 = CateterUS.transform.position = US4;//First position
            endPos2 = CateterUS.transform.position = US3; //Second position
                                                            // For movement transform.Translate(Vector3.right * amountmove, Space.Self);
                                                            //transform.position = new Vector3(0.14f, -0.85f, 3.67f); //Versión1.0
            Registro = 3;
            currentlerptime = 0;
        }
        currentlerptime += Time.deltaTime;
        if (currentlerptime >= lerptime)
        {
            currentlerptime = lerptime;
        }
        float Perc = currentlerptime / lerptime;
        CateterGuia.transform.position = Vector3.Lerp(startPos, endPos, Perc);
        CateterUS.transform.position = Vector3.Lerp(startPos2, endPos2, Perc);
        payloada[3] = null;
    }
}

