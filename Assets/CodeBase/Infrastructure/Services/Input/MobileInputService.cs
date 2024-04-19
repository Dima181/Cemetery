using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}
