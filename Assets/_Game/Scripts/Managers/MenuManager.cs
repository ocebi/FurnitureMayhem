using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField, ReadOnly]
    private TMP_Text m_TargetText;
    [SerializeField, ReadOnly]
    private TMP_Text m_TimeText;
    [SerializeField, ReadOnly]
    private TMP_Text m_HighscoreText;
    [SerializeField, ReadOnly]
    private TMP_Text m_TargetHackText;
    [SerializeField, ReadOnly] 
    private Image m_TargetImage;
    [SerializeField, ReadOnly] 
    private Image m_ProgressBar;

    [SerializeField, ReadOnly] 
    private GameObject m_GameplayPanel;
    [SerializeField, ReadOnly] 
    private GameObject m_FinishPanel;

    [SerializeField, ReadOnly] 
    private Button m_RestartButton;

    [Button]
    private void SetRefs()
    {
        m_TargetText = transform.FindDeepChild<TMP_Text>("TargetText");
        m_TimeText = transform.FindDeepChild<TMP_Text>("TimeText");
        m_HighscoreText = transform.FindDeepChild<TMP_Text>("HighscoreText");
        m_TargetImage = transform.FindDeepChild<Image>("TargetImage");
        m_ProgressBar = transform.FindDeepChild<Image>("ProgressBar");
        m_GameplayPanel = transform.FindDeepChild<GameObject>("GameplayPanel");
        m_FinishPanel = transform.FindDeepChild<GameObject>("FinishPanel");
        m_RestartButton = transform.FindDeepChild<Button>("RestartButton");
        m_TargetHackText = transform.FindDeepChild<TMP_Text>("TargetHackText");
    }

    private void OnValidate()
    {
        SetRefs();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        m_RestartButton.onClick.AddListener(OnRestartClicked);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        m_RestartButton.onClick.RemoveAllListeners();
    }

    public override void Start()
    {
        base.Start();
        m_FinishPanel.SetActive(false);
        m_GameplayPanel.SetActive(true);
    }

    public void SetTarget(eCollectable collectableType, int currentAmount, int targetAmount)
    {
        var collectableData = GameConfig.Instance.CollectableDataDict[collectableType];
        m_TargetImage.sprite = collectableData.Sprite;
        m_TargetText.SetText($"{currentAmount}/{targetAmount}");
    }

    public void SetProgressBar(int current, int target)
    {
        m_ProgressBar.fillAmount = (float)current / target;
    }

    public void SetTargetHackText(int current)
    {
        m_TargetHackText.SetText($"{current}/{GameConfig.Instance.TargetHackAmount}");
    }

    public void SetFinishScreen()
    {
        m_TimeText.SetText($"It took {GameStateManager.Instance.GameTime} seconds");
        m_HighscoreText.SetText($"Your best: {PlayerPrefs.GetInt("Highscore", 10000)}");
        m_GameplayPanel.SetActive(false);
        m_FinishPanel.SetActive(true);
    }

    private void OnRestartClicked()
    {
        SceneManager.LoadScene(0);
    }
}
