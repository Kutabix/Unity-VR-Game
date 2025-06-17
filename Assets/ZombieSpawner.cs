using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class GunZombieTrigger : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnPoint;
    public GameObject player;

    private XRGrabInteractable grab;
    private bool isSpawning = false;

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
        if (!isSpawning)
        {
            StartCoroutine(SpawnZombiesLoop());
            isSpawning = true;
        }
    }

    private IEnumerator SpawnZombiesLoop()
    {
        while (true) 
        {
            SpawnZombie();

            // losowy czas od 6 do 12 sekund
            float waitTime = Random.Range(3f, 10f);
            yield return new WaitForSeconds(waitTime);
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
