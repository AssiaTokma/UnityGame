using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : MonoBehaviour
{

    public List<GameObject> assets = new List<GameObject>();

    public void Play(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Buy(string name)
    {
        name = name + "_blueprint";
        foreach (GameObject asset in assets)
        {
            if (asset.name == name)
            {
                GameObject blueprint = GameObject.FindGameObjectWithTag("Blueprint");
                if (!blueprint)
                {
                    Instantiate(asset);
                } else
                {
                    Destroy(blueprint);
                    Instantiate(asset);
                }
                break;
            }
        }
    }
}
