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

    private void OnEnable()
    {
        combinations.Add(new CraftingCombination(101, 3, 0, 0, 0, 0, 102));
        combinations.Add(new CraftingCombination(101, 1, 129, 1, 127, 1, 164));
        combinations.Add(new CraftingCombination(147, 3, 127, 1, 0, 0, 168));
        combinations.Add(new CraftingCombination(139, 3, 101, 1, 0, 0, 169));
        combinations.Add(new CraftingCombination(122, 1, 129, 1, 0, 0, 172));
        combinations.Add(new CraftingCombination(123, 1, 129, 1, 0, 0, 172));
        combinations.Add(new CraftingCombination(124, 1, 129, 1, 0, 0, 172));
        combinations.Add(new CraftingCombination(144, 1, 153, 1, 162, 1, 170));
        combinations.Add(new CraftingCombination(143, 1, 151, 1, 0, 0, 171));
    }
}
