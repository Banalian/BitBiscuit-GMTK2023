using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grid
{
    public class ElementManager : MonoBehaviour
    {
        [Tooltip("The element currently held by the cursor (the last clicked in shop)")]
        [SerializeField] 
        private GameObject selectedElement;
        
        private GameObject _highlightGO;

        [SerializeField]
        private GameObject defaultElement;
    
        public static ElementManager Instance;

        private void Awake() 
        { 
            //If there is another ElementManager instance, and it's not me, delete myself.
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            //If not, i'm the only ElementManager.
            else 
            { 
                Instance = this; 
            }

            selectedElement = defaultElement;
            InitHighlight();
        }
        
        private void InitHighlight()
        {
            //instantiate a new gameObject and give it the correct sprite
            _highlightGO = new GameObject();
            var sr = _highlightGO.AddComponent<SpriteRenderer>();
            sr.sprite = selectedElement.GetComponentInChildren<SpriteRenderer>().sprite;
            var tempColor = sr.color;
            tempColor.a = .5f;
            sr.color = tempColor;
            UnHighLight();
        }

        /// <summary>
        /// Sets the highlights sprite as the current selected elements sprite
        /// /// </summary>
        private void UpdateHighlight()
        {

            _highlightGO.transform.localScale = selectedElement.transform.localScale;//make the highlight the size of the element
            var sr = _highlightGO.GetComponent<SpriteRenderer>();
            sr.sprite = selectedElement.GetComponentInChildren<SpriteRenderer>().sprite;
            var tempColor = sr.color;
            tempColor.a = .5f; //highlight is transparent
            sr.sortingOrder = 1; //highlight is drawn on top of other sprites in the layer
            sr.color = tempColor;
        }

        /// <summary>
        /// Sets the selected element (element that is supposed to be placed)
        /// </summary>
        public void SetSelectedElement(GameObject newSelection)
        {
            this.selectedElement = newSelection;
            UpdateHighlight();
        }

        //TODO set the name of the brick so that it has its coordinate relative to the grid
        private GameObject ReplaceElementWith(GameObject objToReplace, GameObject replacingObject)
        {
            GameObject placedElement = Instantiate(replacingObject, objToReplace.transform.position, _highlightGO.transform.rotation, this.transform);
            Destroy(objToReplace);
            return placedElement;
        }
    
        public void ReplaceElement(GameObject objToReplace)
        {
            if (selectedElement != null)
            {
                GameObject placedElement = ReplaceElementWith(objToReplace, selectedElement);
            }
        }

        public void DeleteElement(GameObject objToDel)
        {
            ReplaceElementWith(objToDel, defaultElement);
        }

        public void SetSelected(GameObject newElement)
        {
            selectedElement = newElement;
        }
    
        public void ResetSelected()
        {
            selectedElement = null;
        }
    

        public void Highlight(GameObject objToHighlight)
        {
            _highlightGO.SetActive(true);
            _highlightGO.transform.position = objToHighlight.transform.position;
            UpdateHighlight();
        }

        public void UnHighLight()
        {
            _highlightGO.SetActive(false);
        }
    }
}