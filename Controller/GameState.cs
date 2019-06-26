using Model.Map;

namespace Controller
{
    [System.Serializable]
    sealed class GameState
    {
        public Map CurrentLevel { get; set; }
        public int EnemiesLeft { get; set; }
        public int LevelNumber { get; set; }
        public int Lives { get; set; }
    }
}
