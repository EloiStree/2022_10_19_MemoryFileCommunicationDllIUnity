using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryFileConnectionUtility;
using UnityEngine.Events;
using System;

public class Demo_FileConnectionWithMutex : MonoBehaviour
{
    public string m_fileName = "NoMutexTest";
    public UnityStringEvent m_received;
    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    void Start()
    {
        InvokeRepeating("Test", 0, 1.5f);
    }
    public void Test() {
        MemoryFileConnectionWithMutex connectionRecovert = new MemoryFileConnectionWithMutex(m_fileName, 1000000);
        connectionRecovert.SetText(DateTime.Now.ToString());
        connectionRecovert.GetAsText(out string t);
        m_received.Invoke(t);
    }
}
