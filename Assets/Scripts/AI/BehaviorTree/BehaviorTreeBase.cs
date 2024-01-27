
using UnityEngine;

namespace ProjectBarde.BehaviorTree
{
    public class BehaviorTreeBase : MonoBehaviour
    {
        private Node root = null;

        /// <summary>
        /// Starts the setup the Tree
        /// </summary>
        public void Start()
        {
            root = SetupTree();
        }

        /// <summary>
        /// Call this in Update Funktion in Unity
        /// Evaluats the Tree
        /// </summary>
        public void UpdateTree()
        {
            if (root != null)
            {
                root.Evaluate();
            }
        }

        protected virtual Node SetupTree() { return root; }
    }

}
