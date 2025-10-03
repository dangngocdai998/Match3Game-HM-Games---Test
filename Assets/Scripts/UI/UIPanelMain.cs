﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelMain : MonoBehaviour, IMenu
{
    [SerializeField] private Button btnTimer;

    [SerializeField] private Button btnMoves;
    [SerializeField] private Button btnChangeSkin;
    [SerializeField] private Image iconSkin;

    private UIMainManager m_mngr;

    private void Awake()
    {
        btnMoves.onClick.AddListener(OnClickMoves);
        btnTimer.onClick.AddListener(OnClickTimer);
        btnChangeSkin.onClick.AddListener(OnChangeSkin);
    }

    private void OnDestroy()
    {
        if (btnMoves) btnMoves.onClick.RemoveAllListeners();
        if (btnTimer) btnTimer.onClick.RemoveAllListeners();
        if (btnChangeSkin) btnChangeSkin.onClick.RemoveAllListeners();
    }

    public void Setup(UIMainManager mngr)
    {
        m_mngr = mngr;
    }

    private void OnClickTimer()
    {
        m_mngr.LoadLevelTimer();
    }

    private void OnClickMoves()
    {
        m_mngr.LoadLevelMoves();
    }
    private void OnChangeSkin()
    {
        GameManager.Instance.ChangeSkinItem();
        DislayIconSkin();
    }

    void DislayIconSkin()
    {
        iconSkin.sprite = ImageManager.Instance.GetSpriteItemNormal(NormalItem.eNormalType.TYPE_ONE);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        DislayIconSkin();
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
