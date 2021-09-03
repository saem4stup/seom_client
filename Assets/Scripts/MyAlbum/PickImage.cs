using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickImage : MonoBehaviour
{
    public GameObject Picked; // Top, Bottom이 들어감
    GameObject PickingButton;
    private void Start()
    {
        PickingButton = GameObject.Find("MyAlbum").transform.Find("SelectButton").gameObject;
    }
    public void NotifyImagePicked()
    {
        GameObject PickedThing = PickingButton.GetComponent<AfterPicked>().LastPickedThing;
        PickedThing.transform.Find("Photo").transform.Find("Picker").gameObject.SetActive(false);
        PickingButton.GetComponent<AfterPicked>().LastPickedThing = Picked;
        Picked.transform.Find("Photo").transform.Find("Picker").gameObject.SetActive(true);

        /* 버튼 이미지 색상 변경하는 코드 넣기!! */
        PickingButton.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Tags/Rectangle 55");
        
    }
}
