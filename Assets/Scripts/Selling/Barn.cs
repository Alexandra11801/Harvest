using System.Collections;
using DG.Tweening;
using Harvest.PlayerControl;
using Harvest.UI;
using UnityEngine;

namespace Harvest.Selling
{
    public class Barn : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private float stackTransportTime;
        [SerializeField] private float stackTransportDelay;
        [SerializeField] private int cost;
        [SerializeField] private MoneyDisplay moneyDisplay;

        public void StartUnloadingStacks()
        {
            StartCoroutine(nameof(UnloadStacks));
        }

        private IEnumerator UnloadStacks()
        {
            while (!player.WheatCollector.IsEmpty())
            {
                var stack = player.WheatCollector.GetLastStack();
                player.WheatCollector.RemoveStack(stack);
                stack.transform.DOMove(transform.position, stackTransportTime).OnComplete(() => SellStack(stack));
                yield return new WaitForSeconds(stackTransportDelay);
            }
            player.Go();
        }

        private void SellStack(GameObject stack)
        {
            Destroy(stack);
            moneyDisplay.AddCoins(cost);
        }
    }
}