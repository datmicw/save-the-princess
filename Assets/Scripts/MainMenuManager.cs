using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Panel_Options;

    void Start()
    {
        // Kiểm tra xem Panel_Options đã được gán chưa
        if (Panel_Options != null)
        {
            Panel_Options.SetActive(false); 
        }
        else
        {
            Debug.LogWarning("Panel_Options is not assigned!");
        }
    }

    public void StartGame()
    {
        Debug.Log("Starting Game...");
        if (Application.CanStreamedLevelBeLoaded("Level_1"))
        {
            SceneManager.LoadScene("Level_1");
        }
        else
        {
            Debug.LogError("Scene 'Level_1' not found!");
        }
    }

    public void ShowOptions()
    {
        Debug.Log("Showing Options...");
        if (Panel_Options != null)
        {
            Panel_Options.SetActive(true); // Hiển thị Panel Option
        }
        else
        {
            Debug.LogWarning("Panel_Options is missing!");
        }
    }

    // Hàm Đóng Option
    public void CloseOptions()
    {
        Debug.Log("Closing Options...");
        if (Panel_Options != null)
        {
            Panel_Options.SetActive(false); // Ẩn Panel Option
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void HideOptions()
    {
        if (Panel_Options != null)
        {
            Panel_Options.SetActive(false); // Ẩn panel options
        }
        else
        {
            Debug.LogWarning("Panel_Options is missing!");
        }
    }
}
