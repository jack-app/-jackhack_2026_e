using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public int selectedstage=1;
    public GameObject SelectPanel;
    // Start is called before the first frame update
    void Start()
    {
        SelectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        SelectPanel.SetActive(true);
    }

    public void OnClickStage1()
    {
        selectedstage = 1;
    }
    public void OnClickStage2()
    {
        selectedstage = 2;
    }
    public void OnClickStage3()
    {
        selectedstage = 3;
    }
    public void OnClickStage4()
    {
        selectedstage = 4;
    }
    public void OnClickStage5()
    {
        selectedstage = 5;
    }
    public void OnClickStage6()
    {
        selectedstage = 6;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Stage" + selectedstage);
    }   
}
