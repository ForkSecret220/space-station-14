using Content.Shared.Dataset;
using Content.Server.NPC.Components;
using Content.Shared.Roles;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;


namespace Content.Server.GameTicking.Rules.Components;

[RegisterComponent, Access(typeof(VoidZoneRuleSystem))]
public sealed partial class VoidZoneRuleComponent : Component
{
    [DataField]
    public int PlayersPerSyndie = 1;

    public int MaxVoidSyndie = 70;

    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer))]
    public TimeSpan? BriefingTimeVoidZone;

    [DataField]
    public TimeSpan BriefingTimerVoidZone = TimeSpan.FromMinutes(15);

}
