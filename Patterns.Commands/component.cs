using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Commands
{
    public class State
    {
        Dictionary<string, object> state = new Dictionary<string, object>();
        
        public void SetState(IDictionary<string, object> newState)
        {
            
        }

        public object GetState(string key)
        {
            object result = null;
            state.TryGetValue(key, out result);
            return result;            
        }

        public void SetState(string key, object obj)
        {
            
        }


    }

    public class Component
    {
        State state = new State();
        public object GetState(string key)
        {
            return state.GetState(key);
        }

        public void SetState(string key, object obj)
        {
            state.SetState(key, obj);
        }

        public virtual void Render() { }
    }
}
