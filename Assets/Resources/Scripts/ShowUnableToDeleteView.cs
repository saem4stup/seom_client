using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnableToDeleteView : MonoBehaviour
{
    
    public void showView()
    {
        GameObject view = GameObject.Find("AdditionalViews").transform.Find("UnableToDeleteView").gameObject;
        view.SetActive(true);
    }

}
