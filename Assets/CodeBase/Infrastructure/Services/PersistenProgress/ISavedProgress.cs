using Assets.CodeBase.Data;

namespace Assets.CodeBase.Infrastructure.Services.PersistenProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}