using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class ClickableThingy : MonoBehaviour, IMixedRealityInputHandler
{
    public void OnInputUp(InputEventData eventData)
    {
        GetComponent<MeshRenderer>().material.color = Color.green;

    }

    public void OnInputDown(InputEventData eventData)
    {
        var now = GetComponent<MeshRenderer>().material.color;
        
        

    }
}