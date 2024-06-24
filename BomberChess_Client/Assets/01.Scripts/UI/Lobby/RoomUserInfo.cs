using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomUserInfo : MonoBehaviour
{
    [SerializeField]private Image checkImg;
    [SerializeField]private TextMeshProUGUI userIdTxt;
    public void SetUser(int userId)
    {
        userIdTxt.text = userId.ToString();
    }
    public void SetReady(bool value)
    {
        checkImg.gameObject.SetActive(value);
    }
}
