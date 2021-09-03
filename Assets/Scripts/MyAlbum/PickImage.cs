using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickImage : MonoBehaviour
{
    public GameObject Picked; // Top, Bottom�� ��
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

        /* ��ư �̹��� ���� �����ϴ� �ڵ� �ֱ�!! */
        PickingButton.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Tags/Rectangle 55");
        
    }
}
