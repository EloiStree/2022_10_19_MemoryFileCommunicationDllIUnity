using MemoryFileConnectionUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Demo_FileConnectionWithoutMutex : MonoBehaviour
{
    public TargetMemoryFileInitation m_fileInfo = new TargetMemoryFileInitation() { m_fileName = "NoMutexTest", m_maxMemorySize = 1000000 };
    public UnityStringEvent m_received;
    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    void Start()
    {
        InvokeRepeating("Test", 0, 1.5f);
    }
    public void Test()
    {
        MemoryFileConnectionFacade.CreateConnection(MemoryFileConnectionType.MemoryFileLocker,
             m_fileInfo, out IMemoryFileConnectionSetGet connectionRecovert);
        connectionRecovert.SetAsText(DateTime.Now.ToString());
        connectionRecovert.GetAsText(out string t, false);
        m_received.Invoke(t);
    }
}
