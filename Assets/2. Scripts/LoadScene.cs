using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private void Update()
    {
        
    }
    public static LoadScene instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<LoadScene>();
            }

            return m_instance;
        }
    }
    private static LoadScene m_instance; // ΩÃ±€≈Ê¿Ã «“¥Áµ… static ∫Øºˆ

    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        AudioManager.Instance.PlaySFX("StartButton");
        if(sceneName == "Tutorial")
        {
            AudioManager.Instance.PlayMusic("Success");
        }
        else if(sceneName == "Main")
        {
            AudioManager.Instance.PlayMusic("BGM");
        }
        else if (sceneName == "Game")
        {
            AudioManager.Instance.PlayMusic("GamePlay");
        }
    }
}
