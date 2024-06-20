using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// Canvas및 해당 UI의 기능을 제작한다. 
/// 추상화 클래스로 만듬으로써 여러 캔버스에서 사용 가능하게함
/// 상속받는 어떠한 클래스가 자신이 필요한 무언가를 만들거임
/// </summary>
public abstract class UIContent : MonoBehaviour
{
    protected Canvas _canvas;

    protected virtual void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
    }
    public virtual void ActiveCanvas(bool value)
    {
        gameObject.SetActive(false);
    }
}
