using UnityEngine;
using System.IO.Ports;
// THIS IS FOR THE RIGHT HAND.
public class RightHand : MonoBehaviour
{
    // Make this class a Singleton so other scripts can access it easily.
    public static RightHand instance;

    public string portName = "COM3"; // Change this to your Arduino's COM port
    public int baudRate = 115200;

    private SerialPort serial;
    private float accelY;
    private string receivedData;

    void OnApplicationQuit()
    {
        if (serial != null && serial.IsOpen)
        {
            serial.Close();
            Debug.Log(portName + " port closed successfully on exit.");
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Set up the serial connection
        serial = new SerialPort(portName, baudRate);
        serial.ReadTimeout = 1;

        try
        {
            serial.Open();
            //Debug.Log("Serial Port Opened: " + portName);
            //Debug.Log("Getting things ready!!");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error opening serial port: " + ex.Message);
        }
    }

    void Update()
    {
        // Check if data is available and read it


        if (serial.IsOpen && serial.BytesToRead > 0)
        {
            // Debug.Log("Sensors working!");
            try
            {
                receivedData = serial.ReadLine();

                    string[] data = receivedData.Split(',');
                    if (data.Length >= 1)
                    {
                        
                        accelY = float.Parse(data[0]);
                    }
                
            }
            catch (System.Exception ex)
            {
                // This is a common error if a line is incomplete, just ignore it.
                Debug.LogWarning("Incomplete data line: " + ex.Message);
            }
        }
    }

    
    public float AccelY { get { return accelY; } }
   
}