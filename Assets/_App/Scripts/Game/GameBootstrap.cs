using Cysharp.Threading.Tasks;
using UnityEngine;
using VgGames.Core.EventBusModule;
using VgGames.Core.InjectModule;
using VgGames.Core.MonoBehaviours;
using VgGames.Core.StateMachineModule;
using VgGames.Game.Balls;
using VgGames.Game.Obstacles;
using VgGames.Game.RuntimeData;
using VgGames.Game.Signals;
using VgGames.Game.Sound;
using VgGames.Game.States;
using VgGames.Game.UI.Buttons;
using VgGames.Game.UI.GameText;
using VgGames.Game.UI.Menus;

namespace VgGames.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        private readonly StateMachine _stateMachine = new();
        private readonly EventBus _eventBus = new();
        private SettingsData _settingsData;
        private LocalizationData _localization;
        private BallsData _ballsData;
        private CoinsData _coinsData;
        private TimerData _timerData;
        private readonly MonoBehavioursContainer _monoBehaviours = new();
        private readonly MenusStateMachine _menusStateMachine = new();

        private void Start()
        {
            Application.targetFrameRate = 60;
            _settingsData = new SettingsData();
            _localization = new LocalizationData();
            _ballsData = new BallsData();
            _coinsData = new CoinsData();
            _timerData = new TimerData();
            AddMonoBehaviours();
            AddInjects();
            CreateStates();
        }

        private void CreateStates()
        {
            _stateMachine.Add(new InitState(_stateMachine, _menusStateMachine, _monoBehaviours));
            _stateMachine.Add(new GameState(_stateMachine, _monoBehaviours, _eventBus, _menusStateMachine, _coinsData));

            _stateMachine.SetState<InitState>().Forget();
        }

        private void AddInjects()
        {
            InjectSystem.Clear();
            InjectSystem.GetMonoBehaviour();

            InjectSystem.Add(_eventBus);
            InjectSystem.Add(_settingsData);
            InjectSystem.Add(_localization);
            InjectSystem.Add(_ballsData);
            InjectSystem.Add(_coinsData);
            InjectSystem.Add(_timerData);
            InjectSystem.Add(_monoBehaviours);
            InjectSystem.Add(_menusStateMachine);

            InjectSystem.Inject(_localization);
        }

        private void AddMonoBehaviours()
        {
            _monoBehaviours.Add<Menu>();
            _monoBehaviours.Add<BallsManager>();
            _monoBehaviours.Add<PushButton>();
            _monoBehaviours.Add<BaseText>();
            _monoBehaviours.Add<ObstacleMove>();
            _monoBehaviours.Add<SoundPlayer>();
        }

        private void LateUpdate()
        {
            _stateMachine.Tick();
        }

        private void OnApplicationQuit()
        {
            _eventBus.Invoke(new EndSignal());
        }

        private void OnDestroy()
        {
            _eventBus.Invoke(new EndSignal());
        }
    }
}