using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class controll : MonoBehaviour
{
    public static string coin = "0";
    public Camera mainCam;
    public float interactionDistance = 15f;
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    [SerializeField] float speed = 7.3f;
    private float rotspeed = 90f;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private CharacterController controller;
    private float _initialYAngle = 0f;
    private float _appliedGyroYAngle = 0f;
    private float _calibrationYAngle = 0f;
    private Transform _rawGyroRotation;
    private float _tempSmoothing;
    private float _smoothing = 0.1f;
    public Text deviceName;
    private bool IsConnected;
    public static string dataRecived = "";
    private void Update()
    {
        ApplyGyroRotation();
        ApplyCalibration();
        transform.rotation = Quaternion.Slerp(transform.rotation, _rawGyroRotation.rotation, _smoothing);
        GetInput();
        if (IsConnected)
        {
            try
            {
                string datain = BluetoothService.ReadFromBluetooth();
                if (datain.Length > 1)
                {
                    dataRecived = datain;
                    print(dataRecived);
                    if (dataRecived == "0.00")
                    {
                        Vector3 move = new Vector3(Input.acceleration.x * speed * Time.deltaTime, 0f, -1 * speed * Time.deltaTime);
                        Vector3 rotMovement = transform.TransformDirection(move);
                        controller.Move(rotMovement);
                    }
                    else if (dataRecived == "4.00")
                    {
                        Vector3 move = new Vector3(Input.acceleration.x * speed * Time.deltaTime, 0f, 1 * speed * Time.deltaTime);
                        Vector3 rotMovement = transform.TransformDirection(move);
                        controller.Move(rotMovement);
                    }
                    else if (dataRecived == "5.00")
                    {
                        Vector3 move = new Vector3(Input.acceleration.x * speed * Time.deltaTime, 0f, 1 * speed * Time.deltaTime);
                        Vector3 rotMovement = transform.TransformDirection(move);
                        controller.Move(rotMovement);
                    }
                    else if (dataRecived == "10.00")
                    {
                        Vector3 move = new Vector3( -1 * speed * Time.deltaTime, 0f, -Input.acceleration.z * speed * Time.deltaTime);
                        Vector3 rotMovement = transform.TransformDirection(move);
                        controller.Move(rotMovement);
                    }
                    else if (dataRecived == "14.00")
                    {
                        Vector3 move = new Vector3( 1 * speed * Time.deltaTime, 0f, -Input.acceleration.z * speed * Time.deltaTime);
                        Vector3 rotMovement = transform.TransformDirection(move);
                        controller.Move(rotMovement);
                    }
                    else if (dataRecived == "15.00")
                    {
                        Vector3 move = new Vector3( 1 * speed * Time.deltaTime, 0f, -Input.acceleration.z * speed * Time.deltaTime);
                        Vector3 rotMovement = transform.TransformDirection(move);
                        controller.Move(rotMovement);
                    }
                    else if (dataRecived == "20.00")
                    {
                        InteractionRay();
                        coin = "1";
                    }
                    else if (dataRecived == "30.00")
                    {
                        coin = "2";
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

    }
    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();
                interactable.Interact();
            }
        }
        interactionUI.SetActive(hitSomething);
    }
    private IEnumerator Start()
    {
        Input.gyro.enabled = true;
        Application.targetFrameRate = 60;
        _initialYAngle = transform.eulerAngles.y;

        _rawGyroRotation = new GameObject("GyroRaw").transform;
        _rawGyroRotation.position = transform.position;
        _rawGyroRotation.rotation = transform.rotation;
        yield return new WaitForSeconds(1f);
        StartCoroutine(CalibrateYAngle());
        IsConnected = false;
        BluetoothService.CreateBluetoothObject();
        if (!IsConnected)
        {
            print(deviceName.text.ToString());
            IsConnected = BluetoothService.StartBluetoothConnection(deviceName.text.ToString());
        }
    }
    private IEnumerator CalibrateYAngle()
    {
        _tempSmoothing = _smoothing;
        _smoothing = 1f;
        _calibrationYAngle = _appliedGyroYAngle - _initialYAngle;
        yield return null;
        _smoothing = _tempSmoothing;
    }
    private void ApplyGyroRotation()
    {
        _rawGyroRotation.rotation = Input.gyro.attitude;
        _rawGyroRotation.Rotate(0f, 0f, 180f, Space.Self);
        _rawGyroRotation.Rotate(90f, 180f, 0f, Space.World);
        _appliedGyroYAngle = _rawGyroRotation.eulerAngles.y;
    }
    private void ApplyCalibration()
    {
        _rawGyroRotation.Rotate(0f, -_calibrationYAngle, 0f, Space.World);
    }
    public void SetEnabled(bool value)
    {
        enabled = true;
        StartCoroutine(CalibrateYAngle());
    }
    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += transform.forward * speed * Time.deltaTime;
        }
    }
}
