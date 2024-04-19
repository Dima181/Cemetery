using Assets.CodeBase.Hero;
using Assets.CodeBase.Logic;
using UnityEngine;

namespace Assets.CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HPBar HPBar;

        private IHealth _health;

        public void Construct(IHealth hero)
        {
            _health = hero;

            _health.HealthChanged += UpdateHPBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        /*private void OnDestroy() =>
            _health.HealthChanged -= UpdateHPBar;*/

        private void UpdateHPBar() => 
            HPBar.SetValue(_health.Current, _health.Max);

    }
}