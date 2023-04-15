using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIFx : MonoBehaviour, IPointerClickHandler
{
    [Header("SFx Type")]
    public SfxPresets SfxPreset;

    [Header("SFx")]
    public bool SoundAtClick = true;

    [Header("VFx Type")]
    public VfxTypes VfxPreset;

    [Header("VFx")]
    public bool VfxAtClick = true;
    public float VfxSpeed = 0.5f;

    Button _Button;

    void Start()
    {
        _Button = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (VfxAtClick)
        {
            if(_Button!=null)
            {
                if (!_Button.interactable) return;
            }

            if (VfxPreset == VfxTypes.Punch)
            {
                transform.DOScale(Vector3.one, VfxSpeed).From(Vector3.zero).SetEase(Ease.OutElastic);
            }
            if (VfxPreset == VfxTypes.Bounce)
            {
                transform.DOScale(Vector3.one, VfxSpeed).From(Vector3.zero).SetEase(Ease.OutBounce);
            }
        }

        if (SoundAtClick) MasterController.get.PlayPresetSfx((int)SfxPreset);
        //throw new System.NotImplementedException();
    }

    public enum VfxTypes
    {
        Punch,
        Bounce
    }
}
