using UnityEngine;

namespace Grid
{
    public class Highlightable : MonoBehaviour
    {
    
        void OnMouseEnter()
        {
            if (isActiveAndEnabled)
            {
                ElementManager.Instance.Highlight(this.gameObject);
            }
        }

        void OnMouseExit()
        {
            if (isActiveAndEnabled)
            {
                ElementManager.Instance.UnHighLight();
            }
        }
    }
}

