using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public int CurrentStage;

    public GameObject MianUI;
    public GameObject Name;
    public GameObject PauseButton;
    public GameObject ExitButton_;
    public GameObject StartButton_;
    public GameObject BackButton;
    public List<GameObject> NonMainUI = new List<GameObject>();
    public GameObject PauseUI;
    public GameObject ExitUI;

    public WayGame waygame;
    public KindGame kindgame;
    public ClockGame clockgame;
    public PlaceGame placegame;

    public List<AudioSource> SFX = new List<AudioSource>();

    public List<Sprite> TextSprite = new List<Sprite>();
    public GameObject TextBox;
    public GameObject TextMessage;

    public bool click = true;

    public void Start()
    {
        Screen.SetResolution(1280, 800, true);
    }

    public void StartAndBackButton(bool check)
    {
        ExitButton_.SetActive(!check);
        StartButton_.SetActive(!check);
        Name.SetActive(!check);
        MianUI.SetActive(check);
        PauseButton.SetActive(check);
        BackButton.SetActive(check);
    }

    public void ExitUIButton(bool check)
    {
        ExitUI.SetActive(check);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void StageSelect(int StageNumber)
    {
        BackButton.SetActive(false);
        CurrentStage = StageNumber;

        switch (CurrentStage)
        {
            case 1:
                waygame.GameStart();
                break;
            case 2:
                clockgame.GameStart();
                break;
            case 3:
                kindgame.GameStart();
                break;
            case 4:
                placegame.GameStart();
                break;
        }

        MianUI.SetActive(false);
    }

    public void GamePause()
    {
        click = false;
        Time.timeScale = 0f;
        PauseUI.SetActive(true);
    }

    public void Resume()
    {
        click = true;
        Time.timeScale = 1f;
        PauseUI.SetActive(false);
    }

    public void GoToMain()
    {
        BackButton.SetActive(true);
        Time.timeScale = 1f;

        MianUI.SetActive(true);
        PauseUI.SetActive(false);

        waygame.StopAllCoroutines();
        waygame.StopSound();

        clockgame.StopAllCoroutines();
        clockgame.StopSound();

        kindgame.StopAllCoroutines();
        kindgame.StopSound();
        kindgame.RemoveLine();

        placegame.StopAllCoroutines();
        placegame.StopSound();
        placegame.RemoveLine();

        for (int i = 0; i < NonMainUI.Count; i++)
        {
            NonMainUI[i].SetActive(false);
        }
    }

    bool soundscale = true;

    public List<Sprite> btn = new List<Sprite>();
    public void SoundScale(Image obj)
    {
        for (int i = 0; i < SFX.Count; i++)
        {
            if (soundscale)
            {
                SFX[i].volume = 0;
            }
            else
            {
                SFX[i].volume = 1;
                if (i == 1 || i == 3)
                {
                    SFX[i].volume = 0.3f;
                }
            }
        }
        soundscale = !soundscale;
        obj.sprite = btn[(int)SFX[0].volume];
    }

    public void ChangeText(int i)
    {
        if (!TextBox.activeSelf)
            TextBox.SetActive(true);
        TextMessage.GetComponent<Image>().sprite = TextSprite[i];
    }
}
