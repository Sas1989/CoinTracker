using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions
{
    internal static class ActionFactory
    {
        private static readonly Dictionary<Type, object> _actions = new Dictionary<Type, object>();

        public static T GetAction<T>() where T : ICleanable, new()
        {
            var action = typeof(T);

            if (!_actions.ContainsKey(action))
            {
                _actions.Add(action, new T());
            }
            return (T)_actions[action];
        }

        public static async Task ClearAllAsync()
        {
            var task = new List<Task>();
            foreach (ICleanable action in _actions.Values)
            {
                task.Add(action.Clean());
            }
            await Task.WhenAll(task);
        }
    }
}