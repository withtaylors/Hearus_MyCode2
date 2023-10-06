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
        itemList.Add(new Item(101, "질긴 넝쿨", "질긴 넝쿨. 잘라내기는 어렵지만 그만큼 질기고 잘 끊어지지 않는다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(102, "밧줄", "넝쿨로 만든 꽤 튼튼한 밧줄. 여러 번 쓸 수 있다.", Item.ItemType.소모품, Item.ItemEffect.기타, 0, true, 10));
        itemList.Add(new Item(103, "망가진 태엽 인형", "별 쓸모는 없는 태엽 인형. 속에 이끼가 잔뜩 꼈는지 잘 움직이지 않는다. 어릴 적 가지고 있던 인형과 비슷하게 생겼다.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(104, "냄새무당버섯", "선명한 빨간색의 독버섯.", Item.ItemType.소모품, Item.ItemEffect.피해, 10));
        itemList.Add(new Item(105, "버터넛", "부드럽고 달콤한 호박. 크기가 커 배부르게 먹을 수 있다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(106, "포도", "잘 익은 포도. 향이 강하다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(107, "은행", "은행나무에서 나오는 은행. 많이 먹으면 배탈이 날 수 있다.", Item.ItemType.소모품, Item.ItemEffect.회복, 5));
        itemList.Add(new Item(108, "죽순", "대나무의 어린 싹. 아삭아삭한 식감이 일품이다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(109, "대나무 잎", "대나무의 이파리. 특유의 향이 난다.", Item.ItemType.소모품, Item.ItemEffect.회복, 5));
        itemList.Add(new Item(110, "고비", "동그랗게 말린 모양의 양치식물. 연하고 독성도 없다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(111, "어린 까마중 줄기", "까마중의 어린 줄기. 열매는 먹을 수 없으나 어린 줄기는 먹을 수 있다.", Item.ItemType.소모품, Item.ItemEffect.회복, 5));
        itemList.Add(new Item(112, "표고버섯", "행성에서도 자주 먹었던 표고버섯. 자연산이라 그런지 향이 진하다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(113, "송이버섯", "행성에서는 매우 비싸게 팔리는 송이버섯. 버섯이라 빨리 먹는 편이 좋다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10, false));
        // 시간 경과 시 HP -10 구현 필요
        itemList.Add(new Item(114, "까마중", "까맣고 동그란 열매. 약한 독성이 있어 먹으면 배탈이 난다.", Item.ItemType.소모품, Item.ItemEffect.피해, 5));
        itemList.Add(new Item(115, "고사리", "잘 말려서 익히면 먹을 수 있는 고사리. 말릴 시간이 없기 때문에 아예 먹지 않는 편이 좋다.", Item.ItemType.소모품, Item.ItemEffect.피해, 10));
        itemList.Add(new Item(116, "은방울꽃", "향기가 좋은 은방울꽃. 행성에서는 향수의 재료로 많이 쓰이나 독이 있어 식용으로는 쓰이지 않는다.", Item.ItemType.소모품, Item.ItemEffect.피해, 10));
        itemList.Add(new Item(117, "여로", "크고 기다란 여러 이파리가 모여있는 식물. 약한 독성을 띈다.", Item.ItemType.소모품, Item.ItemEffect.피해, 10));
        itemList.Add(new Item(118, "붉은대자리공 잎", "붉은 줄기를 가진 식물의 이파리. 강한 독성을 띈다.", Item.ItemType.소모품, Item.ItemEffect.피해, 10));
        itemList.Add(new Item(119, "붉은사슴뿔버섯", "사슴 뿔처럼 생긴 붉은 버섯. 매우 강한 독성이 있다.", Item.ItemType.소모품, Item.ItemEffect.피해, 20));
        itemList.Add(new Item(120, "광대버섯", "하얀 점박이 무늬가 있는 빨간 버섯. 강한 독성을 띄고 있다.", Item.ItemType.소모품, Item.ItemEffect.피해, 15));
        itemList.Add(new Item(121, "화경버섯", "독이 있는 버섯. 먹으면 죽진 않지만 배탈이 난다. 밤엔 빛이 나 주위를 밝힌다.", Item.ItemType.소모품, Item.ItemEffect.피해, 5));
        itemList.Add(new Item(122, "까마중 줄기", "까마중의 줄기. 통증을 완화시켜 준다.", Item.ItemType.소모품, Item.ItemEffect.기타, 0));
        // 까마중 줄기: 통증 디버프 해제
        itemList.Add(new Item(123, "골쇄보", "고사리와 비슷하게 생긴 식물. 찧어서 바르면 지혈된다.", Item.ItemType.소모품, Item.ItemEffect.기타, 0));
        // 골쇄보: 타박상 디버프 해제
        itemList.Add(new Item(124, "쐐기풀", "겉에 작은 털들이 있는 풀. 먹거나 다친 부위에 찧어서 바르면 해독된다.", Item.ItemType.소모품, Item.ItemEffect.기타, 0));
        // 쐐기풀: 타박상 디버프 해제
        itemList.Add(new Item(125, "큰 나무껍질", "커다랗게 떨어져 나온 나무 껍질. 여기의 나무들이 매우 크기에 껍질도 두껍고 큰 것 같다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(126, "두꺼운 나뭇가지", "얇은 장작 정도 두께의 매우 두꺼운 나뭇가지. 여기의 나무들이 매우 크기에 나뭇가지도 두꺼운 것이 나오는 것 같다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(127, "나뭇가지", "행성에서도 볼 수 있는 평범한 두께의 나뭇가지. 여기의 나무들에 비해서는 아주 얇은 잔가지 정도인 것 같다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(128, "나뭇잎", "넓은 나뭇잎.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(129, "돌멩이", "작은 돌멩이. 부싯돌 정도로 쓸 수 있는 크기이다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(130, "나무 베리", "달콤한 맛이 나는 과일. 생김새는 독특하지만 맛은 매우 좋다.", Item.ItemType.소모품, Item.ItemEffect.회복, 15));
        itemList.Add(new Item(131, "산딸기", "신맛이 강한 과일. 빨갛게 잘 익은 것일수록 맛이 좋다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(132, "블랙베리", "산딸기보다 약간 단맛이 도는 과일. 산딸기와 달리 검은색이다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(133, "액체 연료", "우주선의 연료로 쓰이는 액체 연료.", Item.ItemType.연료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(134, "물에 젖은 나뭇가지", "물에 젖은 나뭇가지. 바닷물이라 그런지 약간 끈적거린다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        // 물에 젖은 나뭇가지: 0.5개씩 획득?...???
        itemList.Add(new Item(136, "마그네슘 덩어리", "연료를 만드는 주 재료인 마그네슘 덩어리.", Item.ItemType.연료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(137, "미역", "먹을 수 있는 미역.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(138, "김", "바닷가에서 자라는 김. 은근 중독성이 있는 맛이다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(139, "목화", "하얀 목화. 우주복을 고치거나 필터를 만들 수 있는 등 다양한 곳에 쓸 수 있다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(140, "소금 결정", "깨끗하고 큰 소금 결정. 쓸모는 없는 것 같다.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(141, "조개 껍데기", "예쁘지만 쓸모는 없는 조개 껍데기.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(142, "수정", "예쁘지만 쓸모는 없다.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(143, "부식된 기계", "바닷물 때문에 많이 부식된 기계. 그래도 원래의 모습이 어느정도 남아있다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(144, "알루미늄 분말", "우주선 연료의 재료로 쓰이는 알루미늄 분말.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(145, "피스톤 링", "우주선의 엔진을 고칠 수 있는 피스톤 링.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(146, "해초", "얕은 바다에서 자라는 해초.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(147, "독이 빠진 전갈 꼬리", "햇빛에 바싹 말라서인지 독이 빠진 전갈 꼬리.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(148, "웰위치아", "사막에서 자라는 식물. 무척 거대하다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
        itemList.Add(new Item(149, "바싹 마른 나뭇가지", "바싹 마른 나뭇가지. 탄력이 전혀 없다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(151, "녹슨 기계", "바깥에 오래 있어서인지 많이 녹슨 기계. 그래도 원래의 모습을 알아볼 수는 있다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(152, "실린더", "우주선의 엔진을 고칠 수 있는 실린더.", Item.ItemType.부품, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(153, "바싹 마른 선인장", "한때 비가 전혀 내리지 않았는지 바싹 마른 선인장.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(154, "헤드 가스켓", "우주선의 엔진을 고칠 수 있는 헤드 가스켓.", Item.ItemType.부품, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(155, "메모", "간단한 연료 제작법이 담긴 메모. 과거 지구에서는 간이연료를 꽤 자주 만들었던 것 같다.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(156, "무언가의 열쇠", "무언가를 열기 위한 열쇠.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(157, "누군가의 편지", "누군가의 마음이 깊게 담긴 편지. 수신자가 그 마음을 받았었다면 좋겠다.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(158, "오래된 통조림", "언제 만들어졌는지 모르는 오래된 통조림.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(159, "연료 탱크", "우주선의 엔진을 고칠 수 있는 연료 탱크.", Item.ItemType.부품, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(160, "액체 연료 2", "우주선의 연료로 쓰이는 액체 연료.", Item.ItemType.연료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(161, "헤드 볼트", "우주선의 엔진을 고칠 수 있는 헤드볼트.", Item.ItemType.부품, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(162, "연료 키트", "간단하게 연료를 만들 수 있는 키트. 원래는 난로 등의 연료를 만들기 위해 쓰인 것 같다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(163, "액체 연료 3", "우주선의 연료로 쓰이는 액체 연료.", Item.ItemType.연료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(164, "도끼", "", Item.ItemType.도구, Item.ItemEffect.기타, 0, false));
        itemList.Add(new Item(165, "닫혀 있는 상자", "굳게 닫혀 있는 상자. 도구를 사용하면 열 수 있을 것 같다.", Item.ItemType.기타, Item.ItemEffect.기타, 0, false));
        itemList.Add(new Item(166, "엔진 블록", "우주선의 엔진을 고칠 수 있는 엔진 블록.", Item.ItemType.부품, Item.ItemEffect.기타, 0, false));
    }
}
