public interface IHeroState
{
    void EnterState(HeroController hero);
    void HandleInput();
    void Update();
    void ExitState();
}
