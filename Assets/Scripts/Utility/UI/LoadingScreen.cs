using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using TMPro;
using System;

public class LoadingScreen : MonoBehaviour
{
    [Header("Menu Items")]
    public GameObject loadingScreen;
    public GameObject mainMenu;
    [Header("Loading functionality")]
    public Slider loadingBar;
    public TextMeshProUGUI tipsText;
    public TextAsset tips;

    private void Awake()
    {
        SetRandomInformationTip();
    }

    public void LoadLevel(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        // Load method
        StartCoroutine(LoadAsync(levelToLoad));
    }

    IEnumerator LoadAsync(string levelToLoad)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOp.isDone)
        {
            float progressVal = Mathf.Clamp01(loadOp.progress / 0.9f);
            loadingBar.value = progressVal;
            yield return null;
            yield return new WaitForSeconds(2);
        }
    }

    void SetRandomInformationTip()
    {
        XmlDocument tipsDoc = new XmlDocument();
        tipsDoc.LoadXml(tips.text);

        XmlNodeList scriptNodes = tipsDoc.SelectNodes("/scripts/script");

        foreach (XmlNode sNode in scriptNodes)
        {
            XmlNodeList lineNodes = sNode.SelectNodes("line");

            List<String> tipsLines = new List<string>();

            foreach (XmlNode lNode in lineNodes)
            {
                tipsLines.Add(lNode.InnerText.Trim());
            }
            if (tipsLines.Count > 0)
            {
                int infoRand = UnityEngine.Random.Range(0, tipsLines.Count);
                tipsText.text = tipsLines[infoRand];
            }
        }
    }
}
