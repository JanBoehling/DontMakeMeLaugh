using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public abstract class Action : MonoBehaviour
    {
        [SerializeField]
        private string _actionName;
        [SerializeField]
        private WorldState[] _preCons;
        [SerializeField]
        private WorldState[] _afterEffs;

        public float Cost;
        public float Duration;
        public bool Running;
        public Dictionary<string, int> AfterEffects;
        public WorldStates Beliefs;

        private Dictionary<string, int> _preConditions;

        private void Awake()
        {
            AfterEffects = new Dictionary<string, int>();
            _preConditions = new Dictionary<string, int>();
            Beliefs = new WorldStates();

            foreach (WorldState preCons in _preCons)
            {
                _preConditions.Add(preCons.Key, preCons.Value);
            }

            foreach (WorldState afterEffs in _afterEffs)
            {
                AfterEffects.Add(afterEffs.Key, afterEffs.Value);
            }
        }

        public abstract bool PrePerform();
        public abstract bool PostPerform();

        public virtual bool IsAchievable()
        {
            return true;
        }

        public virtual bool IsAchievableGivenPreCons(Dictionary<string, int> cons)
        {
            foreach (var preCon in _preConditions)
            {
                if (!cons.ContainsKey(preCon.Key))
                    return false;
                if (cons[preCon.Key] != preCon.Value)
                    return false;
            }

            return true;
        }
    }
}
