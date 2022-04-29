create PROCEDURE [ClearanceAudit](
    @securityClearanceId AS int,
    @agencyId AS int
)
AS
BEGIN
    select a.firstname, a.lastname, a.DateOfBirth, aa.BadgeId, aa.ActivationDate, aa.DeactivationDate
    from agent a
    join AgencyAgent aa on a.AgentId = aa.AgentId
    join SecurityClearance s on aa.SecurityClearanceId = s.SecurityClearanceId
    join agency ag on aa.AgencyId = ag.AgencyId
    where s.SecurityClearanceId = @securityClearanceId and ag.AgencyId = @agencyId;
END
GO