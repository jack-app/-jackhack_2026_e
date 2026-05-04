using UnityEngine;
using UnityEngine.SceneManagement; // シーンのロードに必要

public class ReloadScene : MonoBehaviour
{
    void Update()
    {
        // 左Shift または 右Shift を押している状態で、Rキーが押された瞬間を判定
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
    }

    private void ReloadCurrentScene()
    {
        // 現在アクティブなシーンの名前を取得して、同じシーンを再読み込みする
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}