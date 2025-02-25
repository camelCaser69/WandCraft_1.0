// ==================== MainSceneSetup.cs ====================
// Sets up the main scene with camera, lighting, UI elements for HP display,
// allows scene resolution adjustments, and supports customizable placeholder text
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
    public Text enemyHPText;

    [Header("Placeholder Settings")]
    public bool useCustomPlaceholderText = false;
    public string customPlaceholderText = "HP: ???";

    private void Awake()
    {
        SetupCamera();
        SetupLighting();
        SetupUI();
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
        playerHPText = CreateHPText("Player HP", new Vector2(100, -50), TextAnchor.UpperLeft);
        enemyHPText = CreateHPText("Enemy HP", new Vector2(-100, -50), TextAnchor.UpperRight);

        ApplyPlaceholderText();
    }

    private void ApplyPlaceholderText()
    {
        if (useCustomPlaceholderText)
        {
            playerHPText.text = customPlaceholderText;
            enemyHPText.text = customPlaceholderText;
        }
        else
        {
            playerHPText.text = "Player HP: ???";
            enemyHPText.text = "Enemy HP: ???";
        }
    }

    private Text CreateHPText(string label, Vector2 offset, TextAnchor anchor)
    {
        GameObject textObj = new GameObject(label);
        textObj.transform.SetParent(uiCanvas.transform, false);
        Text hpText = textObj.AddComponent<Text>();
        hpText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        hpText.fontSize = 24;
        hpText.color = Color.white;
        hpText.alignment = anchor;
        RectTransform rect = hpText.GetComponent<RectTransform>();
        rect.anchorMin = anchor == TextAnchor.UpperLeft ? new Vector2(0, 1) : new Vector2(1, 1);
        rect.anchorMax = rect.anchorMin;
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = offset;
        rect.sizeDelta = new Vector2(160, 30);
        return hpText;
    }
}