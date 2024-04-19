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

    [DataField]
    public VoidZoneSpawnPreset AdmiralSpawnDetails = new() { AntagRoleProto = "AdmiralVoidZone", GearProto = "SyndicateCommanderGearFull", NamePrefix = "nukeops-role-commander", NameList = "SyndicateNamesElite" };
}
