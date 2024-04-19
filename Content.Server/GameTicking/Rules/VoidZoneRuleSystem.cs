/*
Созревает вопрос, как я всё это писал с нулевыми знаниями C#. Ответ я в душе не ебу
Jokerge
*/
using Content.Server.Administration.Managers;
using Content.Server.Antag;
using Content.Server.GameTicking.Rules.Components;
using Content.Server.Humanoid;
using Content.Server.Mind;
using Content.Server.NPC.Components;
using Content.Server.NPC.Systems;
using Content.Server.Popups;
using Content.Server.Preferences.Managers;
using Content.Server.RandomMetadata;
using Content.Server.RoundEnd;
using Content.Server.Shuttles.Systems;
using Content.Server.Station.Systems;
using Content.Server.Store.Systems;
using Content.Shared.Roles;
using Content.Shared.Tag;
using Robust.Server.Player;
using Robust.Shared.Configuration;
using Robust.Shared.Prototypes;

namespace Content.Server.GameTicking.Rules;

public sealed class VoidZoneRuleSystem : GameRuleSystem<VoidZoneRuleComponent>
{
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly IServerPreferencesManager _prefs = default!;
    [Dependency] private readonly IAdminManager _adminManager = default!;
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    [Dependency] private readonly ILogManager _logManager = default!;
    [Dependency] private readonly EmergencyShuttleSystem _emergency = default!;
    [Dependency] private readonly HumanoidAppearanceSystem _humanoid = default!;
    [Dependency] private readonly MetaDataSystem _metaData = default!;
    [Dependency] private readonly RandomMetadataSystem _randomMetadata = default!;
    [Dependency] private readonly MindSystem _mind = default!;
    [Dependency] private readonly NpcFactionSystem _npcFaction = default!;
    [Dependency] private readonly PopupSystem _popupSystem = default!;
    [Dependency] private readonly RoundEndSystem _roundEndSystem = default!;
    [Dependency] private readonly SharedRoleSystem _roles = default!;
    [Dependency] private readonly StationSpawningSystem _stationSpawning = default!;
    [Dependency] private readonly StoreSystem _store = default!;
    [Dependency] private readonly TagSystem _tag = default!;
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

        SubscribeLocalEvent<RoundStartAttemptEvent>(OnStartAttempt);
        SubscribeLocalEvent<RulePlayerSpawningEvent>(OnPlayersSpawning);
    }

    protected override void Started(EntityUid uid, VoidZoneRuleComponent component, GameRuleComponent gameRule,
        GameRuleStartedEvent args)
    {
        base.Started(uid, component, gameRule, args);
    }

    private void OnStartAttempt(RoundStartAttemptEvent ev)
    {
        TryRoundStartAttempt(ev, Loc.GetString("voidzone-title"));
    }

    private void OnPlayersSpawning(RulePlayerSpawningEvent ev)
    {
        var query = QueryActiveRules();
        while (query.MoveNext(out var uid, out var voidzone, out var gameRule))
        {

            if (ev.PlayerPool.Count == 0)
                continue;

        }

    }
    private sealed class VoidZoneSpawn
    {
        public ICommonSession? Session { get; private set; }
        public VoidZoneSpawnPreset Type { get; private set; }

        public VoidZoneSpawn(ICommonSession? session, VoidZoneSpawnPreset type)
        {
            Session = session;
            Type = type;
        }
    }
}
