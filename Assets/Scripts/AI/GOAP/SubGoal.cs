using System.Collections.Generic;

namespace GOAP
{
    [System.Serializable]
    public class SubGoal
    {
        public Dictionary<string, int> Goals;
        public bool Remove; // Should the goals get removed after completion

        public SubGoal(string goalName, int goalValue, bool remove)
        {
            Goals = new Dictionary<string, int>
            {
                { goalName, goalValue }
            };
            Remove = remove;
        }
    }
}
