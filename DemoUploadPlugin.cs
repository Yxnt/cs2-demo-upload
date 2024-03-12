using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;

namespace csgo2_demo_s3
{

    [MinimumApiVersion(190)]
    public class DemoUploadPlugin: BasePlugin, IPluginConfig<S3Config>
    {
        public override string ModuleName => "CS2DemoUploader";

        public override string ModuleVersion => "0.0.1";

        public override string ModuleAuthor => "Yxnt";

        private string _demoFileFullPath { get; set; } = string.Empty;
        private string _demoName { get; set; } = string.Empty;

        public S3Config Config { get; set; }
        public override void Load(bool hotReload)
        {
            Console.WriteLine("Hello World!");
        }

        public void OnConfigParsed(S3Config config)
        {
            if (config.EndPoint == null || config.AccessKey == null || config.SecretKey == null)
            {
                throw new Exception("Attribute cannot be null, Pls check endpoint/accesskey/secretkey attribute");
            }
            Config = config;
        }

        [ConsoleCommand("game_start", "Start Game")]
        [CommandHelper(whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
        public void GameStartCommand(CCSPlayerController? caller, CommandInfo command)
        {
            Server.ExecuteCommand("mp_warmup_end");
            _demoName = $"{Utils.FormatDemoFileName()}.dem";
            _demoFileFullPath = $"{Config.DemoStorePath}/{_demoName}";
            Server.ExecuteCommand($"tv_record {_demoFileFullPath}");
        }

        [ConsoleCommand("game_stop", "Stop Game")]
        [CommandHelper(whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
        public void GameStopCommand(CCSPlayerController? caller, CommandInfo command)
        {
            Server.ExecuteCommand("tv_stoprecord");
        }

        [GameEventHandler]
        public HookResult GameEndHandle(EventGameEnd @event)
        {
            Server.ExecuteCommand("tv_stoprecord");
            var zipFileName = Utils.ArchiveDemo(_demoFileFullPath, _demoName);
            //TODO: Upload zipFile to s3
            return HookResult.Continue;
        }
    }
}
