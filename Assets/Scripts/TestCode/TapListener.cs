using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
public class TapListener : MonoBehaviour, IMixedRealityGestureHandler
{
    [SerializeField]
    private MixedRealityInputAction selectAction; // You'll need to set this in the Inspector to Select

    public void OnGestureCompleted(InputEventData eventData)
    {
        Debug.Log("testest");
        if (eventData.MixedRealityInputAction == selectAction)
        {
            Debug.Log("Tap!");
        }
    }

    public void OnGestureStarted(InputEventData eventData) { }
    public void OnGestureUpdated(InputEventData eventData) { }
    public void OnGestureCanceled(InputEventData eventData) { }
}