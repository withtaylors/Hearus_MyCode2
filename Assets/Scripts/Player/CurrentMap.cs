using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CurrentMap : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("중심들판_Border_1")) // 중심들판 <-> 빽빽한숲
            Debug.Log("중심들판_Border_1");

        if (collision.gameObject.name.Equals("빽빽한숲_Border_1")) // 중심들판 <-> 빽빽한숲
            Debug.Log("빽빽한숲_Border_1");

        if (collision.gameObject.name.Equals("빽빽한숲_Border_2")) // 빽빽한숲 <-> 시냇물숲
            Debug.Log("빽빽한숲_Border_1");

        if (collision.gameObject.name.Equals("시냇물숲_Border_1")) // 빽빽한숲 <-> 시냇물숲
            Debug.Log("빽빽한숲_Border_1");
    }
}
