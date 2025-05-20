using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class OpeningCutsceneDialogue : MonoBehaviour
{
    public TextAsset sob_openSC;
    public TextMeshProUGUI dialogueText;

    private void Start()
    {
        StartCoroutine(PlayDialogue());
    }

    public IEnumerator PlayDialogue()
    {
        XmlDocument sob_script = new XmlDocument();
        sob_script.LoadXml(sob_openSC.text);

        XmlNodeList scriptNodes = sob_script.SelectNodes("/scripts/script");

        foreach (XmlNode sNode in scriptNodes)
        {
            XmlNodeList lineNodes = sNode.SelectNodes("line");

            foreach (XmlNode lNode in lineNodes)
            {
                string linesText = lNode.InnerText.Trim();
                dialogueText.text = linesText;
                yield return new WaitForSeconds(5);
            }
        }
    }
}