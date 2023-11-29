using Managers.Player_Handler;
using UnityEngine;
using Zenject;

    public class PlayeHandlerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHandler playerHandler;
        public override void InstallBindings()
        {
            var inputManagerInstance = Container.InstantiatePrefabForComponent<PlayerHandler>(playerHandler);
            DontDestroyOnLoad(inputManagerInstance);
            Container.Bind<PlayerHandler>().FromInstance(inputManagerInstance).AsSingle().NonLazy();
        }
    }