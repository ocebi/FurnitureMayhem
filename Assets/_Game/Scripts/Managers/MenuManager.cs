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
    private TMP_Text m_CooldownText;
    [SerializeField, ReadOnly] 
    private Image m_TargetImage;
    [SerializeField, ReadOnly] 
    private Image m_ProgressBar;

    [SerializeField, ReadOnly] 
    private GameObject m_StartPanel;
    [SerializeField, ReadOnly] 
    private GameObject m_GameplayPanel;
    [SerializeField, ReadOnly] 
    private GameObject m_FinishPanel;

    [SerializeField, ReadOnly] 
    private Button m_RestartButton;
    [SerializeField, ReadOnly] 
    private Button m_StartButton;
    [SerializeField, ReadOnly] 
    private Button m_QuitButton;
    [SerializeField, ReadOnly] 
    private Button m_AboutButton;

    [Button]
    private void SetRefs()
    {
        m_StartPanel = transform.FindDeepChild<GameObject>("StartPanel");
        m_TargetText = transform.FindDeepChild<TMP_Text>("TargetText");
        m_TimeText = transform.FindDeepChild<TMP_Text>("TimeText");
        m_HighscoreText = transform.FindDeepChild<TMP_Text>("HighscoreText");
        m_TargetImage = transform.FindDeepChild<Image>("TargetImage");
        m_ProgressBar = transform.FindDeepChild<Image>("ProgressBar");
        m_GameplayPanel = transform.FindDeepChild<GameObject>("GameplayPanel");
        m_FinishPanel = transform.FindDeepChild<GameObject>("FinishPanel");
        m_RestartButton = transform.FindDeepChild<Button>("RestartButton");
        m_StartButton = transform.FindDeepChild<Button>("StartButton");
        m_QuitButton = transform.FindDeepChild<Button>("QuitButton");
        m_AboutButton = transform.FindDeepChild<Button>("AboutButton");
        m_TargetHackText = transform.FindDeepChild<TMP_Text>("TargetHackText");
        m_CooldownText = transform.FindDeepChild<TMP_Text>("CooldownText");
        
        
    }

    private void OnValidate()
    {
        SetRefs();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        m_RestartButton.onClick.AddListener(OnRestartClicked);
        m_StartButton.onClick.AddListener(OnStartClicked);
        m_QuitButton.onClick.AddListener(OnQuitClicked);
        m_AboutButton.onClick.AddListener(OnAboutClicked);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        m_RestartButton.onClick.RemoveAllListeners();
        m_StartButton.onClick.RemoveAllListeners();
        m_QuitButton.onClick.RemoveAllListeners();
        m_AboutButton.onClick.RemoveAllListeners();
    }

    public override void Start()
    {
        base.Start();
        m_StartPanel.SetActive(true);
        m_GameplayPanel.SetActive(false);
        m_FinishPanel.SetActive(false);
        m_CooldownText.gameObject.SetActive(false);
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

    public void SetCooldownText(float cooldown)
    {
        CancelInvoke(nameof(DisableCooldownText));
        m_CooldownText.SetText($"Cooldown ({cooldown:0.00} sec)");
        m_CooldownText.gameObject.SetActive(true);
        Invoke(nameof(DisableCooldownText), 1.5f);
    }

    private void DisableCooldownText()
    {
        m_CooldownText.gameObject.SetActive(false);
    }

    private void OnRestartClicked()
    {
        SceneManager.LoadScene(0);
    }

    private void OnStartClicked()
    {
        m_StartPanel.SetActive(false);
        m_GameplayPanel.SetActive(true);
        m_FinishPanel.SetActive(false);
        GameStateManager.Instance.StartGame();
    }

    private void OnAboutClicked()
    {
        
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}
