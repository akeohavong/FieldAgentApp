Create PROCEDURE [GetTopAgents]
AS
BEGIN
    select top(3) a.FirstName, a.LastName, a.DateOfBirth, count(m.MissionId) as [Missions Completed]
    from agent a
    join MissionAgent ma on a.AgentId = ma.AgentId
    join mission m on ma.MissionId = m.MissionId
    where m.ActualEnddate is NOT NULL
    GROUP by a.FirstName, a.LastName, a.DateOfBirth
    ORDER by COUNT(m.MissionId) desc;
END
GO