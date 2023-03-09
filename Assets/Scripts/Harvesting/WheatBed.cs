using System.Collections;
using Harvest.PlayerControl;
using UnityEngine;

namespace Harvest.Harvesting
{
    public class WheatBed : MonoBehaviour
    {
        [SerializeField] private GameObject grownWheat;
        [SerializeField] private GameObject cutWheat;
        [SerializeField] private GameObject wheatStack;
        [SerializeField] private Transform stackSpawnPoint;
        [SerializeField] private float wheatGrowthTime;
        [SerializeField] private SphereCollider trigger;
        private PlayerWheatCollector wheatCollector;

        private void Start()
        {
            wheatCollector = FindObjectOfType<PlayerWheatCollector>();
        }

        public void Harvest()
        {
            grownWheat.SetActive(false);
            cutWheat.SetActive(true);
            trigger.enabled = false;
            var stack = Instantiate(wheatStack, stackSpawnPoint.position, Quaternion.identity);
            Physics.IgnoreCollision(stack.GetComponent<BoxCollider>(), wheatCollector.GetComponent<BoxCollider>());
            StartCoroutine(nameof(GrowWheat));
        }

        private IEnumerator GrowWheat()
        {
            yield return new WaitForSeconds(wheatGrowthTime);
            grownWheat.SetActive(true);
            cutWheat.SetActive(false);
            trigger.enabled = true;
        }
    }
}