using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Commands
{
    public class State
    {
        private readonly Dictionary<string, object> _state = new Dictionary<string, object>();
        private readonly Stack<Dictionary<string, object>> _states = new Stack<Dictionary<string, object>>();
        private readonly Stack<Dictionary<string, object>> _redoStates = new Stack<Dictionary<string, object>>();
        
        public void SetState(IDictionary<string, object> newState)
        {
            var step = _state
                .Where(x => newState.ContainsKey(x.Key))
                .ToDictionary(x => x.Key, x=> x.Value);
            _redoStates.Clear();
            _states.Push(step);

            newState.Select(x => x).ToList().ForEach(x => _state[x.Key] = x.Value);
        }

        public object GetState(string key)
        {
            object result = null;
            _state.TryGetValue(key, out result);
            return result;            
        }

        public void Undo()
        {
            if (_states.Count == 0) return;
            var stepToReproduce = _states.Pop();

            var stepToStore = _state
                .Where(x => stepToReproduce.ContainsKey(x.Key))
                .ToDictionary(x => x.Key, x => x.Value);
            _redoStates.Push(stepToStore);

            stepToReproduce.Select(x => x).ToList().ForEach(x => _state[x.Key] = x.Value);
        }

        public void Redo()
        {
            if (_redoStates.Count == 0) return;
            var stepToRevert = _redoStates.Pop();

            var stepToStore = _state
                .Where(x => stepToRevert.ContainsKey(x.Key))
                .ToDictionary(x => x.Key, x => x.Value);
            _states.Push(stepToStore);

            stepToRevert.Select(x => x).ToList().ForEach(x => _state[x.Key] = x.Value);
        }

    }

    public class Component
    {
        private readonly State _state = new State();
        public object GetState(string key)
        {
            return _state.GetState(key);
        }

        public void SetState(IDictionary<string, object> newState)
        {
            _state.SetState(newState);
        }

        public virtual void Render() { }
        public void Undo()
        {
            _state.Undo();
        }

        public void Redo()
        {
            _state.Redo();
        }
    }
}
