using System.Collections;
using System.Globalization;
using System.IO.Ports;
using UnityEngine;

public class ConnectionArdunio : MonoBehaviour
{
    public static ConnectionArdunio instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    SerialPort data_stream = new SerialPort("COM6", 9600);

    [HideInInspector]
    public float _pDistance;

    string _recieveStr;
    private void Start()
    {
        data_stream.Open();
    }

    public void SetLight(bool setVal)
    {
        if(setVal)
        {
            data_stream.WriteLine("y");
        }
        else
        {
            data_stream.WriteLine("n");
        }
    }
    private void Update()
    {
        _recieveStr = data_stream.ReadLine();
        _recieveStr = _recieveStr.Split(':')[1].Trim();
        if (float.TryParse(_recieveStr, out _pDistance))
        {
            //Here you can put code that will only run if the text is a valid number
            _pDistance *= 0.034f / 2f;
        }
        else
        {
            Debug.Log("Text is not a valid number  "+ _recieveStr);
            //You can replace this with whatever code you want to run if the text is not a number, perhaps show some text telling the player that they have insert a incorrect string
        }
    }

    private void OnDestroy()
    {
        data_stream.WriteLine("n");
    }
}
