using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightSwitch : MonoBehaviour, IInteractable
{
    public Light m_Light;
    public Light m_Light1;
    public Light m_Light2;
    public Light m_Light3;
    public Light m_Light4;
    public bool isOn;
    public bool isOn1;
    public bool isOn2;
    public bool isOn3;
    public bool isOn4;
    void Start()
    {
        m_Light.enabled = isOn;
        m_Light1.enabled = isOn1;
        m_Light2.enabled = isOn2;
        m_Light3.enabled = isOn3;
        m_Light4.enabled = isOn4;
    }
    public string GetDescription()
    {
        if (isOn) return "����� [�������] <color=red>����</color> ����.";
        return "����� [�������] <color=green>���</color> ����.";
        if (isOn1) return "����� [�������] <color=red>����</color> ����.";
        return "����� [�������] <color=green>���</color> ����.";
        if (isOn2) return "����� [�������] <color=red>����</color> ����.";
        return "����� [�������] <color=green>���</color> ����.";
        if (isOn3) return "����� [�������] <color=red>����</color> ����.";
        return "����� [�������] <color=green>���</color> ����.";
        if (isOn4) return "����� [�������] <color=red>����</color> ����.";
        return "����� [�������] <color=green>���</color> ����.";
    }
    public void Interact()
    {
        isOn = !isOn;
        m_Light.enabled = isOn;
        isOn1 = !isOn1;
        m_Light1.enabled = isOn1;
        isOn2 = !isOn2;
        m_Light2.enabled = isOn2;
        isOn3 = !isOn3;
        m_Light3.enabled = isOn3;
        isOn4 = !isOn4;
        m_Light4.enabled = isOn4;
    }
}
