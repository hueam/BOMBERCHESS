using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 씬마다 여러 UI, Canvas가 있을 것이기 떄문에 이를 관리하기 위해 UIManager를 만들었음
/// 자세한건 UIContent에서 관리할 예정
/// </summary>
public class UIManager : MonoSingleton<UIManager>
{
    private UIContent _currentUI;
    public UIContent GetContent<T>() where T : UIContent
    {
        if(_currentUI != null) return _currentUI;

        _currentUI = FindObjectOfType<UIContent>();
        return _currentUI;
    }


}
