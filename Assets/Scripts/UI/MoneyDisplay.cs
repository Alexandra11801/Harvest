using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Harvest.UI
{
    public class MoneyDisplay : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform coinsSpawnWorldPoint;
        [SerializeField] private GameObject canvas;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private float coinsFlyTime;
        [SerializeField] private GameObject icon;
        [SerializeField] private float iconDilation;
        [SerializeField] private float iconDilationTime;
        private int money;

        private void Start()
        {
            money = 0;
        }

        public void AddCoins(int coins)
        {
            var coinSpawnPoint = canvas.transform.InverseTransformPoint( 
                RectTransformUtility.WorldToScreenPoint(mainCamera, coinsSpawnWorldPoint.position));
            var coin = Instantiate(coinPrefab, canvas.transform);
            coin.transform.localPosition = coinSpawnPoint;
            coin.transform.DOMove(transform.position, coinsFlyTime).OnComplete(() => UpdateView(coins, coin));
        }

        public void UpdateView(int coins, GameObject coin)
        {
            Destroy(coin);
            money += coins;
            label.text = money.ToString();
            icon.transform.DOScale(iconDilation, iconDilationTime / 2).OnComplete(()
                => icon.transform.DOScale(1, iconDilationTime / 2));
        }
    }
}