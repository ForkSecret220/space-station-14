// © SS220, An EULA/CLA with a hosting restriction, full text: https://raw.githubusercontent.com/SerbiaStrong-220/space-station-14/master/CLA.txt

using Content.Server.Antag;
using Content.Server.Chat.Systems;
using Content.Server.GameTicking.Rules.Components;
using Content.Server.NPC.Components;
using Robust.Server.Player;
using Content.Server.RoundEnd;
using Content.Server.Shuttles.Events;
using Content.Shared.CCVar;
using Robust.Shared.Configuration;
using Robust.Shared.Map;
using Content.Shared.Roles;
using Robust.Shared.Timing;
using Content.Server.GameTicking.Components;
using Content.Shared.NPC.Prototypes;

namespace Content.Server.GameTicking.Rules;

public sealed class VoidZoneRuleSystem : GameRuleSystem<VoidZoneRuleComponent>
{
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly ILogManager _logManager = default!;
    [Dependency] private readonly IGameTiming _gameTiming = default!;
    [Dependency] private readonly RoundEndSystem _roundEndSystem = default!;
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    [Dependency] private readonly AntagSelectionSystem _antagSelection = default!;
    private ISawmill _sawmill = default!;


    [ValidatePrototypeId<AntagPrototype>]
    public const string VoidNukeID = "Syndicate";

    [ValidatePrototypeId<NpcFactionPrototype>]
    private const string SyndicateFactionId = "Syndicate";

    [ValidatePrototypeId<NpcFactionPrototype>]
    private const string NanoTrasenFactionId = "NanoTrasen";

    public RoundEndSystem RoundEndSystem => _roundEndSystem;


    public override void Initialize()
    {
        base.Initialize();

        _sawmill = _logManager.GetSawmill("VoidZone");

        //SubscribeLocalEvent<RoundStartAttemptEvent>(OnStartAttempt);
        SubscribeLocalEvent<RulePlayerSpawningEvent>(OnPlayersSpawning);
        SubscribeLocalEvent<ControlShuttleEvent>(OnShuttleControlAttempt);
    }

    protected override void Added(EntityUid uid, VoidZoneRuleComponent component, GameRuleComponent gameRule, GameRuleAddedEvent args)
    {
        base.Added(uid, component, gameRule, args);

        gameRule.MinPlayers = _cfg.GetCVar(CCVars.VoidZoneMinPlayers);
    }

    protected override void Started(EntityUid uid, VoidZoneRuleComponent component, GameRuleComponent gameRule,
        GameRuleStartedEvent args)
    {
        base.Started(uid, component, gameRule, args);
    }

    private void OnStartAttempt(RoundStartAttemptEvent ev)
    {
        //TryRoundStartAttempt(ev, Loc.GetString("voidzone-title"));
        Log.Debug("VOIDZONE ROUND STARTED");
    }

    private void OnPlayersSpawning(RulePlayerSpawningEvent ev)
    {
        ///VoidZoneRuleSystem.Comp.Message = Loc.GetString("war-declarator-default-message");
        ///voidzone.Comp.DisableAt = _gameTiming.CurTime + TimeSpan.FromMinutes(VoidZoneRuleComponent.Comp.StartDeclarationDelay);
        ///Я  в душе не ебу как привязать хуйню

        var query = QueryActiveRules();
        while (query.MoveNext(out var uid, out var voidzone, out var gameRule))
        {

            if (ev.PlayerPool.Count == 0)
                continue;

            //var nukiesToSelect = _antagSelection.GetTargetAntagCount(_playerManager.PlayerCount, voidzone.PlayersPerSyndie, voidzone.MaxVoidSyndie);

        }

    }
    private void OnShuttleControlAttempt(ref ControlShuttleEvent ev)
    {
        var query = EntityQueryEnumerator<GameRuleComponent, VoidZoneRuleComponent>();
        while (query.MoveNext(out _, out _, out var voidzone))
        {
            //if (!HasComp<ShuttleConsoleComponent>(ev.Uid))  // badcode?
            ///   continue;

            if (voidzone.BriefingTimeVoidZone != null)
            {
                var timeAfterDeclaration = Timing.CurTime.Subtract(voidzone.BriefingTimeVoidZone.Value);
                var timeRemain = voidzone.BriefingTimerVoidZone.Subtract(timeAfterDeclaration);

                if (timeRemain > TimeSpan.Zero)
                {
                    ev.Cancelled = true;
                    ev.Reason = Loc.GetString("ОШИБКА: Обнаружен ионный шторм. Управление недоступно.",
                                    ("time", timeRemain.ToString("mm\\:ss")));
                    continue;
                }
            }

        }

    }

}
