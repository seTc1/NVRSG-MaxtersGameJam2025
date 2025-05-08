using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public GameEvent_Manager _gameEventManager;
    public override void InstallBindings()
    {
        Container.Bind<GameEvent_Manager>().FromInstance(_gameEventManager).AsSingle();
    }
}