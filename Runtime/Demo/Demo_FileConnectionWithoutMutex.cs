using MemoryFileConnectionUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Demo_FileConnectionWithoutMutex : MonoBehaviour
{
    public string m_fileName = "NoMutexTest";
    public UnityStringEvent m_received;
    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    void Start()
    {
        InvokeRepeating("Test", 0, 1.5f);
    }
    public void Test()
    {
        MemoryFileConnectionNoMutexLocker connectionRecovert = new MemoryFileConnectionNoMutexLocker(m_fileName, 1000000);
        connectionRecovert.SetText(DateTime.Now.ToString());
        connectionRecovert.TextRecovering(out string t, false);
        m_received.Invoke(t);
    }
}
