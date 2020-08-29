public interface IWizardStep
{
    bool Done { get; }
    void Begin(WizardController controller);
    void End();
}
