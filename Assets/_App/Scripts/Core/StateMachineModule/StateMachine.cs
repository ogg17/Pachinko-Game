using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VgGames.Core.CustomDebug;

namespace VgGames.Core.StateMachineModule
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        private IState _currentState;

        public void Add<T>(T state) where T : IState
        {
            var key = typeof(T);
            if (_states.ContainsKey(key))
            {
                Debug.Log($"{key} is already added!");
                return;
            }
            Debug.Log($"State Machine: Add State - {key}");
            _states.Add(key, state);
        }

        public async UniTask SetState<T>() where T : IState
        {
            var key = typeof(T);
            if (_states.TryGetValue(key, out var state))
            {
                if (_currentState == state) return;
                if (_currentState != null) 
                    await _currentState.Exit();
                Debug.Log($"State Machine: State - {_currentState?.GetType()} -> State - {key}");
                _currentState = state;
                await state.Enter();
            }
            else throw new Exception($"State {key} do not exist!");
        }

        public void Tick()
        {
            if(_currentState is IUpdateState updateState) updateState.StateUpdate();
        }
    }
}