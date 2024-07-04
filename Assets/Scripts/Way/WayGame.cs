using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayGame : MonoBehaviour
{

    public GameObject Map;

    public List<Sprite> ObjSprites = new List<Sprite>();

    public List<Sprite> TargetText = new List<Sprite>();

    public List<SpriteRenderer> ObjRenderers = new List<SpriteRenderer>();

    public GameObject Boxs;

    public GameObject WayUI;

    public GameObject Daronge;

    public Transform point;

    public Image finshtext;

    private string finshpoint;

    public List<AudioSource> sound = new List<AudioSource>();

    public void StopSound()
    {
        for (int i = 0; i < sound.Count; i++)
        {
            sound[i].Stop();
        }
    }

    private List<int> getRandomInt(int length, int min, int max)
    {
        List<int> randArray = new List<int>();
        bool isSame;

        for (int i = 0; i < length; ++i)
        {
            randArray.Add(Random.Range(min, max));
            while (true)
            {
                randArray[i] = Random.Range(min, max);
                isSame = false;

                for (int j = 0; j < i; ++j)
                {
                    if (randArray[j] == randArray[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }
        return randArray;
    }

    public void GameStart()
    {
        Daronge.transform.localScale = Vector3.one * 3f;
        StartCoroutine(ShowDarong());
        StopSound();
        sound[1].Play();
        Daronge.transform.position = point.position;
        Map.SetActive(true);
        Daronge.SetActive(true);
        Boxs.SetActive(true);
        WayUI.SetActive(true);
        List<int> randint = getRandomInt(ObjSprites.Count, 0, ObjSprites.Count);
        for (int i = 0; i < ObjSprites.Count; i++)
        {
            ObjRenderers[i].sprite = ObjSprites[randint[i]];
        }
        int ra = Random.Range(0, ObjSprites.Count);
        finshpoint = ObjSprites[ra].name;
        finshtext.sprite = TargetText[ra];
    }

    private IEnumerator ShowDarong()
    {
        yield return new WaitForSeconds(0.5f);
        while (Daronge.transform.localScale != Vector3.one)
        {
            Daronge.transform.localScale -= Vector3.one * 0.1f;
            yield return new WaitForEndOfFrame();
        }
    }

    RaycastHit2D hit;
    public void Move(int move)
    {
        switch (move)
        {
            case 1:
                //Debug.DrawRay(Daronge.transform.position + Vector3.up / 2, Vector3.up * 1f, Color.red, 1000f);
                StopSound();
                sound[4].Play();
                hit = Physics2D.Raycast(Daronge.transform.position + Vector3.up / 2, Vector3.up, 1f, (1 << 8));
                //Debug.Log(hit.transform.tag);
                break;
            case 2:
                StopSound();
                sound[2].Play();
                hit = Physics2D.Raycast(Daronge.transform.position + Vector3.down / 2, Vector3.down, 1f, (1 << 8));
                break;
            case 3:
                StopSound();
                sound[5].Play();
                hit = Physics2D.Raycast(Daronge.transform.position + Vector3.right / 2, Vector3.right, 1f, (1 << 8));
                break;
            case 4:
                StopSound();
                sound[3].Play();
                hit = Physics2D.Raycast(Daronge.transform.position + Vector3.left / 2, Vector3.left, 1f, (1 << 8));
                break;
        }
        if (hit.transform != null && hit.transform.CompareTag("Point"))
        {
            Daronge.transform.position = hit.transform.position;
        }
    }

    public void FinshButton(string name)
    {
        if (finshpoint == name)
        {
            StartCoroutine(Finsh(name));
        }
        else
        {
            StopSound();
            sound[6].Play();
            sound[7].Play();
            Daronge.transform.localScale = Vector3.one * 3f;
            Daronge.transform.position = point.position;
            StartCoroutine(ShowDarong());
        }
    }

    public IEnumerator Finsh(string name)
    {
        StopSound();
        sound[0].Play();
        sound[8].Play();
        yield return new WaitForSeconds(2f);
        Debug.Log(finshpoint + ", " + name);
        GameStart();
    }
}
