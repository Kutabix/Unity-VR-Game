using UnityEngine;

public class ToggleLightsGroup : MonoBehaviour
{
    public Light[] allLights;
    private bool lightsOn = true;

    public void ToggleAllLights()
    {
        lightsOn = !lightsOn;

        foreach (Light light in allLights)
        {
            if (light != null)
                light.enabled = lightsOn;
        }
    }
}
