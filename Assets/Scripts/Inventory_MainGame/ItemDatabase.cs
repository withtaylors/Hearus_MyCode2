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
        itemList.Add(new Item(106, "포도", "", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(107, "은행", "은행나무에서 나오는 은행. 많이 먹으면 배탈이 날 수 있다.", Item.ItemType.소모품, Item.ItemEffect.회복, 5));
        itemList.Add(new Item(108, "죽순", "", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(109, "대나무 잎", "", Item.ItemType.소모품, Item.ItemEffect.회복, 5));
        itemList.Add(new Item(110, "고비", "동그랗게 말린 모양의 양치식물. 연하고 독성도 없다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(111, "어린 까마중 줄기", "", Item.ItemType.소모품, Item.ItemEffect.회복, 15));
        itemList.Add(new Item(112, "표고버섯", "행성에서도 자주 먹었던 표고버섯. 자연산이라 그런지 향이 진하다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(113, "송이버섯", "행성에서는 매우 비싸게 팔리는 송이버섯. 버섯이라 빨리 먹는 편이 좋다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        // 시간 경과 시 HP -10 구현 필요
        itemList.Add(new Item(114, "까마중", "까맣고 동그란 열매. 약한 독성이 있어 먹으면 배탈이 난다.", Item.ItemType.소모품, Item.ItemEffect.피해, 5));
        itemList.Add(new Item(115, "고사리", "잘 말려서 익히면 먹을 수 있는 고사리. 말릴 시간이 없기 때문에 아예 먹지 않는 편이 좋다.", Item.ItemType.소모품, Item.ItemEffect.피해, 10));
        itemList.Add(new Item(116, "은방울꽃", "향기가 좋은 은방울꽃. 행성에서는 향수의 재료로 많이 쓰이나 독이 있어 식용으로는 쓰이지 않는다.", Item.ItemType.소모품, Item.ItemEffect.피해, 10));
        itemList.Add(new Item(117, "여로", "", Item.ItemType.소모품, Item.ItemEffect.회복, 15));
        itemList.Add(new Item(118, "붉은대자리공 잎", "", Item.ItemType.소모품, Item.ItemEffect.회복, 15));
        itemList.Add(new Item(119, "붉은사슴뿔버섯", "", Item.ItemType.소모품, Item.ItemEffect.회복, 15));
        itemList.Add(new Item(120, "광대버섯", "", Item.ItemType.소모품, Item.ItemEffect.회복, 15));
        itemList.Add(new Item(121, "화경버섯", "독이 있는 버섯. 먹으면 죽진 않지만 배탈이 난다. 밤엔 빛이 나 주위를 밝힌다.", Item.ItemType.소모품, Item.ItemEffect.피해, 5));
        //itemList.Add(new Item(122, "까마중 줄기", "", Item.ItemType.소모품, Item.ItemEffect.회복, 15));
        //itemList.Add(new Item(123, "골쇄보", "", Item.ItemType.소모품, Item.ItemEffect.회복, 15));
        //itemList.Add(new Item(124, "쐐기풀", "", Item.ItemType.소모품, Item.ItemEffect.회복, 15));
        itemList.Add(new Item(125, "큰 나무껍질", "커다랗게 떨어져 나온 나무 껍질. 여기의 나무들이 매우 크기에 껍질도 두껍고 큰 것 같다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(126, "두꺼운 나뭇가지", "얇은 장작 정도 두께의 매우 두꺼운 나뭇가지. 여기의 나무들이 매우 크기에 나뭇가지도 두꺼운 것이 나오는 것 같다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(127, "나뭇가지", "행성에서도 볼 수 있는 평범한 두께의 나뭇가지. 여기의 나무들에 비해서는 아주 얇은 잔가지 정도인 것 같다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(128, "나뭇잎", "넓은 나뭇잎.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(129, "돌멩이", "작은 돌멩이. 부싯돌 정도로 쓸 수 있는 크기이다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(130, "나무베리", "", Item.ItemType.소모품, Item.ItemEffect.회복, 15));
        itemList.Add(new Item(131, "산딸기", "신맛이 강한 과일. 빨갛게 잘 익은 것일수록 맛이 좋다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(132, "블랙베리", "산딸기보다 약간 단맛이 도는 과일. 산딸기와 달리 검은색이다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(133, "액체 연료", "", Item.ItemType.연료, Item.ItemEffect.기타, 0));

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
