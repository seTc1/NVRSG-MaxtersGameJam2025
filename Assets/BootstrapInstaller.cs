using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public GameEvent_Manager _gameEventManager;
    public PlayerStats_Manager _playerStats;
    public PlayerInteraction_Script _playerInteractionScript;
    public DiscetHolderBar_Controller _discetHolderBar;
    public EffectSpawner _effectSpawner;
    public GameState_Manager _gameStateManager;
    public override void InstallBindings()
    {
        Container.Bind<EffectSpawner>().FromInstance(_effectSpawner).AsSingle();
        Container.Bind<DiscetHolderBar_Controller>().FromInstance(_discetHolderBar).AsSingle();
        Container.Bind<PlayerInteraction_Script>().FromInstance(_playerInteractionScript).AsSingle();
        Container.Bind<GameEvent_Manager>().FromInstance(_gameEventManager).AsSingle();
        Container.Bind<PlayerStats_Manager>().FromInstance(_playerStats).AsSingle();
        Container.Bind<GameState_Manager>().FromInstance(_gameStateManager).AsSingle();
    }
}