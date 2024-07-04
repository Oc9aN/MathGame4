using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockGame : MonoBehaviour {

    public Main main;

    public GameObject Clock;
    public GameObject ClockUI;

    public GameObject HourObj;
    private int H;
    public GameObject MinuteObj;
    private int M;

    public List<Vector2> CurrentTime = new List<Vector2>();

    private int TimeIndex;

    public Text Message;
    public Text TimeMessage;

    public List<AudioSource> sound = new List<AudioSource>();

    public void StopSound()
    {
        for (int i = 0; i < sound.Count; i++)
        {
            sound[i].Stop();
        }
    }

    public void GameStart()
    {
        main.ChangeText(17);
        MinuteObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        HourObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        H = 0;
        M = 0;
        ClockUI.SetActive(true);
        Clock.SetActive(true);

        TimeIndex = Random.Range(0, CurrentTime.Count);

        MessgeMakeing();
        TimeMessage.text = CurrentTime[TimeIndex].x.ToString() + " 시 " + (CurrentTime[TimeIndex].y * 10).ToString() +  " 분";

        int randH = Random.Range(1, 3);
        M = Random.Range(0, 6);

        H = (int)CurrentTime[TimeIndex].x - randH;

        Debug.Log(H + "," + M);
        MinuteObj.transform.Rotate(0f, 0f, -60f * M);
        HourObj.transform.Rotate(0f, 0f, -5f * (H * 6 + M));
    }

    private void MessgeMakeing()
    {
        switch (TimeIndex)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                Message.text = "일어나는 시간";
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                Message.text = "자는 시간";
                break;
            case 8:
            case 9:
            case 10:
                Message.text = "아침 먹는 시간";
                break;
            case 11:
            case 12:
            case 13:
                Message.text = "점심 시간";
                break;
            case 14:
            case 15:
            case 16:
                Message.text = "저녁 먹는 시간";
                break;
            case 17:
            case 18:
                Message.text = "학교 가는 시간";
                break;
            case 19:
            case 20:
            case 21:
            case 22:
                Message.text = "학교 끝나는 시간";
                break;
            case 23:
            case 24:
            case 25:
            case 26:
                Message.text = "TV 보는 시간";
                break;
            case 27:
            case 28:
            case 29:
            case 30:
                Message.text = "축구하는 시간";
                break;
            case 31:
            case 32:
            case 33:
            case 34:
                Message.text = "공부하는 시간";
                break;
            case 35:
            case 36:
            case 37:
            case 38:
                Message.text = "과일 먹는 시간";
                break;
            case 39:
            case 40:
            case 41:
                Message.text = "간식 먹는 시간";
                break;
            case 42:
            case 43:
            case 44:
            case 45:
                Message.text = "심부름 시간";
                break;
            case 46:
            case 47:
            case 48:
                Message.text = "양치질 시간";
                break;
            case 49:
            case 50:
            case 51:
            case 52:
                Message.text = "청소하는 시간";
                break;
        }
    }

    Coroutine TS;
    public void MoveM(int Rotate)
    {
        MinuteObj.transform.Rotate(0f, 0f, 60f * Rotate);
        HourObj.transform.Rotate(0f, 0f, 5f * Rotate);
        if (Rotate == 1)
        {
            if (M == 0)
            {
                M = 6;
                if (H == 0)
                    H = 12;
                H--;
            }
            M--;
        }
        else if (Rotate == -1)
        {
            M++;
            if (M == 6)
            {
                M = 0;
                H++;
                if (H == 12)
                    H = 0;
            }
        }
        if (TS != null)
            StopCoroutine(TS);

        TS = StartCoroutine(TimeSound());
    }

    private IEnumerator TimeSound()
    {
        StopSound();
        sound[H].Play();
        yield return new WaitForSeconds(1f);
        if (M != 0)
            sound[11 + M].Play();
        Debug.Log(H + "," + M);
        yield return 0;
    }

    public void CheckButton()
    {
        if (H == 0)
            H = 12;
        if (H == CurrentTime[TimeIndex].x && M == CurrentTime[TimeIndex].y)
        {
            Debug.Log("성공");
            StartCoroutine(Ending());
        }
        else
        {
            StopSound();
            sound[17].Play();
            sound[18].Play();
            Debug.Log("실패");
        }
        if (H == 12)
            H = 0;
    }

    private IEnumerator Ending()
    {
        StopSound();
        sound[19].Play();
        sound[20].Play();
        main.ChangeText(6);
        yield return new WaitForSeconds(2f);
        GameStart();
        yield return 0;
    }
}
