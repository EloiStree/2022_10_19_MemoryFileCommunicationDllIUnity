
using MemoryFileConnectionUtility;
using System;
using UnityEngine;




public class MemoryFileConnectionDLLMono : MonoBehaviour, IMemoryFileConnectionSetGet
{
    public MemoryFileConnectionDLL m_connection= new MemoryFileConnectionDLL();



    public void GetConnection(out IMemoryFileConnectionSetGet connection) {

        m_connection.CheckThatConnectionExist();
        connection = m_connection; }
    public IMemoryFileConnectionSetGet GetConnection() {

        m_connection.CheckThatConnectionExist();
        return m_connection; }
    public IMemoryFileConnectionSetGet Connection
    {
        get {
            m_connection.CheckThatConnectionExist();
            return m_connection; }
    }

    public void Awake()
    {
        m_connection.CheckThatConnectionExist();
    }

    void Reset()
    {
        m_connection.m_setupInfo = new TargetMemoryFileInitation();
        m_connection.m_setupInfo.m_fileName = Guid.NewGuid().ToString();

    }
    

    public void GetAsJson<T>(out T targetObject) => targetObject = m_connection.GetInObjectFromJsonFormat<T>();
    public void SetAsJson<T>(T targetObject) => m_connection.SetAsOjectInJsonFromat<T>(targetObject);
    public void GetAstexture2DWithDefault(out Texture2D m_textureRecovered)=>  m_connection.GetAsTexture2D(out m_textureRecovered);
    public void SetAsTextureWithDefault(Texture2D texture) => m_connection.SetAsTextureDefault(texture);


    public void SetAsRenderTextureDefault(in RenderTexture texture)
    {
        Eloi.E_Texture2DUtility.RenderTextureToTexture2D(in texture, out Texture2D t);
        SetAsTextureWithDefault(t);
    }
    public void SetAsTextureDefault(in Texture texture)
    {
        if (texture is RenderTexture)
            SetAsRenderTextureDefault((RenderTexture) texture);
        else if (texture is Texture2D)
              SetAsTextureWithDefault((Texture2D) texture);
    }

    public void SetWithGenericObject(object givenObject) {
       SetAsText(JsonUtility.ToJson(givenObject,true));
    }

    public void SetAsText(string text) => Connection.SetAsText(text);
    public void AppendTextAtStart(string text) => Connection.AppendTextAtEnd(text);
    public void AppendTextAtEnd(string text) => Connection.AppendTextAtStart(text);
    public void SetAsBytes(byte[] bytes) => Connection.SetAsBytes(bytes);
    public void SetWithNameAndSize(string name, int size)=>Connection.SetWithNameAndSize(name,  size);
    public void SetName(string name) =>Connection.SetName(name);
    public void SetMemorySize(int sizeInBit) =>Connection.SetMemorySize(sizeInBit);
    public void SetMemorySizeTo1MO() =>Connection.SetMemorySizeTo1MO();
    public void SetMemorySizeTo1KO() =>Connection.SetMemorySizeTo1KO();
    public void ResetToEmpty() =>Connection.ResetToEmpty();
    
    public void GetAsText(out string readText, bool removeContentAfter = false) =>
        Connection.GetAsText(out readText, removeContentAfter);
    public void GetAsBytes(out byte[] bytes, bool removeContentAfter = false) =>
        Connection.GetAsBytes(out bytes, removeContentAfter);
    public void Dispose() => Connection.Dispose();
    
}
[System.Serializable]
public class MemoryFileConnectionDLL : IMemoryFileConnectionSetGet
{
    public TargetMemoryFileInitation m_setupInfo;
    public MemoryFileConnectionType m_connectionType=MemoryFileConnectionType.MemoryFileLocker;
    private IMemoryFileConnectionSetGet m_connection;

    public void CheckThatConnectionExist()
    {
        if(m_connection==null)
        MemoryFileConnectionFacade.CreateConnection(m_connectionType,
            m_setupInfo, out m_connection) ;
    }
    public void SetNameThenReset(string fileName)
    {

        Dispose();
        m_setupInfo.m_fileName = fileName;
        MemoryFileConnectionFacade.CreateConnection(m_connectionType,
            m_setupInfo, out m_connection);
    }
    public void SetMaxSizeThenReset(string fileName)
    {
        Dispose();
        m_setupInfo.m_fileName = fileName;
        MemoryFileConnectionFacade.CreateConnection(m_connectionType,
            m_setupInfo, out m_connection);
    }

    public void Dispose()
    {
        if (m_connection != null)
        {
            m_connection.Dispose();
            m_connection = null;
        }
    }


    public void SetAsRenderTextureDefault(in RenderTexture texture)
    {
        Eloi.E_Texture2DUtility.RenderTextureToTexture2D(in texture, out Texture2D t);
        SetAsTextureDefault(t);

    }
    public void SetAsTextureDefault(in Texture texture)
    {
        if (texture is RenderTexture)
            SetAsRenderTextureDefault((RenderTexture)texture);
        else if (texture is Texture2D)
            SetAsTextureDefault((Texture2D)texture);

    }


    public void SetNameAndSizeThenReset(string fileName, int maxSize)
    {

        Dispose();
        m_setupInfo.m_fileName = fileName;
        m_setupInfo.m_maxMemorySize = maxSize;
        MemoryFileConnectionFacade.CreateConnection(m_connectionType,
            m_setupInfo, out m_connection);
    }


    public IMemoryFileConnectionSetGet Connection()
    {
        CheckThatConnectionExist();
        return m_connection;
    }
    public void SetAsBytes(byte[] bytes)
    {
        Connection().SetAsBytes(bytes);
    }
    public void SetAsText(string text)
    {
        Connection().SetAsText(text);
    }
    public void AppendTextAtEnd(string text)
    {
        Connection().AppendTextAtEnd(text);
    }
    public void AppendTextAtStart(string text)
    {
        Connection().AppendTextAtStart(text);
    }
   
    public void SetAsTexture2D_Heavy(RenderTexture renderTexture)
    {
        //THIS CODE CREATE MEMORY LEAK I TINK SO CHECK IF I IT RENDER TEXTURE TO TEXTURE OR SET BYTES
        //IS IT POSSIBLE THAT IAM SET BYTES WITH TO HEAVY IMAGE AND SO PUSH DATA OUT OF MY ZONE OF MEMORY AND LEAK ?
        Eloi.E_Texture2DUtility.RenderTextureToTexture2D(in renderTexture, out Texture2D texture);
        byte[] t = texture.EncodeToPNG();
        Connection().SetAsBytes(t);
    }
    public void SetAsTextureDefault(Texture2D texture)
    {
        byte[] t = texture.EncodeToPNG();
        Connection().SetAsBytes(t);
    }

    
    public void GetAsTexture2D(out Texture2D texture)
    {
        Connection().GetAsBytes(out byte[] bytes, false);
        texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.LoadImage(bytes);
        texture.Apply();
    }
    public void GetAsTexture2DAndFlush(out Texture2D texture)
    {
        Connection().GetAsBytes(out byte[] bytes, true);
        texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.LoadImage(bytes);
        texture.Apply();
    }


    public void SetAsOjectInJsonFromat<T>(T target)
    {
        string json = JsonUtility.ToJson(target);
        SetAsText(json);
    }
    public T GetInObjectFromJsonFormat<T>()
    {
        GetAsText(out string json,false);
        return JsonUtility.FromJson<T>(json);
    }
    public void GetInObjectFromJsonFormat<T>(out T recovered)
    {
        GetAsText(out string json);
        recovered = JsonUtility.FromJson<T>(json);
    }


    public void SetWithNameAndSize(string name, int size)
    {
        m_connection.SetWithNameAndSize(name, size);
    }

    public void SetName(string name)
    {
        m_connection.SetName(name);
    }

    public void SetMemorySize(int sizeInBit)
    {
        m_connection.SetMemorySize(sizeInBit);
    }

    public void SetMemorySizeTo1MO()
    {
        m_connection.SetMemorySizeTo1MO();
    }

    public void SetMemorySizeTo1KO()
    {
        m_connection.SetMemorySizeTo1KO();
    }

    public void ResetToEmpty()
    {
        m_connection.ResetToEmpty();
    }

    public void GetAsText(out string readText, bool removeContentAfter = false)
    {
        m_connection.GetAsText(out readText, removeContentAfter);
    }

    public void GetAsBytes(out byte[] bytes, bool removeContentAfter = false)
    {
        m_connection.GetAsBytes(out bytes, removeContentAfter);
    }

  
}
