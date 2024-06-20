using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모노 싱글톤 편하게 싱글톤을 만들이 위해 만듬
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance 
    {get {
        if(_instance == null)
        {
            _instance = FindObjectOfType<T>();
        }    
        return _instance;
    }}

}
