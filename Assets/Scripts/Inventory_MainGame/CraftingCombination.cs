using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingCombination : MonoBehaviour
{
    public static CraftingCombination instance;

    public int firstID;
    public int secondID;
    public int thirdID;

    public int firstCount;
    public int secondCount;
    public int thirdCount;

    public int outputID;

    public CraftingCombination(int _firstID, int _firstCount, int _secondID, int _secondCount, int _thirdID, int _thirdCount, int _outputID)
    {
        firstID = _firstID;
        firstCount = _firstCount;
        secondID = _secondID;
        secondCount = _secondCount;
        thirdID = _thirdID;
        thirdCount = _thirdCount;
        outputID = _outputID;
    }

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<CraftingCombination> combinations = new List<CraftingCombination>();

    private void Start()
    {
        combinations.Add(new CraftingCombination(101, 3, 0, 0, 0, 0, 102));
    }
}
