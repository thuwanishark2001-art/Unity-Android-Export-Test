using UnityEngine;

public class ExportTest : MonoBehaviour
{
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        Application.targetFrameRate = 60;
    }

    void OnGUI()
    {
        GUIStyle title = new GUIStyle(GUI.skin.label);
        title.fontSize = Mathf.RoundToInt(Screen.width * 0.055f);
        title.alignment = TextAnchor.MiddleCenter;
        title.normal.textColor = Color.white;

        GUIStyle body = new GUIStyle(GUI.skin.label);
        body.fontSize = Mathf.RoundToInt(Screen.width * 0.035f);
        body.alignment = TextAnchor.MiddleCenter;
        body.normal.textColor = Color.white;

        GUI.Label(new Rect(0, Screen.height * 0.28f, Screen.width, 80),
            "UNITY ANDROID EXPORT TEST", title);

        GUI.Label(new Rect(0, Screen.height * 0.40f, Screen.width, 60),
            "If you can install this APK, the cloud build pipeline works.", body);

        GUI.Label(new Rect(0, Screen.height * 0.50f, Screen.width, 60),
            "Running: " + (Time.time - startTime).ToString("0.0") + " seconds", body);
    }
}
