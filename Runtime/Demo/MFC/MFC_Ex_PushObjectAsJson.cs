using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityMemoryFileCommunication4Unity;
public class MFC_Ex_PushObjectAsJson : MonoBehaviour
{
    public MemoryFileConnectionDLLMono m_connection;
    [System.Serializable]
    public class LevelName {
        public string m_levelName;
        public int m_levelIndex;
        public float m_difficultyPercent;
    }

    [System.Serializable]
    public class Donjon {
        public LevelName[] m_levelInDonjon;
    }
    public Donjon m_donjonToPush;
    public Donjon m_donjonRecovered;

    [ContextMenu("Push and recover")]
    public void PushAndRecoverDonjon() {
        m_connection.SetAsJson<Donjon>(m_donjonToPush);
        m_connection.GetAsJson<Donjon>(out m_donjonRecovered);
    }

}
