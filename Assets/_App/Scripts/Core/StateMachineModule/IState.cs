using Cysharp.Threading.Tasks;

namespace VgGames.Core.StateMachineModule
{
    public interface IState
    {
        public UniTask Enter() => UniTask.CompletedTask;
        public UniTask Exit() => UniTask.CompletedTask;
    }

    public interface IUpdateState : IState
    {
        public void StateUpdate();
    }
}