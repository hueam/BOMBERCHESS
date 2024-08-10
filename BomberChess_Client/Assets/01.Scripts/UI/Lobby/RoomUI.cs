using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : UIObject
{
    protected override void OnOpen()
    {
        
    }
    protected override void OnClose()
    {
        foreach (var info in _id2Info)
        {
            PoolManager.Instance.Push(info.Value);
        }
        _id2Info.Clear();

        C_Leaveroom packet = new C_Leaveroom();
        packet.UserId = NetworkManager.Instance.userId;
        packet.RoomId = NetworkManager.Instance.roomId;
        NetworkManager.Instance.Send(packet);
        base.Close();
    }
    private Dictionary<int, RoomUserUI> _id2Info = new();
    private int _onwerId = 0;
    [SerializeField] private Transform _userGroup;
    [SerializeField] private GameObject _startBtn;
    public void EnterRoom(bool isOwner = false)
    {
        _startBtn.SetActive(isOwner);
        AddUser(NetworkManager.Instance.userId, isOwner,false);
    }
    public void SetOwner(int id)
    {
        _onwerId = id;
        _startBtn.SetActive(_onwerId == NetworkManager.Instance.userId);

        if (_id2Info.TryGetValue(id, out RoomUserUI info))
            info.IsOwner = true;
    }
    public void InitRoom(List<RoomUserInfo> roomUsers)
    {
        roomUsers.ForEach(p =>
        {
            if (_id2Info.ContainsKey(p.UserId)) return;
            AddUser(p.UserId, _onwerId == p.UserId,p.Ready);
        });
    }
    public void AddUser(int userID, bool isOwner, bool ready)
    {
        RoomUserUI user = PoolManager.Instance.Pop(PoolType.RoomUserInfo, _userGroup) as RoomUserUI;
        user.SetUser(userID);
        user.IsOwner = isOwner;
        user.SetReady(ready);
        _id2Info.Add(userID, user);
    }
    public void LeaveUser(int userID)
    {
        if (_id2Info.ContainsKey(userID))
        {
            PoolManager.Instance.Push(_id2Info[userID]);
            _id2Info.Remove(userID);
        }
    }
    public void UpdateReady(int id, bool ready)
    {
        _id2Info[id].SetReady(ready);
    }

    public void Ready()
    {
        C_Ready p = new();
        p.RoomId = NetworkManager.Instance.roomId;
        p.UserId = NetworkManager.Instance.userId;
        NetworkManager.Instance.Send(p);
    }
}
