using System.Collections.Generic;
using Brick;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ShopBrickToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private ABrick brickToDisplay;

        private GameObject _tooltip;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipScreenSpaceUI.Instance.ShowTooltip(GenerateTooltipText);
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

        /// <summary>
        /// Using info from the brick, generates the text to display in the tooltip
        /// </summary>
        /// <returns>A list of sentences representing the state of the brick</returns>
        private string GenerateTooltipText()
        {
            return brickToDisplay.GetDescription();
        }
    }
}