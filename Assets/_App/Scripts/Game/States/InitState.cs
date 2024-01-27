using System.Linq;
using Cysharp.Threading.Tasks;
using VgGames.Core.Extra;
using VgGames.Core.InjectModule;
using VgGames.Core.MonoBehaviours;
using VgGames.Core.StateMachineModule;
using VgGames.Game.UI.Menus;

namespace VgGames.Game.States
{
    public class InitState : IState
    {
        private readonly MonoBehavioursContainer _monoBehaviours;
        private readonly StateMachine _stateMachine;
        private readonly MenusStateMachine _menusStateMachine;

        public InitState(StateMachine stateMachine, MenusStateMachine menusStateMachine,
            MonoBehavioursContainer monoBehavioursContainer)
        {
            _stateMachine = stateMachine;
            _menusStateMachine = menusStateMachine;
            _monoBehaviours = monoBehavioursContainer;
            
            _stateMachine.Add(this);
        }
        
        public async UniTask Enter()
        {
            InjectSystem.InjectAllMono();
            CallInit();
            DisableMenus();
            
            await _menusStateMachine.SetMenu(MenuType.Logo);
            _stateMachine.SetState<GameState>().Forget();
        }

        private void DisableMenus()
        {
            _monoBehaviours.GetAll<Menu>().ForEach(m => m.ForceDisable());
        }

        private void CallInit()
        {
            foreach (var init in _monoBehaviours.GetAll().OfType<IInit>()) init.Init();
        }
    }
}