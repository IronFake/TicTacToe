using UnityEngine;
using UnityEngine.SceneManagement;

namespace IronFake.Utils
{
    public class SingletonPersistent<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        // public static T Instance
        // {
        //     get {
        //         if (_instance == null)
        //         {
        //             Scene activeScene = SceneManager.GetActiveScene();
        //             SceneManager.SetActiveScene(SceneManager.GetSceneByName("Managers"));
        //             GameObject obj = new GameObject();
        //             obj.name = typeof(T).Name;
        //             obj.hideFlags = HideFlags.HideAndDontSave;
        //             _instance = obj.AddComponent<T>();
        //             SceneManager.SetActiveScene(activeScene);
        //         }
        //         return _instance;
        //     }
        // }

        public static T Instance => _instance;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}