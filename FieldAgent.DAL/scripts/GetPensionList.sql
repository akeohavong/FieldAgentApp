Create PROCEDURE [GetPensionList](
    @agencyId AS int
)
AS
BEGIN
    select ag.shortname, aa.badgeid, a.FIRSTname, a.lastname, a.dateofbirth, aa.deactivationdate
    from agent a
    join AgencyAgent aa on a.AgentId = aa.AgentId
    join Agency ag on aa.AgencyId = ag.AgencyId
    where ag.AgencyId = @agencyId;
END
GO