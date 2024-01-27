
namespace GOAP
{
    [System.Serializable]
    public class WorldState
    {
        public string Key;
        public int Value;

        public WorldState(string key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}
