using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Attach this script to show a simple tooltip when hovered
    /// </summary>
    public class SimpleToolTipAttachment : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        [TextArea]
        private String textToDisplay;

        private GameObject _tooltip;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipScreenSpaceUI.Instance.ShowTooltip(textToDisplay);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipScreenSpaceUI.Instance.HideTooltip();
        }

        private void OnDisable()
        {
            if (TooltipScreenSpaceUI.Instance != null)
            {
                TooltipScreenSpaceUI.Instance.HideTooltip();
            }
        }
    }
}