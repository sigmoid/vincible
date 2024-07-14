using Dan.Main;
using Dan.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using Unity.VisualScripting;

public class ScoreItem
{
    public string Score;
    public string UserName;
}

public class ScoreDB : MonoBehaviour
{
    public bool LoadScores;

    public TMP_Text[] Texts;

    private const string SERVER_URL = "https://www.ludiorum.com/";

	// Start is called before the first frame update
	void Start()
	{
		if (!PlayerPrefs.HasKey("ClientId"))
		{
			PlayerPrefs.SetString("ClientId", System.Guid.NewGuid().ToString());
		}

		if (LoadScores)
			StartCoroutine(QueryScores());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendScore(string name, int score, System.Action<bool> callback)
    {
        StartCoroutine(UploadScore(name, score, callback));
    }

    public IEnumerator UploadScore(string name, int score, System.Action<bool> callback)
    {
        Debug.Log("Start Upload");
        WWWForm form = new WWWForm();
        form.AddField("v", name + "~" + score);
        bool success = false;
        using (UnityWebRequest req = UnityWebRequest.Post(SERVER_URL + "addScore", form)) 
        {
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                success = false;
                Debug.LogError("Failed to upload score!");
            }
            else
            {
                success = true;
                Debug.Log("Score Uploaded");
            }            
        }

        callback(success);
	}

    private IEnumerator QueryScores()
    {
		using (UnityWebRequest req = UnityWebRequest.Get(SERVER_URL + "getScores"))
		{
			yield return req.SendWebRequest();

			if (req.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Failed to get score!");
			}

            switch (req.result) {
				case UnityWebRequest.Result.ConnectionError:
				case UnityWebRequest.Result.DataProcessingError:
					Debug.LogError("Error: " + req.error);
					break;
				case UnityWebRequest.Result.ProtocolError:
					Debug.LogError("HTTP Error: " + req.error);
					break;
				case UnityWebRequest.Result.Success:
                    string data = req.downloadHandler.text;
                    data = data.Remove(0, 1);
                    data = data.Remove(data.Length - 1);
                    data = data.Replace('"', ' ');
                    data = data.Trim();
                    string[] res = data.Split(',');
                    SetTexts(res);
                    break;


			}
		}
	}

    private void SetTexts(string[] entries)
    {

        List<ScoreItem> scores = new List<ScoreItem>();

        foreach (var e in entries)
        {
            string name = e.Split('~')[0].Trim();
            string score = e.Split("~")[1].Trim();

            scores.Add(new ScoreItem() { Score = score, UserName = name });
        }

        scores = scores.OrderByDescending(x => int.Parse(x.Score)).ToList();

        int i = 0;
        foreach (var e in scores)
        {
            if (i >= Texts.Length)
                break;

            Texts[i].text = e.UserName + " " + e.Score;
            i++;
        }
    }

}