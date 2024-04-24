using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityMemoryFileCommunication4Unity;

public class MFC_PushLargeBytesWithDateUpdate : MonoBehaviour
{
    public MemoryFileConnectionDLLMono m_whereToPushBytes;
    public MemoryFileConnectionDLLMono m_whereToPushDate;

    public void PushBytes( byte[] bytesToPush) {

        m_whereToPushBytes.SetAsBytes(bytesToPush);
        m_whereToPushDate.SetAsText(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
    }
}
