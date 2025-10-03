using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action<eStateGame> StateChangedAction = delegate { };

    public enum eLevelMode
    {
        TIMER,
        MOVES
    }

    public enum eStateGame
    {
        SETUP,
        MAIN_MENU,
        GAME_STARTED,
        PAUSE,
        GAME_OVER,
    }

    private eStateGame m_state;
    public eStateGame State
    {
        get { return m_state; }
        private set
        {
            m_state = value;

            StateChangedAction(m_state);
        }
    }


    [SerializeField] GameSettings m_gameSettings;


    private BoardController m_boardController;

    [SerializeField] UIMainManager m_uiMenu;

    private LevelCondition m_levelCondition;
    private eLevelMode m_currentMode;
    [SerializeField] PoolingController m_pooling;


    [Header("Config Skin")]
    [SerializeField] eTypeSkinItem m_typeSkinItem;
    public eTypeSkinItem TypeSkin => m_typeSkinItem;


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!m_uiMenu)
        {
            m_uiMenu = FindObjectOfType<UIMainManager>();
            if (!m_uiMenu)
                Debug.LogError("UIMainManager not found in Scene!!!!");
        }

        if (!m_gameSettings)
        {
            m_gameSettings = Resources.Load<GameSettings>(Constants.GAME_SETTINGS_PATH);
            if (!m_gameSettings)
                Debug.LogError("GameSettings not found in Rescources!!!!");
        }
    }
#endif

    public override void Awake()
    {
        base.Awake();
        State = eStateGame.SETUP;

        // m_gameSettings = Resources.Load<GameSettings>(Constants.GAME_SETTINGS_PATH);

        // m_uiMenu = FindObjectOfType<UIMainManager>();
        m_uiMenu.Setup(this);
    }

    void Start()
    {
        State = eStateGame.MAIN_MENU;
    }

    // Update is called once per frame
    void Update()
    {
        m_boardController?.UpdateBoard();
    }


    internal void SetState(eStateGame state)
    {
        State = state;

        if (State == eStateGame.PAUSE)
        {
            DOTween.PauseAll();
        }
        else
        {
            DOTween.PlayAll();
        }
    }

    public void LoadLevel(eLevelMode mode)
    {
        m_currentMode = mode;
        if (!m_boardController)
            m_boardController = new GameObject("BoardController").AddComponent<BoardController>();
        m_boardController.StartGame(this, m_gameSettings);

        if (mode == eLevelMode.MOVES)
        {
            m_levelCondition = this.gameObject.AddComponent<LevelMoves>();
            m_levelCondition.Setup(m_gameSettings.LevelMoves, m_uiMenu.GetLevelConditionView(), m_boardController);
        }
        else if (mode == eLevelMode.TIMER)
        {
            m_levelCondition = this.gameObject.AddComponent<LevelTime>();
            m_levelCondition.Setup(m_gameSettings.LevelTime, m_uiMenu.GetLevelConditionView(), this);
        }

        m_levelCondition.ConditionCompleteEvent += GameOver;

        State = eStateGame.GAME_STARTED;
    }

    public void GameOver()
    {
        StartCoroutine(WaitBoardController());
    }

    internal void ClearLevel()
    {
        if (m_boardController)
        {
            m_boardController.Clear();
            // Destroy(m_boardController.gameObject);
            // m_boardController = null;
        }
    }

    private IEnumerator WaitBoardController()
    {
        while (m_boardController.IsBusy)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);

        State = eStateGame.GAME_OVER;

        if (m_levelCondition != null)
        {
            m_levelCondition.ConditionCompleteEvent -= GameOver;

            Destroy(m_levelCondition);
            m_levelCondition = null;
        }
    }

    #region Pooling
    public GameObject GetItemInPooling(Sprite sprite)
    {
        GameObject go = m_pooling.GetObject(PoolKey.KEY_ITEM);
        go.GetComponent<SpriteRenderer>().sprite = sprite;
        go.transform.localScale = Vector3.one;
        return go;
    }
    public GameObject GetCellInPooling(Vector3 position, Transform parent = null)
    {
        GameObject go = m_pooling.GetObject(PoolKey.KEY_CELL, position, parent);
        go.transform.localScale = Vector3.one;
        return go;
    }
    public void DisableGOPooling(GameObject go)
    {
        m_pooling.DisableObjPooling(go);
    }
    #endregion

    #region Skin
    public void ChangeSkinItem()
    {
        if (m_typeSkinItem == eTypeSkinItem.NORMAL)
            m_typeSkinItem = eTypeSkinItem.FISH;
        else m_typeSkinItem = eTypeSkinItem.NORMAL;
    }
    #endregion

    #region Restart
    public void Restart()
    {
        m_boardController.RestartCells();

        if (m_currentMode == eLevelMode.MOVES)
        {
            if (m_levelCondition == null)
            {
                m_levelCondition = this.gameObject.AddComponent<LevelMoves>();
                m_levelCondition.ConditionCompleteEvent += GameOver;
            }
            m_levelCondition.Setup(m_gameSettings.LevelMoves, m_uiMenu.GetLevelConditionView(), m_boardController);
        }
        else if (m_currentMode == eLevelMode.TIMER)
        {
            if (m_levelCondition == null)
            {
                m_levelCondition = this.gameObject.AddComponent<LevelTime>();
                m_levelCondition.ConditionCompleteEvent += GameOver;
            }

            m_levelCondition.Setup(m_gameSettings.LevelTime, m_uiMenu.GetLevelConditionView(), this);//
        }


        State = eStateGame.GAME_STARTED;
        m_boardController.SetRestartParam();
    }
    #endregion
}
