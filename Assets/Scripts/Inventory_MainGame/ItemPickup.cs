using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ItemPickup : MonoBehaviour
{
    // E 키를 눌러 아이템을 습득함

    public int _itemID;
    public int _count;

    public UnityEvent Picked;

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
        GameObject instantiatedParticle = Instantiate(particlePrefab, objectPosition, objectRotation); // 인스턴스화
        instantiatedParticle.transform.SetParent(transform); // 파티클을 현재 오브젝트의 자식으로 지정함
        particleSystem = particlePrefab.GetComponent<ParticleSystem>(); // ParticleSystem 컴포넌트 가져오기
        particleSystem.Play();
    }

    private void FixedUpdate()
    {
        if (CheckObjectInCamera(gameObject))
        {
            //Debug.Log("CheckObjectInCamera() " + gameObject.GetComponent<ItemPickup>()._itemID);
        }
    }

    public void Pickup() // 아이템 줍기
    {
        //pickingID = item.GetComponent<ItemPickup>()._itemID;
        //pickingCount = item.GetComponent<ItemPickup>()._count;

        Picked.Invoke();

        scriptManager.FindScriptByItemID(_itemID);
        scriptManager.ShowScript();

        TextLogs.instance.GetItemLog(_itemID);
        Inventory.instance.GetAnItem(_itemID, _count);
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

    public void CheckConditions()
    {

    }
}