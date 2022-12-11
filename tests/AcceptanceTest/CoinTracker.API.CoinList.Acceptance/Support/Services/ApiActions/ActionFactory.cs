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
        private static readonly Dictionary<Type, IApiActionBase> _actions = new Dictionary<Type, IApiActionBase>();

        public static T GetAction<T>() where T : IApiActionBase, new()
        {
            var action = typeof(T);

            if (!_actions.ContainsKey(action))
            {
                _actions.Add(action, new T());
            }
            return (T)_actions[action];
        }

        public static List<IApiActionBase> GetAll()
        {
            return _actions.Values.ToList();
        }
    }
}