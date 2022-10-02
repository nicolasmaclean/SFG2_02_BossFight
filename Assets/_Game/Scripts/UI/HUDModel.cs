using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.UI;
using UnityEngine;

namespace Game
{
    public class HUDModel : Singleton<HUDModel>
    {
        [SerializeField]
        Transform _healthParent;
        
        [SerializeField]
        HealthBar _healthBarPrefab;

        [SerializeField]
        float _halfWidth = 600;

        List<HealthBar> _healthBars = new();
        public HealthBar AddHealthBar(string title)
        {
            HealthBar healthBar = Instantiate(_healthBarPrefab, _healthParent);
            healthBar.TargetName = title;
            
            if (_healthBars.Count == 1)
            {
                foreach (var bar in new[] { _healthBars[0], healthBar })
                {
                    var t = bar.GetComponent<RectTransform>();
                    Vector2 size = t.sizeDelta;
                    
                    size.x = _halfWidth;
                    t.sizeDelta = size;
                }

                healthBar.LeftAligned = false;
            }
            
            _healthBars.Add(healthBar);
            return healthBar;
        }

        public void Remove(HealthBar healthBar)
        {
            var bars = _healthBars.Count == 2 ?  _healthBars.ToArray() : new[] { healthBar };
            foreach (var bar in bars)
            {
                var t = bar.transform;
                if (t.parent != _healthParent) continue;
                
                t.SetParent(t.parent.parent, worldPositionStays: true);
            }
            
            Destroy(healthBar.gameObject, healthBar.LerpLength);
            _healthBars.Remove(healthBar);
        }
    }
}
