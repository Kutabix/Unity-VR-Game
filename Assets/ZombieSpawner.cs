using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunZombieTrigger : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnPoint;
    public GameObject player;

    private XRGrabInteractable grab;
    private bool hasSpawned = false;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrabbed);
    }

    private void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (!hasSpawned) 
        {
            SpawnZombie();
            hasSpawned = true; 
        }
    }

    void SpawnZombie()
    {
        if (zombiePrefab != null && spawnPoint != null)
        {
            GameObject zombieInstance = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

            ZombieFollowPlayer zombieScript = zombieInstance.GetComponent<ZombieFollowPlayer>();
            if (zombieScript != null)
            {
                zombieScript.target = player.transform;
            }
        }
    }
}
