using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class Click2DForView : MonoBehaviour, IMixedRealityInputHandler
{
    public GameObject view;

    public void OnInputUp(InputEventData eventData)
    {
        if (view.activeSelf == true)
        {
            // 뷰가 보이는 상황
            view.SetActive(false);
        }
        else if (view.activeSelf == false)
        {
            view.SetActive(true);
        }
    }
    public void OnInputDown(InputEventData eventData)
    {
        
    }
}