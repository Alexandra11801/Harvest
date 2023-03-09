using System;
using Harvest.PlayerControl;
using TMPro;
using UnityEngine;

namespace Harvest.UI
{
    public class WheatStacksDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private PlayerWheatCollector wheatCollector;

        public void UpdateView()
        {
            label.text = String.Format("{0}/{1}", wheatCollector.StackCount, wheatCollector.MaxStackCount);
        }
    }
}