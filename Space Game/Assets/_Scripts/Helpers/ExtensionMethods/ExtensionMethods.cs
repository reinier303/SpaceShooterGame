using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasHelpers
{
    /// <summary>
    /// static extensions for methods I often use
    /// </summary>
    public static class ExtensionMethods
    {
        public static RectTransform LerpRectTransform(this RectTransform rectTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
        {
            return ActivateCoroutineLerpRectTransformPositions(rectTransform, owner, targetPosition, speed);
        }

        public static Transform LerpTransform(this Transform currentTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
        {
            return ActivateCoroutineLerpTransformPositions(currentTransform, owner, targetPosition, speed);
        }

        //public static Vector3 LerpVector(this Vector3 currentVector, MonoBehaviour owner, Vector3 targetPosition, float speed)
        //{
        //    
        //}

        public static List<T> ToList<T>(this T[] array) where T : class
        {
            List<T> output = new List<T>();
            output.AddRange(array);
            return output;
        }

        public static RectTransform ActivateCoroutineLerpRectTransformPositions(RectTransform rectTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
        {
            if (rectTransform == null) return null;

            if (owner != null)
            {
                owner.StartCoroutine(ExtensionHelpers.LerpRectTransformPositions(rectTransform, targetPosition, speed));
                return rectTransform;
            }
            else
            {
                Debug.Log("Our Owner is null");
                return null;
            }
        }

        public static Transform ActivateCoroutineLerpTransformPositions(Transform currentTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
        {
            if (currentTransform == null) return null;

            if (owner != null)
            {
                owner.StartCoroutine(ExtensionHelpers.LerpTransformPositions(currentTransform, targetPosition, speed));
                return currentTransform;
            }
            else
            {
                Debug.Log("Our Owner is null");
                return null;
            }
        }

        public static bool HasComponent<T>(this GameObject obj)
        {
            return obj.GetComponent(typeof(T)) != null;
        }

        public static Vector3 GetDirectionTo(this Vector3 from, Vector3 lookAt)
        {
            return lookAt - from;
        }
    }
}




