using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField] private string sceneName; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FadeController fadeController = FindObjectOfType<FadeController>();
            if (fadeController != null)
            {
                fadeController.FadeOut();
            }
            LoadSceneWithDelay();
        }
    }

    void LoadSceneWithDelay()
    {
        Invoke("LoadScene", 0.5f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
