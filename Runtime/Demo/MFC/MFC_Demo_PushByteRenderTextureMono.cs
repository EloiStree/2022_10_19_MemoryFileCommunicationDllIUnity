using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFC_Demo_PushByteRenderTextureMono : MonoBehaviour
{

    public RenderTexture m_renderTexutre;
    public Eloi.PrimitiveUnityEvent_BytesArray m_bytesArrayEvent;

    public int m_width = 256, m_heigt = 256;
    public Texture2D m_pushed;

    public int m_bytesCount;
    public int m_bitsCount;
    [ContextMenu("Push")]
    public void Push() {

        Eloi.E_Texture2DUtility.RenderTextureToTexture2D(m_renderTexutre, out Texture2D texture);
        TextureScale.Scale(texture, m_width, m_heigt);
        m_pushed = texture;
        byte[] b= texture.EncodeToPNG();
        m_bytesCount = b.Length;
        m_bitsCount = b.Length*8;
        m_bytesArrayEvent.Invoke(b);
        //87352


    }

}
