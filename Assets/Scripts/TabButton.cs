using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


//[RequreComponent(typeof(Image))]
public class TabButton : MonoBehaviour/*, IPointerEnterHandler, IPointerClickHandler.IPointerExitHandler*/
{
    /*    public TabGroup tabGroup;

        //public Image background;

        public void OnPointerClick(PointerEvent eventData)
        {
            tabGroup.OnTabSelected(this);
        }

        public void OnPointerEnter(PointerEvent eventData)
        {
            tabGroup.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEvent eventData)
        {
            tabGroup.OnTabExit(this);
        }

        void Start()
        {
            //backgroud = GetComponent<Image>();
            tabGroup.Subscribe(this);
        }*/


    Image background;
    //public TMP_Text text;
    //public GameObject newButton;

    public Sprite idleImg;
    public Sprite selectedImg;

    public void Awake()
    {
        background = GetComponent<Image>();
        //text = GameObject<Text>();
        //normaltextcolor = text.color; 

        //GameObject object = GameObject.Find("Text");
        //text = GetComponent<TMP_Text>();
    }

    public void Selected()
    {
        background.sprite = selectedImg;
        //text.color = Color.black;
        //object.GetComponent<Text>().textColor = Color.Red;
        //newButton.GetComponent
        //text.color = Color.red;
    }

    public void DeSelected()
    {
        background.sprite = idleImg;
        //text.color = Color.white;
    }
}
