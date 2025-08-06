using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent(typeof(T)) as T;
                    obj.name = typeof(T).ToString();

                    //모든 씬에서 살아 있어야될 때 활성화
                    DontDestroyOnLoad(obj);
                }
            }

            return _instance;
        }
    }
}
