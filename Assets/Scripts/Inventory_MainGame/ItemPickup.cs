using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPickup : MonoBehaviour
{
    // E 키를 눌러 아이템을 습득함

    public int _itemID;
    public int _count;

    private ScriptManager scriptManager;
    private UnityEngine.Camera selectedCamera;
    private Vector3 objectPosition;
    private Quaternion objectRotation;
    private GameObject particlePrefab;
    private ParticleSystem particleSystem;

    private void Start()
    {
        scriptManager = FindObjectOfType<ScriptManager>(); // 스크립트 매니저: 아이템 스크립트를 재생할 때 쓰임
        selectedCamera = UnityEngine.Camera.main; // 메인 카메라

        objectPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); // 해당 오브젝트의 위치 저장
        objectRotation = new Quaternion(-90, 0, 0, 90); // 파티클이 위로 나오도록 Rotation 설정

        particlePrefab = Resources.Load("Object_Particle") as GameObject; // Resources/Prefabs/Object Particle 로드
        Instantiate(particlePrefab, objectPosition, objectRotation); // 인스턴스화
        particleSystem = particlePrefab.GetComponent<ParticleSystem>(); // ParticleSystem 컴포넌트 가져오기
        particleSystem.Play();
    }

    private void FixedUpdate()
    {
        if (CheckObjectInCamera(gameObject))
        {
            Debug.Log("CheckObjectInCamera() " + gameObject.GetComponent<ItemPickup>()._itemID);
        }
    }

    public void Pickup(GameObject item) // 아이템 줍기
    {
        int pickingID;
        int pickingCount;

        pickingID = item.gameObject.GetComponent<ItemPickup>()._itemID;
        pickingCount = item.gameObject.GetComponent<ItemPickup>()._count;

        scriptManager.FindScriptByItemID(pickingID);
        scriptManager.ShowScript();

        TextLogs.instance.GetItemLog(pickingID);
        Inventory.instance.GetAnItem(pickingID, pickingCount);
    }

    private bool CheckObjectInCamera(GameObject item) // 오브젝트가 카메라 안에 있는지 확인
    {
        Vector3 itemPos = selectedCamera.WorldToViewportPoint(item.transform.position);

        if (itemPos.x >= 0 && itemPos.x <= 1 &&
            itemPos.y >= 0 && itemPos.y <= 1 && itemPos.z > 0)
            return true;
        else
            return false;
    }
}