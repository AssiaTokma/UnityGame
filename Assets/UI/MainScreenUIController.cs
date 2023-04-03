using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainScreenUIController : MonoBehaviour
{
    private Button buttonPlay;
    private Button buttonExit;
    private Label scoreLabel;
    public UserInterfaceManager uim;

    private void OnEnable()
    {
        var visualElements = GetComponent<UIDocument>().rootVisualElement;

        buttonPlay = visualElements.Q<Button>("ButtonPlay");
        buttonExit = visualElements.Q<Button>("ButtonExit");
        scoreLabel = visualElements.Q<Label>("ScoreLabel");

        buttonPlay.RegisterCallback<ClickEvent>(ev => uim.Play(1));
        buttonExit.RegisterCallback<ClickEvent>(ev => uim.Exit());
    }

    void Start()
    {
        scoreLabel.text = "Score: " + PlayerPrefs.GetInt("MaxWave", 0);
    }
}
