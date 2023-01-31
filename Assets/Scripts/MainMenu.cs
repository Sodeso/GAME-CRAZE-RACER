using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using System.Data;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MainMenu : MonoBehaviour
{
    GameObject panelInputUsername;
    GameObject panelLeaders;
    private void Awake()
    {
        panelLeaders = GameObject.Find("PanelLeaders");
        panelInputUsername = GameObject.Find("PanelInputUsername");
        CreateLeaders();
        LoadLeaders();
        panelLeaders.SetActive(false);
        panelInputUsername.SetActive(false);
    }

    public void ClickNewGame()
    {
        if (!panelInputUsername.activeSelf)
        {
            panelInputUsername.SetActive(true);
        }
        else
        {
            try
            {
                panelInputUsername.transform.localPosition = new Vector3(0, 0, 0);
                panelInputUsername.SetActive(false);
            }
            catch (Exception buggPanel)
            {
                Debug.LogException(buggPanel, this);
            }
        }
    }
    public void ClickEnterInputUsername()
    {
        TMP_InputField inputFieldUsername = TMP_InputField.FindObjectOfType<TMP_InputField>();

        inputFieldUsername.text = inputFieldUsername.text.Trim();
        Debug.Log(inputFieldUsername.text);
        if (inputFieldUsername.text != "")
        {
            PlayerName.name = inputFieldUsername.text;
            if (PlayerPrefs.HasKey($"{inputFieldUsername.text}[MaxScore]"))
            {
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
            }
            else
            {
                PlayerPrefs.SetInt($"{inputFieldUsername.text}[MaxScore]", 0);
                PlayerPrefs.SetInt($"{inputFieldUsername.text}[LastScore]", 0);
                PlayerPrefs.SetInt($"{inputFieldUsername.text}[Gold]", 0);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
            }
        }
        
    }
    public void ClickLeaders()
    {
        if (!panelLeaders.activeSelf)
        {
            panelLeaders.SetActive(true);
        }
        else
        {
            try
            {
                panelLeaders.transform.localPosition = new Vector3(0, 0, 0);
                panelLeaders.SetActive(false);
            }
            catch (Exception buggPanel)
            {
                Debug.LogException(buggPanel, this);
            }
        }
    }
    public void ClickClosePanel()
    {
        GameObject buttonClose = GameObject.Find("ButtonClose");
        buttonClose.transform.parent.gameObject.SetActive(false);
    }

    void CreateLeaders()
    {
        if (PlayerPrefs.HasKey("Top1[name]") && PlayerPrefs.HasKey("Top1[score]"))
        {
            return;
        }
        else
        {
            for (int i = 1; i <= 10; i++)
            {
                PlayerPrefs.SetString($"Top{i}[name]", "none"); PlayerPrefs.SetInt($"Top{i}[score]", 0);
                PlayerPrefs.Save();
            }
        }

    }
    void LoadLeaders()
    {
        for(int i = 1; i <= 10; i++)
        {
            TextMeshProUGUI textName = panelLeaders.transform.Find($"Top {i}").gameObject.transform.Find("TextName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI textScore = panelLeaders.transform.Find($"Top {i}").gameObject.transform.Find("TextScore").GetComponent<TextMeshProUGUI>();
            textName.text = PlayerPrefs.GetString($"Top{i}[name]");
            textScore.text = PlayerPrefs.GetInt($"Top{i}[score]").ToString();
        }
    }
}
