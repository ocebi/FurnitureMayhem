using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField, ReadOnly]
    private TMP_Text m_TargetText;
    [SerializeField, ReadOnly] 
    private Image m_TargetImage;

    [Button]
    private void SetRefs()
    {
        m_TargetText = transform.FindDeepChild<TMP_Text>("TargetText");
        m_TargetImage = transform.FindDeepChild<Image>("TargetImage");
    }

    private void OnValidate()
    {
        SetRefs();
    }

    public void SetTarget(eCollectable collectableType, int currentAmount, int targetAmount)
    {
        var collectableData = GameConfig.Instance.CollectableDataDict[collectableType];
        m_TargetImage.sprite = collectableData.Sprite;
        m_TargetText.SetText($"{currentAmount}/{targetAmount}");
    }
}
