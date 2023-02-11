using System.Linq;
using System.Threading.Tasks;
using Code.Interaction.Flags.Mediator;
using Code.Player;
using Code.Player.UI;
using Code.Services;
using Code.Services.Assets;
using Code.Services.ConfigService;
using Code.Services.Factories;
using Code.Services.Input;
using Code.Services.Timer;
using Code.UI;
using UnityEngine;

namespace Code
{
    public class GameBootstraper : MonoBehaviour
    {
        [SerializeField] private MiniGame _miniGame;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private MiniSlider _slider;
        [SerializeField] private CaptureBanSlider _banSlider;

        private ISpawnerFactory _spawnerFactory;
        private IFlagFactory _flagFactory;
        private IAssetProvider _assetProvider;
        private IConfigLoader _configLoader;
        private ITimerService _timerService;
        private IFlagInvader _flagInvader;
        private IInputService _inputService;
        private IPlayerFactory _playerFactory;

        private Transform _player;
        private FlagMediator _mediator;

        private Config _config;

        private async void Start()
        {
            await LoadConfigAsync();
            InitAssetProvider();
            InitMediator();
            InitInputService();
            InitPlayerFactory();
            InitPlayer();
            InitFlagFactory();
            InitSpawnerFactory();


            FlagSpawner flagSpawner = InitFlagSpawner();

            InitMiniGame();
            InitSlider();

            SetMediatorDependencies(flagSpawner);
            InitGameStateHandler(flagSpawner);
            InitCaptureBanSlider();
        }

        private void SetMediatorDependencies(FlagSpawner flagSpawner)
        {
            _mediator.Player = _player.GetComponent<IMediatorReceiver>();
            _mediator.MiniGame = _miniGame;
            _mediator.Flags = flagSpawner.Flags.Select(f => f.GetComponent<IMediatorReceiver>()).ToArray();
        }

        private async Task LoadConfigAsync()
        {
            _configLoader = new ConfigLoader();

            _config = await _configLoader.LoadConfigAsync();
        }


        private void InitAssetProvider() => _assetProvider = new AssetProvider();

        private void InitMediator() => _mediator = new FlagMediator();

        private void InitFlagFactory() =>
            _flagFactory = new FlagFactory(_assetProvider, _player, _mediator,
                _player.GetComponent<IFlagInvader>(),
                _config.FlagRadius,
                _config.CapturingTime, _config.MiniGameCallAttemptRate);

        private void InitSpawnerFactory() =>
            _spawnerFactory = new SpawnerFactory(_assetProvider, _flagFactory, _config.FlagsCount);


        private void InitInputService() => _inputService = new JoystickInputService(_joystick);

        private void InitPlayerFactory() =>
            _playerFactory = new PlayerFactory(_assetProvider, _inputService, _config.PlayerMoveSpeed,
                _config.CaptureBanTime);


        private void InitPlayer() => _player = _playerFactory.CreatePlayer().transform;

        private FlagSpawner InitFlagSpawner()
        {
            FlagSpawner flagSpawner = _spawnerFactory.CreateFlagSpawner();
            flagSpawner.SpawnFlags();
            return flagSpawner;
        }

        private void InitMiniGame()
        {
            _miniGame.Construct(_mediator, _config.MiniGameDuration, _config.MiniGameWinArea);
            _miniGame.InitAreaSize();
        }

        private void InitSlider() => _slider.Construct(_config.MiniGameSliderSpeed);

        private void InitGameStateHandler(FlagSpawner flagSpawner)
        {
            var prefab = _assetProvider.Load<GameStateHandler>(AssetPath.GameStateHandler);

            GameStateHandler gameStateHandler = Instantiate(prefab);
            gameStateHandler.Construct(flagSpawner.Flags);
        }

        private void InitCaptureBanSlider()
        {
            _banSlider.Construct(_player.GetComponent<IFlagInvader>());
            _banSlider.Enable();
        }
    }
}