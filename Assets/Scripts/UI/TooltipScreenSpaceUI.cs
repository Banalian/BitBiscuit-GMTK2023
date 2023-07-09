using System;
using UnityEngine;
using TMPro;

namespace UI
{
    public class TooltipScreenSpaceUI : MonoBehaviour
    {
        public static TooltipScreenSpaceUI Instance { get; private set; }
        
        [SerializeField]
        private RectTransform canvasRectTransform;
        
        private RectTransform _backgroundRectTransform;
        private TextMeshProUGUI _text;
        private RectTransform _rectTransform;
        
        [SerializeField]
        private Vector2 padding = new Vector2(8, 8);

        private Func<string> getTooltipTextFunc;
        private bool _active;

        private void Awake()
        {
            Instance = this;
            
            _backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
            _text = transform.Find("Background").Find("Text").GetComponent<TextMeshProUGUI>();
            _rectTransform = GetComponent<RectTransform>();
            
            HideTooltip();
        }
        
        private void SetText(string tooltipText)
        {
            _text.SetText(tooltipText);
            _text.ForceMeshUpdate();
            
            Vector2 textSize = _text.GetRenderedValues(false);
            Vector2 paddingSize = padding;
            paddingSize.x += 30;
            _backgroundRectTransform.sizeDelta = textSize + paddingSize;
        }

        private void Update()
        {
            if (!_active) return;
            
            SetText(getTooltipTextFunc?.Invoke() ?? "No tooltip text");

            Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
            if (anchoredPosition.x + _backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
            {
                anchoredPosition.x = canvasRectTransform.rect.width - _backgroundRectTransform.rect.width;
            }
            if (anchoredPosition.y + _backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
            {
                anchoredPosition.y = canvasRectTransform.rect.height - _backgroundRectTransform.rect.height;
            }
            _rectTransform.position = anchoredPosition;
        }
        
        public void ShowTooltip(string tooltipText)
        {
            ShowTooltip(() => tooltipText);
        }
        
        public void ShowTooltip(Func<string> getTooltipTextFunc)
        {
            this.getTooltipTextFunc = getTooltipTextFunc;
            gameObject.SetActive(true);
            SetText(getTooltipTextFunc());
            _active = true;
        }
        
        public void HideTooltip()
        {
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }
            getTooltipTextFunc = null;
            _active = false;
        }
    }
}