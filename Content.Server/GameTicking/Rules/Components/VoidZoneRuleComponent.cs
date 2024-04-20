using Content.Shared.Dataset;
using Content.Server.NPC.Components;
using Content.Shared.Roles;
using Robust.Shared.Prototypes;


namespace Content.Server.GameTicking.Rules.Components;

[RegisterComponent, Access(typeof(VoidZoneRuleSystem))]
public sealed partial class VoidZoneRuleComponent : Component
{
    [DataField]
    public int PlayersPerSyndie = 1;

    public int MaxVoidSyndie = 70;

    [DataField(required: true)]
    public ProtoId<NpcFactionPrototype> Faction = default!;

    [DataField]
    public VoidZoneSpawnPreset AdmiralSpawnDetails = new() { AntagRoleProto = "AdmiralSyndicateVoidZone", GearProto = "SyndicateCommanderGearFull", NamePrefix = "nukeops-role-commander", NameList = "SyndicateNamesElite" };
}

[DataDefinition, Serializable]
public sealed partial class VoidZoneSpawnPreset
{

    [DataField]
    public ProtoId<AntagPrototype> AntagRoleProto = "AdmiralSyndicateVoidZone";

    /// <summary>
    /// The equipment set this operative will be given when spawned
    /// </summary>
    [DataField]
    public ProtoId<StartingGearPrototype> GearProto = "SyndicateOperativeGearFull";

    /// <summary>
    /// The name prefix, ie "Agent"
    /// </summary>
    [DataField]
    public LocId NamePrefix = "nukeops-role-operator";

    /// <summary>
    /// The entity name suffix will be chosen from this list randomly
    /// </summary>
    [DataField]
    public ProtoId<DatasetPrototype> NameList = "SyndicateNamesNormal";
}
