using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Net;  // 요거 사용

public class CardDataImporter : EditorWindow
{
    private static readonly string URL = "https://docs.google.com/spreadsheets/d/1udhV6hfr6PdATn5bOkEWv97gwowDXrTfLWNlVj9-hsk/export?format=csv&range=A4:G14&gid=0";
    private static readonly string SAVE_FOLDER = "Assets/Cards";

    [MenuItem("Tools/Import Card Data From Google Sheets")]
    public static void ImportCardData()
    {
        Debug.Log("Downloading TSV...");
        string tsvText = DownloadTSV(URL);
        if (string.IsNullOrEmpty(tsvText))
        {
            Debug.LogError("다운로드 실패!");
            return;
        }
        CreateAssetsFromTSV(tsvText);
    }

    private static string DownloadTSV(string url)
    {
        using (WebClient client = new WebClient())
        {
            try
            {
                return client.DownloadString(url);
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
                return null;
            }
        }
    }

    private static void CreateAssetsFromTSV(string tsvText)
    {
        if (!AssetDatabase.IsValidFolder(SAVE_FOLDER))
        {
            AssetDatabase.CreateFolder("Assets", "Cards");
        }

        string[] lines = tsvText.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] fields = lines[i].Trim().Split(',');
            if (fields.Length >= 7)
            {
                int ID = int.Parse(fields[0]);
                string name = fields[2];
                int type = int.Parse(fields[3]);
                int cost = int.Parse(fields[4]);
                int value = int.Parse(fields[5]);
                string tooltip = fields[6];

                CardData card = ScriptableObject.CreateInstance<CardData>();
                card.ID = ID;
                card.Name = name;
                card.Type = type;
                card.Cost = cost;
                card.Value = value;
                card.Tooltip = tooltip;

                string safeName = name.Replace(" ", "_");
                string assetPath = $"{SAVE_FOLDER}/{safeName}.asset";

                if (File.Exists(assetPath))
                {
                    AssetDatabase.DeleteAsset(assetPath);
                }

                AssetDatabase.CreateAsset(card, assetPath);
                Debug.Log($"Created: {assetPath}");
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Import complete!");
    }
}
