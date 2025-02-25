// ==================== MainSceneSetup.cs ====================
// Sets up the main scene with camera, lighting, and UI elements for HP display
using UnityEngine;
using UnityEngine.UI;

public class MainSceneSetup : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas uiCanvas;
    public Text playerHPText;
    public Text enemyHPText;

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
            canvasObj.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasObj.AddComponent<GraphicRaycaster>();
        }
        playerHPText = CreateHPText("Player HP", new Vector2(100, -50), TextAnchor.UpperLeft);
        enemyHPText = CreateHPText("Enemy HP", new Vector2(-100, -50), TextAnchor.UpperRight);
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