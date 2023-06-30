using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class howreble : MonoBehaviour
{
    void Start()
    {
        uimanager.SetOnHoverText(gameObject.name);
    }
    private void OnMouseExit()
    {
        uimanager.OffOnHoverText();
    }
}
