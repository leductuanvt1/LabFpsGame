using UnityEngine;
using TMPro; // cần để dùng TMP_Text

public class HUD : MonoBehaviour
{
    public static HUD Instance;
    public TMP_Text healthText;       // đổi từ Text -> TMP_Text
    public TMP_Text killCountText;

    private int killCount = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateHealth(float current, float max)
    {
        healthText.text = $"Health: {current}/{max}";
    }

    public void AddKill()
    {
        killCount++;
        killCountText.text = $"Enemies killed: {killCount}";
    }
}
