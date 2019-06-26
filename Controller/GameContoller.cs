using Model.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;

namespace Controller
{
    public sealed class GameContoller
    {
        public const string StandardExtension = "gs";

        #region Настройки к уровням сложности
        private Difficulties difficulty;

        private static Dictionary<Difficulties, int> initialLivesCount = new Dictionary<Difficulties, int>
        {
            { Difficulties.Easy, 5 },
            { Difficulties.Medium, 3 },
            { Difficulties.Hard, 1 }
        };

        private static Dictionary<Difficulties, int> maxEnemiesCount = new Dictionary<Difficulties, int>
        {
            { Difficulties.Easy, 15 },
            { Difficulties.Medium, 20 },
            { Difficulties.Hard, 30 }
        };
        #endregion

        private GameState gameState;
        private Timer gameTimer;

        public GameContoller(Difficulties difficulty)
        {
            this.difficulty = difficulty;
            gameState = new GameState();
            gameTimer = new Timer(100);
            gameTimer.Elapsed += Act;
        }

        public void Start(Map firstLevel)
        {
            gameState.LevelNumber = 0;
            gameState.Lives = initialLivesCount[difficulty];
            NextLevel(firstLevel);
        }

        public void NextLevel(Map nextLevel)
        {
            gameTimer.Stop();
            gameState.EnemiesLeft = maxEnemiesCount[difficulty];
            gameState.LevelNumber++;
            gameState.CurrentLevel = nextLevel;
            gameTimer.Start();
        }

        public void ChangePauseState()
        {
            Action action = gameTimer.Enabled ? (Action)gameTimer.Stop : gameTimer.Start;
            if (gameTimer.Enabled)
                gameTimer.Stop();
            else gameTimer.Start();
        }

        private void Act(object sender, ElapsedEventArgs e)
        {
            
        }

        #region Загрузка/сохранение состояния игры
        public bool LoadGame(string saveName)
        {
            if (!File.Exists(saveName)) return false;
            using (var file = new FileStream(saveName, FileMode.Open, FileAccess.Read))
                gameState = (GameState)new BinaryFormatter().Deserialize(file);
            return true;
        }

        public void SaveGame(string saveName)
        {
            using (var file = new FileStream($"{saveName}.{StandardExtension}", FileMode.OpenOrCreate, FileAccess.Write))
                new BinaryFormatter().Serialize(file, this);
        }
        #endregion
    }
}
