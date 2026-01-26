using UnityEngine;

namespace MineMogulModMenu
{
    public class GameUtilities : MonoBehaviour
    {
        private static PlayerController _localPlayerController;
        private static EconomyManager _economyManager;
        private static QuestManager _questManager;
        private static DepositBox _depositBox;
        
        public static PlayerController LocalPlayerController
        {
            get
            {
                if (_localPlayerController == null)
                    _localPlayerController = Object.FindFirstObjectByType<PlayerController>();
                return _localPlayerController;
            }
        }
        public static QuestManager QuestManager
        {
            get
            {
                if (_questManager == null)
                    _questManager = Object.FindFirstObjectByType<QuestManager>();
                return _questManager;
            }
        }
        public static DepositBox DepositBox
        {
            get
            {
                if (_depositBox == null)
                    _depositBox = Object.FindFirstObjectByType<DepositBox>();
                return _depositBox;
            }
        }
            public static EconomyManager EconomyManager
        {
            get
            {
                if (_economyManager == null)
                    _economyManager = Object.FindFirstObjectByType<EconomyManager>();
                return _economyManager;
            }
        }
        private void Awake() {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            GameUtilities.Reset();
        }
        public static void Reset()
        {
            _localPlayerController = null;
            _economyManager = null;
            _depositBox = null;
            _questManager = null;
        }
    }
}