using System.Collections;
using DG.Tweening;
using Harvest.Harvesting;
using Harvest.Selling;
using UnityEngine;

namespace Harvest.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerHarvester harvester;
        [SerializeField] private PlayerWheatCollector wheatCollector;
        [SerializeField] private Transform character;
        [SerializeField] private Animator animator;
        [SerializeField] private float walkingSpeed;
        [SerializeField] private float rotationTime;
        private float speed;
        private Vector3 walkingDirection;
        private bool isWalking;

        public PlayerWheatCollector WheatCollector => wheatCollector;

        public Vector3 WalkingDirection
        {
            get => walkingDirection;
            set => walkingDirection = value;
        }

        private void Start()
        {
            walkingDirection = transform.forward;
            speed = walkingSpeed;
            Go();
        }

        public void Go()
        {
            animator.SetTrigger("Go");
            isWalking = true;
            StartCoroutine(nameof(WalkRoutine));
        }

        private void Stop()
        {
            isWalking = false;
            StopCoroutine(nameof(WalkRoutine));
        }

        private IEnumerator WalkRoutine()
        {
            while (true)
            {
                yield return null;
                TurnTo(transform.position + walkingDirection);
                var targetPosition = transform.position + walkingDirection * speed;
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            }
        }

        public void TurnTo(Vector3 position)
        {
            character.DOLookAt(position, rotationTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isWalking)
            {
                if (other.TryGetComponent<WheatBed>(out var bed) && Vector3.Angle(character.forward, 
                        other.transform.position - transform.position) < 60 && bed.CanBeHarvested)
                {
                    Stop();
                    harvester.Harvest(bed);
                }
                else if (other.TryGetComponent<Barn>(out var barn) && !wheatCollector.IsEmpty())
                {
                    Stop();
                    animator.SetTrigger("Idle");
                    barn.StartUnloadingStacks();
                }
            }
        }
    }
}