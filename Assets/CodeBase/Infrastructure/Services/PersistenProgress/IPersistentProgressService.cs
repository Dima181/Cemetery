using Assets.CodeBase.Data;

namespace Assets.CodeBase.Infrastructure.Services.PersistenProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}