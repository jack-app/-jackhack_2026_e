using UnityEngine;
using TMPro; // TextMeshProを使用するために必要な名前空間

public class ItemCollector : MonoBehaviour
{
    [Header("Collection Stats")]
    public int melonCount = 0; // メロンの獲得数

    [Header("UI Reference")]
    // Inspector上で、Hierarchy内の"Melontxt"をここにドラッグ＆ドロップしてください
    public TextMeshProUGUI melonText; 

    void Start()
    {
        // UIの表示を初期化
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したオブジェクトのタグが"Melon"かどうかをチェック
        if (collision.gameObject.CompareTag("Melon"))
        {
            // 1. メロンのオブジェクトを破棄
            Destroy(collision.gameObject);

            // 2. カウントを加算
            melonCount++;

            // 3. UIの表示を更新
            UpdateUI();

            // コンソールにログを出力
            Debug.Log("Melon collected! Current count: " + melonCount);
        }
    }

    void UpdateUI()
    {
        if (melonText != null)
        {
            // 現在の獲得数をテキストに反映
            melonText.text = "Melons: " + melonCount.ToString();
        }
    }
}