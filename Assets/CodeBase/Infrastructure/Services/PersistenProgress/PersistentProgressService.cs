using Assets.CodeBase.Data;

namespace Assets.CodeBase.Infrastructure.Services.PersistenProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}
