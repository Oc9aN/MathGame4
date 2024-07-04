using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KindGame : MonoBehaviour {

    public Main main;

    public List<Sprite> Goods_1 = new List<Sprite>();
    public List<Sprite> Goods_2 = new List<Sprite>();

    public List<SpriteRenderer> sprites_1 = new List<SpriteRenderer>();
    public List<SpriteRenderer> sprites_2 = new List<SpriteRenderer>();

    public List<Transform> points = new List<Transform>();

    public List<LineRenderer> lines = new List<LineRenderer>();

    public List<AudioSource> sound = new List<AudioSource>();

    public void StopSound()
    {
        for (int i = 0; i < sound.Count; i++)
        {
            sound[i].Stop();
        }
    }

    public int[] GetRandomInt(int length, int min, int max)
    {
        int[] randArray = new int[length];
        bool isSame;

        for (int i = 0; i < length; ++i)
        {
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

    public void RemoveLine()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].positionCount = 0;
        }
    }

    public void GameStart()
    {
        StopSound();
        sound[0].Play();
        main.ChangeText(32);
        Count = 0;
        RemoveLine();
        int[] GoodsNum = GetRandomInt(3, 0, Goods_1.Count);
        int[] PositionNum = GetRandomInt(3, 0, 3);

        for (int i = 0; i < 3; i++)
        {
            sprites_2[i].transform.position = points[i].position;

            sprites_1[i].gameObject.SetActive(true);
            sprites_1[i].sprite = Goods_1[GoodsNum[i]];
            sprites_1[i].GetComponent<BoxCollider2D>().size = sprites_1[i].sprite.bounds.size;

            sprites_2[i].gameObject.SetActive(true);
            sprites_2[i].sprite = Goods_2[GoodsNum[i]];
            sprites_2[i].GetComponent<BoxCollider2D>().size = sprites_2[i].sprite.bounds.size;

            sprites_2[i].transform.position = points[PositionNum[i]].position;
        }
    }

    private GameObject Collection_1;
    private GameObject Collection_2;

    private LineRenderer useline;

    private int Count;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.CompareTag("KindGame"))
            {
                hit.transform.GetChild(0).transform.localScale = Vector3.one * 0.3f;
                if (Collection_1 == null)
                    Collection_1 = hit.transform.gameObject;
                else if (hit.transform.GetComponent<SpriteRenderer>().sprite.name != Collection_1.GetComponent<SpriteRenderer>().sprite.name)
                    Collection_2 = hit.transform.gameObject;
            }
        }
        if (Input.GetMouseButtonUp(0) && Collection_2 != null)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].positionCount != 2)
                {
                    useline = lines[i];
                }
            }
            useline.positionCount = 2;

            if (Collection_1.name == Collection_2.name)
            {
                Count++;
                useline.SetPosition(0, Collection_1.transform.position);
                useline.SetPosition(1, Collection_2.transform.position);
                if (Count == 3)
                {
                    StartCoroutine(Ending());
                }
            }
            else
            {
                StopSound();
                sound[1].Play();
                sound[2].Play();
                useline.positionCount = 0;
            }
            Collection_1.transform.GetChild(0).transform.localScale = Vector3.one * 0.5f;
            Collection_2.transform.GetChild(0).transform.localScale = Vector3.one * 0.5f;
            useline = null;
            Collection_1 = null;
            Collection_2 = null;
        }
    }

    private IEnumerator Ending()
    {
        StopSound();
        sound[3].Play();
        sound[4].Play();
        main.ChangeText(6);
        yield return new WaitForSeconds(2f);
        GameStart();
        yield return 0;
    }
}
