using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int maxHealth;
    public Vector3 position;
    public Quaternion rotation;
    public int magSize;
    public int totalAmmo;
    public int bulletsShot;
    public int bulletsLeft;
}

public class SaveLoadJSON : MonoBehaviour
{
    PlayerData pData;
    string savePath;
    public float autoSaveTimer = 0;
    public float autoSaveInterval = 120;
    public PlayerMovementSM playsm;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/Player.json";
        pData = new PlayerData();
        playsm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementSM>();

        if (File.Exists(savePath))
        {
            // We have an existing file, load that one.
            LoadGame();
        }
        else
        {
            SaveCurrent();
        }
    }

    private void Update()
    {
        SaveCurrent();

        autoSaveTimer += Time.deltaTime;

        if (autoSaveTimer >= autoSaveInterval)
        {
            Autosave();
            autoSaveTimer = 0;
        }
    }

    protected void SaveCurrent()
    {
        if (playsm.player != null)
        {   
            pData.health = playsm.health.health;
            pData.maxHealth = playsm.health.maxHealth;
            pData.magSize = playsm.weapon.magazineSize;
            pData.totalAmmo = playsm.weapon.totalAmmo;
            pData.bulletsShot = playsm.weapon.bulletsShot;
            pData.bulletsLeft = playsm.weapon.bulletsLeft;
            pData.position = playsm.player.transform.position;
            pData.rotation = playsm.player.transform.rotation;
        }
    }

    protected void LoadData()
    {
        if (playsm.player != null)
        {
            playsm.health.health = pData.health;
            playsm.health.maxHealth = pData.maxHealth;
            playsm.weapon.magazineSize = pData.magSize;
            playsm.weapon.totalAmmo = pData.totalAmmo;
            playsm.weapon.bulletsShot = pData.bulletsShot;
            playsm.weapon.bulletsLeft = pData.bulletsLeft;
            playsm.player.transform.position = pData.position;
            playsm.player.transform.rotation = pData.rotation;
        }
    }

    void Autosave()
    {
        string savePData = JsonUtility.ToJson(pData);
        File.WriteAllText(savePath, savePData);
    }

    public void SaveGame()
    {
        string savePData = JsonUtility.ToJson(pData);
        File.WriteAllText(savePath, savePData);

        SaveCurrent();
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string loadPData = File.ReadAllText(savePath);
            pData = JsonUtility.FromJson<PlayerData>(loadPData);

            LoadData();

            Debug.Log("File requested from: " + savePath);

            if (Time.timeScale <= 0)
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
            }

            if (SceneManager.GetActiveScene().name != "ShadowsOfBlame")
            {
                SceneManager.LoadScene("ShadowsOfBlame");
            }
            else if (SceneManager.GetActiveScene().name == "ShadowsOfBlame")
            {
                LoadData();
            }
            else
            {
                Debug.Log("No save data to load");
            }
        }
    }

    public void DeleteGame()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        else
        {
            Debug.LogWarning("No file to delete!");
        }
    }
}
