// ==================== MainSceneSetup.cs ====================
// Updates: Mana now displays as whole numbers (floored) to avoid rendering issues.

using UnityEngine;
using UnityEngine.UI;

public class MainSceneSetup : MonoBehaviour
{
    [Header("Scene Settings")]
    public Camera mainCamera;
    public Vector2 sceneResolution = new Vector2(1920, 1080);

    [Header("UI Settings")]
    public Canvas uiCanvas;
    public Text playerHPText;
    public Text playerManaText;
    public Text enemyHPText;
    public Text enemyManaText;

    [Header("Placeholder Settings")]
    public bool useCustomPlaceholderText = false;
    public string customPlaceholderText = "HP: ???\nMana: ???";

    [Header("Wizard Prefabs")]
    public GameObject playerWizardPrefab;
    public GameObject enemyWizardPrefab;

    [Header("Spawn Points")]
    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    private WizardController playerWizardController;
    private WizardController enemyWizardController;

    private void Awake()
    {
        SetupCamera();
        SetupLighting();
        SetupUI();
    }

    private void Start()
    {
        InitializeWizards();
        UpdateUI(); // Initial UI update
    }

    private void SetupCamera()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                GameObject camObj = new GameObject("Main Camera");
                mainCamera = camObj.AddComponent<Camera>();
                mainCamera.orthographic = true;
                mainCamera.transform.position = new Vector3(0, 0, -10);
            }
        }
        mainCamera.clearFlags = CameraClearFlags.SolidColor;
        mainCamera.backgroundColor = Color.gray;
        AdjustSceneResolution();
    }

    private void AdjustSceneResolution()
    {
        if (sceneResolution.x > 0 && sceneResolution.y > 0)
        {
            Screen.SetResolution((int)sceneResolution.x, (int)sceneResolution.y, false);
            Debug.Log($"Scene resolution set to {sceneResolution.x}x{sceneResolution.y}");
        }
        else
        {
            Debug.LogWarning("Invalid scene resolution specified. Resolution must be greater than zero.");
        }
    }

    private void SetupLighting()
    {
        if (UnityEngine.Object.FindFirstObjectByType<Light>() == null)
        {
            GameObject lightObj = new GameObject("Global Light");
            Light globalLight = lightObj.AddComponent<Light>();
            globalLight.type = LightType.Directional;
            globalLight.intensity = 0.8f;
            globalLight.transform.rotation = Quaternion.Euler(50, -30, 0);
        }
    }

    private void SetupUI()
    {
        if (uiCanvas == null)
        {
            GameObject canvasObj = new GameObject("UI Canvas");
            uiCanvas = canvasObj.AddComponent<Canvas>();
            uiCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = sceneResolution;
            canvasObj.AddComponent<GraphicRaycaster>();
        }
        playerHPText = CreateUIText("Player HP", new Vector2(100, -50), TextAnchor.UpperLeft);
        playerManaText = CreateUIText("Player Mana", new Vector2(100, -80), TextAnchor.UpperLeft);
        enemyHPText = CreateUIText("Enemy HP", new Vector2(-100, -50), TextAnchor.UpperRight);
        enemyManaText = CreateUIText("Enemy Mana", new Vector2(-100, -80), TextAnchor.UpperRight);

        ApplyPlaceholderText();
    }

    private void ApplyPlaceholderText()
    {
        if (useCustomPlaceholderText)
        {
            playerHPText.text = customPlaceholderText;
            playerManaText.text = customPlaceholderText;
            enemyHPText.text = customPlaceholderText;
            enemyManaText.text = customPlaceholderText;
        }
        else
        {
            playerHPText.text = "Player HP: ???";
            playerManaText.text = "Mana: ???";
            enemyHPText.text = "Enemy HP: ???";
            enemyManaText.text = "Mana: ???";
        }
    }

    private Text CreateUIText(string label, Vector2 offset, TextAnchor anchor)
    {
        GameObject textObj = new GameObject(label);
        textObj.transform.SetParent(uiCanvas.transform, false);
        Text uiText = textObj.AddComponent<Text>();
        uiText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        uiText.fontSize = 24;
        uiText.color = Color.white;
        uiText.alignment = anchor;
        RectTransform rect = uiText.GetComponent<RectTransform>();
        rect.anchorMin = anchor == TextAnchor.UpperLeft ? new Vector2(0, 1) : new Vector2(1, 1);
        rect.anchorMax = rect.anchorMin;
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = offset;
        rect.sizeDelta = new Vector2(180, 30);
        return uiText;
    }

    // ================== WIZARD SPAWNING & UI UPDATES ==================
    private void InitializeWizards()
    {
        if (playerWizardPrefab == null || enemyWizardPrefab == null)
        {
            Debug.LogError("Wizard prefabs are not assigned in MainSceneSetup.");
            return;
        }

        // Instantiate Player
        GameObject playerWizard = Instantiate(playerWizardPrefab, playerSpawnPoint.position, Quaternion.identity);
        playerWizard.name = "PlayerWizard";
        playerWizardController = playerWizard.GetComponent<WizardController>();

        // Instantiate Enemy
        GameObject enemyWizard = Instantiate(enemyWizardPrefab, enemySpawnPoint.position, Quaternion.identity);
        enemyWizard.name = "EnemyWizard";
        enemyWizardController = enemyWizard.GetComponent<WizardController>();

        // Start battle
        playerWizardController.StartBattle();
        enemyWizardController.StartBattle();
        Debug.Log("Wizards have been spawned and battle started.");
    }

    private void Update()
    {
        if (playerWizardController != null && enemyWizardController != null)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (playerWizardController != null)
        {
            playerHPText.text = $"Player HP: {playerWizardController.maxHealth}";
            playerManaText.text = $"Mana: {Mathf.Floor(playerWizardController.GetCurrentMana())}/{playerWizardController.GetMaxMana()}";
        }

        if (enemyWizardController != null)
        {
            enemyHPText.text = $"Enemy HP: {enemyWizardController.maxHealth}";
            enemyManaText.text = $"Mana: {Mathf.Floor(enemyWizardController.GetCurrentMana())}/{enemyWizardController.GetMaxMana()}";
        }
    }
}
