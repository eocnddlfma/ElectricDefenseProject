using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
   where T : MonoBehaviour
{
   private static T instance;
   public static T Instance
   {
      get
      {
         if (instance == null)
         {
            instance = FindAnyObjectByType<T>();
            if (instance == null)
            {
               Debug.LogError($"현재 Hierachy에 {typeof(T).Name} Script가 없습니다. ");
            }
         }

         return instance;
      }
   }


}
