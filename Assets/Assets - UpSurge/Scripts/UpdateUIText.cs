using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UpdateUIText : MonoBehaviour
{
    public TMP_Text Text;
    public float ScrambleSpeed = 0.5f;
    public Transform Icon;

    [Header("Progress Update")]
    public Image ProgressBar;
    public float Value = 0.0f;
    public float MinValue = 0.0f;
    public float MaxValue = 1.0f;

    [Header("SFx")]
    public bool SoundAtUpdate = true;

    [Header("SFx Type")]
    public SfxPresets SfxPreset;

    [Header("VFx Type")]
    public VfxTypes VfxPreset;

    [Header("VFx")]
    public bool VfxAtUpdate = true;
    public float VfxSpeed = 0.5f;

    IEnumerator _Updater;

    void Start()
    {
        _Updater = null;
        if (MinValue < 0) MinValue = 0;
        if (MaxValue <= MinValue) MaxValue = MinValue+1;
    }

    public void UpdateText(string UpdateText)
    {
        if (_Updater != null) StopCoroutine(_Updater);

        Value = float.Parse(UpdateText);
        if (Value > MaxValue) MaxValue = Value;

        _Updater = Updater();
        StartCoroutine(_Updater);
    }

    IEnumerator Updater()
    {
        Text.text = "";
        Text.DOText(Value.ToString(), ScrambleSpeed,true,ScrambleMode.Numerals);

        if (ProgressBar!=null)
        {
            float ScaleNow = Value / MaxValue;
            ProgressBar.DOFillAmount(ScaleNow, ScrambleSpeed * 2).SetEase(Ease.OutBounce);
        }

        yield return new WaitForSeconds(ScrambleSpeed);

        if (VfxAtUpdate && Icon != null)
        {
            if (VfxPreset == VfxTypes.Punch)
            {
                Icon.DOScale(Vector3.one, VfxSpeed).From(Vector3.zero).SetEase(Ease.OutElastic);
            }
            if (VfxPreset == VfxTypes.Bounce)
            {
                Icon.DOScale(Vector3.one, VfxSpeed).From(Vector3.zero).SetEase(Ease.OutBounce);
            }
        }

        if (SoundAtUpdate) MasterController.get.PlayPresetSfx((int)SfxPreset);
    }

    public void UpdateRandom()
    {
        Value = UnityEngine.Random.Range((int)MinValue,(int)MaxValue);
        if (Value > MaxValue) MaxValue = Value;

        _Updater = Updater();
        StartCoroutine(_Updater);
    }

    public enum VfxTypes
    {
        Punch,
        Bounce
    }
}
