using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    protected bool m_IsDestroyOnLoad = false;
    static readonly string objName = $"{typeof(T).ToString()}";

    protected static T m_Instance;

    public static T Instance
    {
        get 
        {
            if (m_Instance == null)
            {
                SetupInstance();
            }

            return m_Instance; 
        }
    }

    private static void SetupInstance()
    {
        T[] objs = FindObjectsOfType<T>();
        if (objs.Length > 1)
        {
            foreach (var obj in objs)
            {
                if (obj.gameObject.name != objName) Destroy(obj.gameObject);
                else m_Instance = obj;
            }
        }
        else if (objs.Length == 1) m_Instance = objs[0];

        if (m_Instance == null)
        {
            Logger.LogWarning($"Cannot find {typeof(T)}, instantiating new object");
            m_Instance = new GameObject(objName).AddComponent<T>();
        }
    }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        CheckDuplicate();

        if(m_Instance == null)
        {
            m_Instance = (T)this;

            if(!m_IsDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void CheckDuplicate()
    {
        T[] objs = FindObjectsOfType<T>();
        if (objs.Length <= 1) return;

        foreach (var obj in objs)
        {
            if (obj.gameObject.name != objName) Destroy(obj.gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        Dispose();
    }

    protected virtual void Dispose()
    {
        m_Instance = null;
    }
}
