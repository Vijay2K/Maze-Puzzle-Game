using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DisplayGameCompletionPanel : MonoBehaviour
{
    [SerializeField] private GameObject gameCompletionPanel;
    [SerializeField] private TMP_Text levelTxt;

    private int levelCount = 1;

    private void Start()
    {        
        gameCompletionPanel.SetActive(false);
        levelTxt.text = "Level " + PlayerPrefs.GetInt("levelcount", levelCount);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void DisplayPanel()
    {
        gameCompletionPanel.SetActive(true);
    }

    public void Next()
    {
        levelCount += PlayerPrefs.GetInt("levelcount", levelCount);
        PlayerPrefs.SetInt("levelcount", levelCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
