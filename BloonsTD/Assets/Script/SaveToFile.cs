using UnityEngine;
using System;
using System.IO;

[Serializable]
public class PlayerData
{
    // mute audio settings
    public bool MuteAudio;
    public bool MuteSound;
    // mute sound settings
    // stats
}
public class SaveToFile : MonoBehaviour
{
    private string filePath;

    public GameObject SaveButton;
    private BoxCollider2D saveButtonCollider;

    //chatgpt 
    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }

    void Start()
    {
        saveButtonCollider = SaveButton.GetComponent<BoxCollider2D>();

        PlayerData loadedData = LoadData();
        ApplyLoadedData(loadedData);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (saveButtonCollider == Physics2D.OverlapPoint(worldPoint))
            {
                SaveData();
            }
        }
    }

    public void SaveData()
    {
        PlayerData Data = new PlayerData();
        Data.MuteAudio = Audio.MuteMusic;
        Data.MuteSound = Audio.MuteSound;

        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Data saved to: " + filePath);
    }

    public PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Data loaded from: " + filePath);
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found. Returning new data.");
            return new PlayerData();
        }
    }

    public void ApplyLoadedData(PlayerData data)
    {
        Audio.MuteMusic = data.MuteAudio;
        Audio.MuteSound = data.MuteSound;
    }
}
