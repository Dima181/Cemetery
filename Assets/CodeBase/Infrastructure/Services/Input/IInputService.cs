using Assets.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool IsAttackButtonUp();
    }
}
