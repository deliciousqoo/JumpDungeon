using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    protected bool _isDestroyOnLoad = false;
    protected static readonly string _objName = $"{typeof(T).ToString()}";
    protected static T _instance;

    public static T Instance
    {
        get 
        {
            if (_instance == null)
            {
                SetupInstance();
            }

            return _instance; 
        }
    }

    private static void SetupInstance()
    {
        T[] objs = FindObjectsOfType<T>();
        if (objs.Length > 1)
        {
            foreach (var obj in objs)
            {
                if (obj.gameObject.name != _objName) Destroy(obj.gameObject);
                else _instance = obj;
            }
        }
        else if (objs.Length == 1) _instance = objs[0];

        if (_instance == null)
        {
            Logger.LogWarning($"Cannot find {typeof(T)}, instantiating new object");
            _instance = new GameObject(_objName).AddComponent<T>();
        }
    }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        CheckDuplicate();

        if(_instance == null)
        {
            _instance = (T)this;

            if(!_isDestroyOnLoad)
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
            if (obj.gameObject.name != _objName) Destroy(obj.gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        Dispose();
    }

    protected virtual void Dispose()
    {
        _instance = null;
    }
}
