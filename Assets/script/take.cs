using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class take : MonoBehaviour
{
    public GameObject camera;
    public float distance = 15f;
    GameObject currentWeapon;
    bool canPickUp = false;
    private void Update()
    {
        if (controll.coin == "1")
        {
            PickUp();
            controll.coin = "5";
        }
        else if (controll.coin == "2")
        {
            Drop();
            controll.coin = "5";
        }
    }
    void PickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "hend")
            {
                if (canPickUp) Drop();
                currentWeapon = hit.transform.gameObject;
                currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
                currentWeapon.GetComponent<Collider>().isTrigger = true;
                currentWeapon.transform.parent = transform;
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                canPickUp = true;
            }
        }
    }
    void Drop()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon.GetComponent<Collider>().isTrigger = false;
        canPickUp = false;
        currentWeapon = null;
    }
}