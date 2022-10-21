using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFC_Demo_RecoverByteRenderTextureMono : MonoBehaviour
{

    public Texture2D m_texture;
    public int m_width=256, m_heigt=256;
    [ContextMenu("Recover")]
    public void Recover(byte [] values)
    {
        m_texture  = new Texture2D(m_width, m_heigt);
        m_texture.LoadImage(values);


    }
}
