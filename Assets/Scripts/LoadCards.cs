using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadCards : MonoBehaviour
{
    public readonly string URL = "https://docs.google.com/spreadsheets/d/1udhV6hfr6PdATn5bOkEWv97gwowDXrTfLWNlVj9-hsk";
    public readonly string range = "A4:G14";
    public readonly long sheetID = 0;

    public List<CardData> cards = new List<CardData>();
    public CardDatabase cardDatabase;
    public static string GetTSVadress(string address, string range, long sheetID)
    {
        return $"{address}/export?format=csv&range={range}&gid={sheetID}";
    }
    private void Start()
    {
        StartCoroutine(LoadData());
    }

    private IEnumerator LoadData()
    {
        UnityWebRequest www = UnityWebRequest.Get(GetTSVadress(URL, range, sheetID));
        yield return www.SendWebRequest();
        ParseTSV(www.downloadHandler.text);

    }
    private void ParseTSV(string tsvtext)
    {
        string[] lines = tsvtext.Split(',');
        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] fields = lines[i].Trim().Split('\n');

            if (fields.Length >= 7)
            {
                int ID = int.Parse(fields[0]);
                string name = fields[2];
                int type = int.Parse(fields[3]);
                int cost = int.Parse(fields[4]);
                int value = int.Parse(fields[5]);
                string tooltip = fields[6];

                CardData cardData = ScriptableObject.CreateInstance<CardData>();
                cardData.Init(ID, name, type, cost, value, tooltip);
                cards.Add(cardData);
                Debug.Log(cardData);
            }
        }
        cardDatabase = ScriptableObject.CreateInstance<CardDatabase>();
        cardDatabase.Cards = cards;
    }
}
