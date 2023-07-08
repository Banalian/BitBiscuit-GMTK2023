using System.Collections.Generic;
using UnityEngine;

namespace Brick
{
    /// <summary>
    /// Script to change the brick's sprite whenever it changes level or damage
    /// </summary>
    public class BrickSpriteChanger : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();
        
        private SpriteRenderer _spriteRenderer;
        
        private ABrick _brick;
        
        private HealthManager _healthManager;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _brick = GetComponentInParent<ABrick>();
            _healthManager = GetComponentInParent<HealthManager>();
        }

        private void Start()
        {
            _brick.OnBrickLevelChanged += ChangeSpriteFromLevel;
            _brick.OnBrickDamaged += ChangeSpriteFromDamage;
        }

        private void ChangeSpriteFromDamage(GameObject brick, int damage, int newHealth)
        {
            if (typeof(BasicBrick) != _brick.GetType()) return;
            if (newHealth > 0)
            {
                ChangeSprite(newHealth - 1);
            }
        }

        private void ChangeSpriteFromLevel(GameObject brick, int newLevel)
        {
            if (typeof(BasicBrick) == _brick.GetType())
            {
                ChangeSprite(_healthManager.Health - 1);
            }
            else if(typeof(GoldBrick) == _brick.GetType())
            {
                ChangeSprite(newLevel - 1);
            }
        }
        
        private void ChangeSprite(int index)
        {
            _spriteRenderer.sprite = sprites[index];
        }

    }
}