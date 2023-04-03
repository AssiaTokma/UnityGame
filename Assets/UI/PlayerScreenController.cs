using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScreenController : MonoBehaviour
{
    private Label coinsLabel;
    private Label waveLabel;
    private VisualElement defeatScreen;

    private int record;
    private int currentWave = 0;

    public UserInterfaceManager uim;

    private void OnEnable()
    {

        var visualElements = GetComponent<UIDocument>().rootVisualElement;

        coinsLabel = visualElements.Q<Label>("Coins");
        waveLabel = visualElements.Q<Label>("WaveLabel");
        defeatScreen = visualElements.Q<VisualElement>("DefeatMenu");


        visualElements.RegisterCallback<ClickEvent>(CallbackButton, TrickleDown.TrickleDown);

        UserStatsController.coinsUpdate.AddListener(UpdateCoinsLabel);
        BaseHealth.baseDestroyed.AddListener(ShowDefeatScreen);
        GameController.newWave.AddListener(UpdateWaveCount);

        record = PlayerPrefs.GetInt("MaxWave", 0);
    }

    public int GetRecord()
    {
        return record;
    }

    private void CallbackButton(ClickEvent evt)
    {
        if (evt.target.GetType() == typeof(Button))
        {
            if ((evt.target as Button).name == "ButtonMenu")
            {
                uim.BackToMenu();
                return;
            }
            if ((evt.target as Button).name == "ButtonRestart")
            {
                uim.Restart();
                return;
            }
            if ((evt.target as Button).name == "ButtonExit")
            {
                uim.Exit();
                return;
            }
            uim.Buy((evt.target as Button).name);
        }
    }

    private void UpdateCoinsLabel(int coins)
    {
        coinsLabel.text = "Coins: " + coins.ToString();
    }

    public void ShowDefeatScreen()
    {
        defeatScreen.visible = true;
        if (currentWave > record)
        {
            PlayerPrefs.SetInt("MaxWave", currentWave);
        }
        GameController gameControler =
            GameObject
            .FindGameObjectWithTag("GameController")?
            .GetComponent<GameController>();
        if (gameControler != null)
        {
            Destroy(gameControler.gameObject);
        }
    }

    private void UpdateWaveCount(int wave)
    {
        currentWave = wave;
        waveLabel.text = "Wave: " + wave.ToString() + " |  Score: " + record.ToString();
    }

    void OnDestroy()
    {
        UserStatsController.coinsUpdate.RemoveListener(UpdateCoinsLabel);
        BaseHealth.baseDestroyed.RemoveListener(ShowDefeatScreen);
        GameController.newWave.RemoveListener(UpdateWaveCount);
    }
}
