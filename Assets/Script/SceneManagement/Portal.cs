using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


namespace Dreams.SceneManagement
{
    public enum DestinationIndentified
    {
        A,
        B,
        C,
        D,
        E,
    }
    
    public class Portal : MonoBehaviour
    {

        [SerializeField] private int sceneIndex = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIndentified destination;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneIndex < 0)
            {
                Debug.LogError($"Scene To Load not set");
                yield break;
            }
            
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneIndex);;
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);


            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }

            return null;
        }

        private void UpdatePlayer(Portal portal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(portal.spawnPoint.position);
            player.transform.rotation = portal.spawnPoint.rotation;
        }
        
    }
}