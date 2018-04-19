using Buildings;
using UnityEngine;

namespace Utilities
{
    public class SharedSceneObjects : MonoBehaviour
    {
        static SharedSceneObjects instance;

        public static void DestroySharedObjectInstance()
        {
            Object.Destroy(SharedSceneObjects.instance.gameObject);
        }

        void Awake()
        {
            if (SharedSceneObjects.instance != null)
            {
                Object.Destroy(this.gameObject);
                return;
            }

            SharedSceneObjects.instance = this;
            
            DontDestroyOnLoad(this.gameObject);
        }

        void OnApplicationQuit()
        {
            IBuildingService buildingService = ServiceLocator.Instance.GetService<IBuildingService>();
            buildingService.Save();
        }
    }
}