using System.Collections.Generic;
using DG.Tweening;
using Harvest.UI;
using UnityEngine;

namespace Harvest.PlayerControl
{
    public class PlayerWheatCollector : MonoBehaviour
    {
        [SerializeField] private int maxStackCount;
        [SerializeField] private Transform stacksBottom;
        [SerializeField] private GameObject stackExample;
        [SerializeField] private float collectingTime;
        [SerializeField] private WheatStacksDisplay stacksDisplay;
        private List<GameObject> stacks;
        private float stackThickness;

        public int StackCount => stacks.Count;
        public int MaxStackCount => maxStackCount;

        private void Start()
        {
            stacks = new List<GameObject>();
            stackThickness = stackExample.transform.localScale.y;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("WheatStack"))
            {
                var stack = other.gameObject;
                if (AddStack(stack))
                {
                    stack.GetComponent<SphereCollider>().enabled = false;
                }
            }
        }

        public bool AddStack(GameObject stack)
        {
            if (StackCount < maxStackCount)
            {
                stack.transform.parent = stacksBottom;
                stack.GetComponent<Rigidbody>().isKinematic = true;
                var targetLocalPosition = StackCount * stackThickness * Vector3.up;
                stack.transform.DOLocalMove(targetLocalPosition, collectingTime);
                stack.transform.DOLocalRotate(Vector3.zero, collectingTime);
                stacks.Add(stack);
                stacksDisplay.UpdateView();
                return true;
            }
            return false;
        }

        public void RemoveStack(GameObject stack)
        {
            stacks.Remove(stack);
            stack.transform.parent = null;
            stacksDisplay.UpdateView();
        }

        public bool IsEmpty()
        {
            return StackCount == 0;
        }
        
        public GameObject GetLastStack()
        {
            return stacks[stacks.Count - 1];
        }
    }
}