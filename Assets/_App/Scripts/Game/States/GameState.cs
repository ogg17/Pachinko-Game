using System.Linq;
using Cysharp.Threading.Tasks;
using VgGames.Core.EventBusModule;
using VgGames.Core.Extra.GameState;
using VgGames.Core.MonoBehaviours;
using VgGames.Core.StateMachineModule;
using VgGames.Game.GiftTimer;
using VgGames.Game.RuntimeData;
using VgGames.Game.Signals;
using VgGames.Game.UI.Menus;

namespace VgGames.Game.States
{
    public class GameState : IState
    {
        private readonly MonoBehavioursContainer _monoBehaviours;
        private readonly EventBus _eventBus;
        private readonly MenusStateMachine _menusStateMachine;
        private readonly CoinsData _coinsData;
        private readonly Timer _timer;
        
        public GameState(StateMachine stateMachine, MonoBehavioursContainer monoBehavioursContainer,
            EventBus eventBus, MenusStateMachine menusStateMachine, CoinsData coinsData)
        {
            _monoBehaviours = monoBehavioursContainer;
            _eventBus = eventBus;
            _menusStateMachine = menusStateMachine;
            _coinsData = coinsData;

            stateMachine.Add(this);

            _timer = new Timer();
        }

        public async UniTask Enter()
        {
            CallActivate();

            await _menusStateMachine.SetMenu(MenuType.GameUI);
            
            _eventBus.Add<EndSignal>(OnEnd);
            _eventBus.Add<BallEndCollisionSignal>(OnBall);
            
            _timer.GameActivate();
        }

        private void OnBall(BallEndCollisionSignal signal)
        {
            _coinsData.Coins.Value += 50;
        }

        private void OnEnd(EndSignal signal) => Exit().Forget();

        private void CallActivate()
        {
            foreach (var init in _monoBehaviours.GetAll().OfType<IGameActivate>()) init.GameActivate();
        }

        private void CallDeactivate()
        {
            foreach (var init in _monoBehaviours.GetAll().OfType<IGameDeactivate>()) init.GameDeactivate();
        }

        public async UniTask Exit()
        {
            _eventBus.Remove<EndSignal>(OnEnd);

            CallDeactivate();
            
            _timer.GameDeactivate();
            await UniTask.CompletedTask;
        }
    }
}