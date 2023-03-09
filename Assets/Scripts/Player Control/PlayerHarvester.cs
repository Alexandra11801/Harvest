using Harvest.Harvesting;
using UnityEngine;

namespace Harvest.PlayerControl
{
    public class PlayerHarvester : MonoBehaviour
    {
        [SerializeField] private PlayerController controller;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject scythe;
        private WheatBed harvestedBed;

        public void Harvest(WheatBed bed)
        {
            harvestedBed = bed;
            controller.TurnTo(harvestedBed.transform.position);
            animator.SetTrigger("Harvest");
            scythe.SetActive(true);
        }

        public void CutWheat()
        {
            harvestedBed.Harvest();
        }

        public void EndHarvesting()
        {
            scythe.SetActive(false);
            controller.Go();
        }
    }
}