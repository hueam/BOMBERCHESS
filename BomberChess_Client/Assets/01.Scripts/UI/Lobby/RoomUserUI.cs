using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomUserUI : PoolableMono
{
    public bool IsOwner { set => crownImg.gameObject.SetActive(value); }

    [SerializeField] private Image checkImg;
    [SerializeField] private Image crownImg;
    [SerializeField] private TextMeshProUGUI userIdTxt;
    public void SetUser(int userId)
    {
        userIdTxt.text = userId.ToString();
    }
    public void SetReady(bool value)
    {
        checkImg.color = value ? Color.green : Color.white;
    }

    public override void Init()
    {
    }
}
