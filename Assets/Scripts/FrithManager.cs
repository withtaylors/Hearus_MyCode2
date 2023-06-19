using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrithManager : MonoBehaviour
{
    public static FrithManager instance;


    private void Awake()
    {
        instance = this; // 싱글톤
    }
}
