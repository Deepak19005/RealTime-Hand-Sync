//Integrated code to simulate 3 fingers together

//Below is the unity c# code to map the flex value to the finger rotation

using UnityEngine;
using System.IO.Ports;

public class FlexSensorController1 : MonoBehaviour
{
    SerialPort serialPort;
    public string portName = "COM3";  // Change this to your Arduino's port
    public int baudRate = 9600;        // Match this with your Arduino's baud rate
    public Transform middle1;
    public Transform middle2;
    public Transform middle3;
    public Transform index1;
    public Transform index2;
    public Transform index3;
    public Transform thumb1;
    public Transform thumb2;
    public Transform thumb3;
    public float minValue1 = 982f;     
    public float maxValue1 = 895f;
    public float minValue2 = 972f;
    public float maxValue2 = 920f; 
    public float minValue3 = 977f;
    public float maxValue3 = 913f;

    public float minAngle = 0f;        // Minimum angle of rotation (corresponding to minValue)
    public float maxAngle = 90f;       // Maximum angle of rotation (corresponding to maxValue)

    void Start()
    {
        // Initialize the serial port
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();

        // Uncomment the line below if you need to read existing data in the buffer
        // serialPort.ReadExisting();

        // Ensure the script is not updating too frequently
        InvokeRepeating("ReadFlexSensor", 0f, 0.1f);  // Adjust the interval as needed
    }

    void ReadFlexSensor()
    {
        if (serialPort.IsOpen)
        {
            try
            {
  
                string data = serialPort.ReadLine();
                Debug.Log("Received Data: " + data);
                // Split the received data into an array based on commas
                string[] flexValues = data.Split(',');

                // Check if there are three values
                int flexValue1;
                int flexValue2;
                int flexValue3;
                if (flexValues.Length == 3)
                {
                    // Convert each string value to an integer
                    flexValue1 = int.Parse(flexValues[0]);
                    flexValue2 = int.Parse(flexValues[1]);
                    flexValue3 = int.Parse(flexValues[2]);
                    float mappedAngle1 = Map(flexValue1, minValue1, maxValue1, minAngle, maxAngle);
                    float mappedAngle2 = Map(flexValue2, minValue2, maxValue2, minAngle, maxAngle);
                    float mappedAngle3 = Map(flexValue3, minValue3, maxValue3, minAngle, maxAngle);
                    thumb1.localRotation = Quaternion.Euler(mappedAngle1, 0f, 0f);
                    thumb2.localRotation = Quaternion.Euler(mappedAngle1, 0f, 0f);
                    thumb3.localRotation = Quaternion.Euler(mappedAngle1, 0f, 0f);
                    index1.localRotation = Quaternion.Euler(mappedAngle2, 0f, 0f);
                    index2.localRotation = Quaternion.Euler(mappedAngle2, 0f, 0f);
                    index3.localRotation = Quaternion.Euler(mappedAngle2, 0f, 0f);
                    middle1.localRotation = Quaternion.Euler(mappedAngle3, 0f, 0f);
                    middle2.localRotation = Quaternion.Euler(mappedAngle3, 0f, 0f);
                    middle3.localRotation = Quaternion.Euler(mappedAngle3, 0f, 0f);
                }
                else
                {
                    Debug.LogError("Received data does not contain three values separated by commas.");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error reading from the serial port: " + e.Message);
            }
        }
    }

    float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        // Linear mapping function
        return Mathf.Clamp((value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin, outMin, outMax);
    }

    void OnDestroy1()
    {
        // Close the serial port when the script is destroyed or the application quits
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}