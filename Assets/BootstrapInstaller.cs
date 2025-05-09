using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public GameEvent_Manager _gameEventManager;
    public PlayerStats_Manager _playerStats;
    public override void InstallBindings()
    {
        Container.Bind<GameEvent_Manager>().FromInstance(_gameEventManager).AsSingle();
        Container.Bind<PlayerStats_Manager>().FromInstance(_playerStats).AsSingle();
    }
}