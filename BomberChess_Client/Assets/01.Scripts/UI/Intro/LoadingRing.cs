using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoadingRing : UIObject
{
    private Image img;
    private void Awake() {
        img = GetComponent<Image>();
        OnOpen();
    }
    protected override void OnClose()
    {
    }

    protected override void OnOpen()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(img.DOFillAmount(1f,1.5f).SetEase(Ease.OutExpo));
        seq.Append(transform.DORotate(new Vector3(0,0,360f),0.4f,RotateMode.FastBeyond360));
        seq.Join(img.DOFillAmount(0,0.4f));
        seq.AppendInterval(1f);
        seq.SetLoops(-1);     
    }
}

