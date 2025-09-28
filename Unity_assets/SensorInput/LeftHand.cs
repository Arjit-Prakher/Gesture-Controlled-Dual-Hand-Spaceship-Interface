using System.IO.Ports;
using UnityEngine;

public class LeftHand : MonoBehaviour
{

    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] Transform _muzzle;
    [SerializeField]
    [Range(0f, 5f)] float _coolDownTime = 0.25f;


    public static LeftHand instance;
    public string portName = "COM6";
    public int baudRate = 115200;

    private SerialPort serial;
    private float accelX, accelY;
    private string receivedData;

    void OnApplicationQuit()
    {
        if (serial != null && serial.IsOpen)
        {
            serial.Close();
            Debug.Log(portName + " port closed successfully on exit.");
        }
    }
    private void Awake()
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
        if (serial.IsOpen && serial.BytesToRead > 0)
        {
            try
            {
                receivedData = serial.ReadLine();
                
                if (receivedData.Contains("FIRE!"))
                {
                    Debug.Log("YOU CAN FIRE");
                    FireProjectile();
                }
                
                string[] data = receivedData.Split(',');
                if (data.Length >= 1)
                {
                    accelY = float.Parse(data[0]);
                    accelX = float.Parse(data[1]);
                }
                
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Incomplete dataline: " + ex.Message);
            }

        }
    }
    void FireProjectile()
    {
        
        
        if (_projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is not assigned in the Inspector!");
            return;
        }
        if (_muzzle == null)
        {
            Debug.LogError("Muzzle Transform is not assigned in the Inspector!");
            return;
        }

        Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
        
    }


    public float AccelY { get { return accelY; } }
    public float AccelX { get { return accelX; } }
}
