using System;
using System.Collections;
using UnityEngine;

namespace Brick
{
    /// <summary>
    /// Script that handle the shock wave effect whenever a boom box is activated. Creates a sprite that will change it's opacity over time., before getting destroyed.
    /// </summary>
    public class BoomBoxShockWaveSpriteHandler : MonoBehaviour
    {
        
        private BallTrajModifierBrick _boomBox;

        [SerializeField]
        private Sprite spriteToUse;
        
        [Range(0f, 1f)]
        [SerializeField]
        private float buildUpTimePercent = 0.1f;
        
        [Range(0f, 1f)]
        [SerializeField]
        private float fadeOutTimePercent = 0.33f;
        
        [SerializeField]
        private float stayAfterDuration = 0.5f;
        
        private void Awake()
        {
            _boomBox = GetComponentInParent<BallTrajModifierBrick>();
        }

        private void Start()
        {
            _boomBox.OnActivated += CreateShockWave;
        }

        private void CreateShockWave(float duration)
        {
            var shockWave = new GameObject("ShockWaveEffect")
            {
                transform =
                {
                    position = transform.position,
                    localScale = Vector3.zero
                }
            };
            shockWave.transform.SetParent(transform);
            var spriteRenderer = shockWave.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = spriteToUse;
            spriteRenderer.sortingLayerName = "Bricks";
            spriteRenderer.sortingOrder = 1;
            
            
            StartCoroutine(ChangeOpacityAndDestroy(spriteRenderer, duration));
        }

        private IEnumerator ChangeOpacityAndDestroy(SpriteRenderer spriteRenderer, float duration)
        {
            float time = 0;
            Vector3 maxScale = Vector3.one * (_boomBox.Range);
            
            float buildUpTime = (duration + stayAfterDuration) * this.buildUpTimePercent;
            float fadeOutTime = (duration + stayAfterDuration) * this.fadeOutTimePercent;
            
            // Build up
            while (time < buildUpTime)
            {
                time += Time.deltaTime;
                var opacity = time / buildUpTime;
                spriteRenderer.transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, opacity);
                spriteRenderer.color = new Color(1, 1, 1, opacity);
                yield return null;
            }
            
            yield return new WaitForSeconds(duration - buildUpTime - fadeOutTime);
            
            while (time < (duration + stayAfterDuration))
            {
                time += Time.deltaTime;
                var opacity = 1 - (time - (duration - fadeOutTime)) / fadeOutTime;
                //scale down to 0.5f
                spriteRenderer.transform.localScale = Vector3.Lerp(Vector3.one/2, maxScale, opacity);
                spriteRenderer.color = new Color(1, 1, 1, opacity);
                yield return null;
            }
            
            Destroy(spriteRenderer.gameObject);
        }
    }
}