using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnableToDeleteView : MonoBehaviour
{
    /*public void ShowView()
    {
        GameObject view = GameObject.Find("Views").transform.Find("UnableToDeleteView").gameObject;
        view.SetActive(true);

    }*/
    public IEnumerator routineForShowView()
    {
        GameObject view = GameObject.Find("AdditionalViews").transform.Find("UnableToDeleteView").gameObject;
        view.SetActive(true);
        yield return new WaitForSeconds(2f);
        view.SetActive(false);
    }
    public void showView()
    {
        StartCoroutine(routineForShowView());
    }

}
