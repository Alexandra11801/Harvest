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
        private bool canBeHarvested;
        private PlayerWheatCollector wheatCollector;

        public bool CanBeHarvested => canBeHarvested;
        
        private void Start()
        {
            canBeHarvested = true;
            wheatCollector = FindObjectOfType<PlayerWheatCollector>();
        }

        public void Harvest()
        {
            canBeHarvested = false;
            grownWheat.SetActive(false);
            cutWheat.SetActive(true);
            var stack = Instantiate(wheatStack, stackSpawnPoint.position, Quaternion.identity);
            Physics.IgnoreCollision(stack.GetComponent<BoxCollider>(), wheatCollector.GetComponent<BoxCollider>());
            StartCoroutine(nameof(GrowWheat));
        }

        private IEnumerator GrowWheat()
        {
            yield return new WaitForSeconds(wheatGrowthTime);
            canBeHarvested = true;
            grownWheat.SetActive(true);
            cutWheat.SetActive(false);
        }
    }
}