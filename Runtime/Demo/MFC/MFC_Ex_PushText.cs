using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityMemoryFileCommunication4Unity;
public class MFC_Ex_PushText : MonoBehaviour
{


    public MemoryFileConnectionDLLMono m_connection;
    [TextArea(0 , 5)] public string m_setText="Mon Ami";
    [TextArea(0 , 5)] public string m_appendAtStartText="Hey !";
    [TextArea(0 , 5)] public string m_appendAtEndText="Tu aimes �a les patates ?";
    [TextArea(0 , 5)]  public string m_recoveredText;
    [ContextMenu("SetText")]
    public void SetText()
    {
        m_connection.GetConnection().SetAsText(m_setText);
        m_connection.GetConnection().GetAsText(out m_recoveredText, false);
    }
    [ContextMenu("AppendStartText")]
    public void AppendStartText()
    {
        m_connection.GetConnection().AppendTextAtStart(m_appendAtStartText);
        m_connection.GetConnection().GetAsText(out m_recoveredText, false);
    }
    [ContextMenu("AppendEndText")]
    public void AppendEndText()
    {
        m_connection.GetConnection().AppendTextAtEnd(m_appendAtEndText);
        m_connection.GetConnection().GetAsText(out m_recoveredText, false);
    }
}
