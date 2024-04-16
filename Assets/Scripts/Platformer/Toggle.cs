using UnityEngine;
using System.Collections;

public class Toggle : MonoBehaviour
{

    [TooltipAttribute("The Game Object to toggle active/inactive.")]
    public GameObject target;       // The GameObject to toggle active/inactive.

    
    public void ToggleActive()
    {
        // if we have a reference to a target game object...
        if (target != null)
        {
            // ...I could play an animation, a sound, etc...

            // ...toggle the target game object.
            target.SetActive(!target.activeSelf); // When we call SetActive, set the state to the flip from the current active state of the target.
        }
    }
}
