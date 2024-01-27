
namespace GOAP
{
    public sealed class World
    {
        public static readonly World Instance = new World();

        private static WorldStates _states;

        static World()
        {
            _states = new WorldStates();
        }

        private World() { }

        public WorldStates GetWorld()
        {
            return _states;
        }
    }
}
