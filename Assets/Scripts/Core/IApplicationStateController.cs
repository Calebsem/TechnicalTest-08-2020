public interface IApplicationStateController
{
    bool Done { get; }
    void Begin(EntityManager manager);
    void End();
}
