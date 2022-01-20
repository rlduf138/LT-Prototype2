using UnityEngine;


namespace PixelSilo.Helper
{
    /// <summary>
    /// 싱글톤 클래스입니다.
    /// MonoBehaviour 클래스가 싱글톤이 되길 원할 경우.
    /// public class mySingleton : Singleton<mySingleton> {}
    /// 의 형태로 클래스를 만들어 주시면 되겠습니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour 
        where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        GameObject TObject = new GameObject(typeof(T).Name);
                        _instance = TObject.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }
    }
}