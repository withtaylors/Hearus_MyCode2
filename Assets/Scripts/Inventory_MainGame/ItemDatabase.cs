using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    public GameObject itemDescriptionPrefab;
    public Transform itemDescriptionParent;

    private void Awake()
    {
        // Singleton
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static List<Item> itemList = new List<Item>();

    void Start()
    {
        itemList.Add(new Item(101, "넝쿨", "밧줄을 만들 수 있는 꽤 질긴 넝쿨.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(102, "밧줄", "넝쿨로 만든 꽤 튼튼한 밧줄. 여러 번 쓸 수 있다.", Item.ItemType.소모품, Item.ItemEffect.기타, 0, 10));
        itemList.Add(new Item(103, "망가진 태엽 인형", "별 쓸모는 없는 태엽 인형. 속에 이끼가 잔뜩 꼈는지 잘 움직이지 않는다. 어릴 적 가지고 있던 인형과 비슷하게 생겼다.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(104, "냄새무당버섯", "선명한 빨간색의 독버섯.", Item.ItemType.소모품, Item.ItemEffect.피해, 10));
        itemList.Add(new Item(105, "버터넛", "부드럽고 달콤한 호박. 크기가 커 배부르게 먹을 수 있다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        
        /*
        itemJourneyList.Add(new ItemJourney(103, "태엽 인형을 발견했다. 대여섯 살 무렵이었나? 그즈음 가지고 놀던 것과 꽤 비슷하게 생겼다. 아마 당시 엄청 인기 많던 캐릭터였지." + "워낙 인기가 많아 옆집에 살던 두어 살 어린애가 자기도 가지고 싶다며 볼 때마다 떼를 썼던 것도 기억난다.그렇게 아끼는 장난감은 아니었으나 그 애가 가지겠다 하니 무척 애틋해지고, 소중히 여기게 되었던 것 같다." +
            "지구에서도 같은 캐릭터가 있었던 걸까 ? 예전의 지구를 잘 알지 못하지만, 이걸 보니 그래도 지구와 행성의 공통점을 하나 발견한 기분이다.\n" + "일전 여기에 오기 전 읽은 책에서 타지에서는 고향이 사무치도록 그리워진다는 글을 봤다." +
            "그글은 아마 지구를 떠나고 얼마 지나지 않아 이곳을 그리워하며 썼으리라…. 그가 기억하던 지구는 이런 모습이 아니겠지.그렇다면 이곳을 본다면 그는 어떤 생각을 할까 ? " +
            "나처럼 행성을 그리워할 수도, 돌아갈 수 없는 이전 지구의 모습을 더더욱 그리워할 수도 있겠다.인간의 고향은 지구라 했지만, 지금 내게는 지구가 그 타지다. " +
            "…그래, 지구에서의 매 순간은 나의 행성을 그립게 만든다.이걸 보니 행성으로 돌아가고 싶은 마음이 더 커졌다.\n" +
            "태엽을 돌려보니 잘 돌아가지는 않는다.아마 흙이나 이끼가 끼어서 그런 것 같다."));
        */
    }
}
