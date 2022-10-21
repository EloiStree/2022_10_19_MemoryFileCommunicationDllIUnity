using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryFileConnectionUtility;
using UnityEngine.Events;
using System;

public class Demo_FileConnectionWithMutex : MonoBehaviour
{
    public TargetMemoryFileInitation m_fileInfo = new TargetMemoryFileInitation() { m_fileName = "MutexTest", m_maxMemorySize = 1000000 };
    public UnityStringEvent m_received;
    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    void Start()
    {
        InvokeRepeating("Test", 0, 1.5f);
    }
    public void Test() {
       MemoryFileConnectionFacade.CreateConnection(MemoryFileConnectionType.WithMutex, 
            m_fileInfo, out IMemoryFileConnectionSetGet connectionRecovert);
        connectionRecovert.SetAsText(DateTime.Now.ToString());
        connectionRecovert.GetAsText(out string t, false);
        m_received.Invoke(t);
    }
}
