Create procedure setknowngoodstate
as
BEGIN
    delete from AgencyAgent;
    delete from SecurityClearance;
    delete from Alias;
    delete from MissionAgent;
    delete from Agent;
    delete from Mission;
    delete from [Location];
    delete from Agency;

    
    --Agency
    DBCC CHECKIDENT (Agency, RESEED, 0);
    insert into Agency(ShortName, LongName) values('FBI', 'Federal Bureau of Investigation');
    insert into Agency(ShortName, LongName) VALUES ('CIA', 'Central Intelligence Agency');
    insert into Agency(ShortName, LongName) VALUES ('NSA', 'National Security Agency');

    --Location
    DBCC CHECKIDENT ([Location], RESEED, 0);
    insert into Location (AgencyID, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (3, 'King, D''Amore and Auer', 'Basil', 'Marcy', 'Kuala Belait', '55555', 'BN');
    insert into Location (AgencyID, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (2, 'Ritchie and Sons', 'Crescent Oaks', 'Ronald Regan', 'Alfena', '4445-005', 'PT');
    insert into Location (AgencyID, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (2, 'Morissette, Hamill and Witting', 'Cardinal', 'Spohn', 'Intibucá', '55555', 'HN');
    insert into Location (AgencyID, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (2, 'Mitchell, Fay and Graham', 'Blaine', 'Union', 'Dayapan', '4211', 'PH');
    insert into Location (AgencyID, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (2, 'Moen Group', 'Rutledge', 'Gateway', 'Mourelos', '3025-600', 'PT');
    insert into Location (AgencyID, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (1, 'Swaniawski, Rutherford and Beer', 'Chinook', 'Hudson', 'Mirador', '87840-000', 'BR');
    insert into Location (AgencyID, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (1, 'Schuster LLC', 'Vermont', 'Lien', 'Vestmannaeyjar', '902', 'IS');
    insert into Location (AgencyID, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (1, 'Hessel, Weimann and Legros', 'Eastlawn', 'Fallview', 'Abilay', '5020', 'PH');
    insert into Location (AgencyID, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (2, 'Treutel, Barton and Rodriguez', 'Knutson', 'Merchant', 'Meishan', '55555', 'CN');
    insert into Location (AgencyID, LocationName, Street1, Street2, City, PostalCode, CountryCode) values (1, 'Champlin Group', 'Brentwood', 'Wayridge', 'Nida', '30001', 'LT');

    
    DBCC CHECKIDENT (Agent, RESEED, 0);
    INSERT into Agent(FirstName, LastName, DateOfBirth, Height) VALUES('John', 'Doe', '1980-01-02', 70);
    insert into Agent (firstname, lastname, DateOfBirth, Height) values ('Garnet', 'Rogez', '1955-07-23', 31);
    insert into Agent (firstname, lastname, DateOfBirth, Height) values ('Shani', 'Sterrie', '1989-04-16', 51);
    insert into Agent (firstname, lastname, DateOfBirth, Height) values ('Debra', 'Trees', '1969-07-14', 12);
    insert into Agent (firstname, lastname, DateOfBirth, Height) values ('Ford', 'Reuther', '1957-10-08', 26);
    insert into Agent (firstname, lastname, DateOfBirth, Height) values ('Sibelle', 'Rubinowitz', '1991-07-17', 38);
    insert into Agent (firstname, lastname, DateOfBirth, Height) values ('Darla', 'Osgar', '1997-10-14', 93);
    insert into Agent (firstname, lastname, DateOfBirth, Height) values ('Bevan', 'Spraggon', '1967-10-30', 82);
    insert into Agent (firstname, lastname, DateOfBirth, Height) values ('Edin', 'Catford', '1994-08-30', 23);
    insert into Agent (firstname, lastname, DateOfBirth, Height) values ('Javier', 'Gillcrist', '1957-01-08', 50);

    DBCC CHECKIDENT (Alias, RESEED, 0);
    insert into Alias (AgentID, AliasName, InterpolID, Persona) values (10, 'Pelican, brown', '2ef84bee-3cb8-4a25-ab03-7d6e061ead9f', 'benchmark leading-edge methodologies');
    insert into Alias (AgentID, AliasName, InterpolID, Persona) values (4, 'Common dolphin', '5682c626-e681-4aa4-ac52-7deae8b69ee9', 'cultivate e-business niches');
    insert into Alias (AgentID, AliasName, InterpolID, Persona) values (1, 'Cormorant, pied', 'db4fcada-4662-40d8-b432-b5b0d75a0fe1', 'evolve customized action-items');
    insert into Alias (AgentID, AliasName, InterpolID, Persona) values (4, 'Nelson ground squirrel', '6b7c38f2-a876-43fc-96ac-8e46af5880b3', 'seize intuitive relationships');
    insert into Alias (AgentID, AliasName, InterpolID, Persona) values (2, 'Malabar squirrel', 'ea656349-dac4-4a3a-af5d-482a293d895d', 'brand mission-critical ROI');
    insert into Alias (AgentID, AliasName, InterpolID, Persona) values (1, 'Tortoise, radiated', '1edd399a-523e-472e-8c06-c0f2b4d38c1a', 'evolve extensible action-items');
    insert into Alias (AgentID, AliasName, InterpolID, Persona) values (10, 'Mexican beaded lizard', '10d3e211-5607-4226-8ce3-5b523b6d8e12', 'deliver strategic architectures');
    insert into Alias (AgentID, AliasName, InterpolID, Persona) values (1, 'White-mantled colobus', '99429698-f621-47d9-af34-544a7ee2d297', 'revolutionize seamless metrics');
    insert into Alias (AgentID, AliasName, InterpolID, Persona) values (9, 'Lemur, sportive', '0f2c844d-d86d-4473-9625-68b7b93a23d4', 'innovate viral eyeballs');
    insert into Alias (AgentID, AliasName, InterpolID, Persona) values (2, 'American crow', 'f8ca9fb1-b8e7-4117-979a-007e13482c89', 'seize enterprise architectures');

    
    DBCC CHECKIDENT (Mission, RESEED, 0);
    insert into Mission (AgencyID, Codename, StartDate, ProjectedEnddate, ActualEnddate, Operationalcost, Notes) values (1, 'Mat Lam Tam', '2006-09-01', '2021-05-28', '2021-06-23', 7919.77, 'Crime|Drama|Thriller|War');
    insert into Mission (AgencyID, Codename, StartDate, ProjectedEnddate, ActualEnddate, Operationalcost, Notes) values (3, 'Cookley', '2004-10-27', '2021-09-12', '2021-07-28', 5986.99, 'Drama');
    insert into Mission (AgencyID, Codename, StartDate, ProjectedEnddate, ActualEnddate, Operationalcost, Notes) values (2, 'Fintone', '2000-06-13', '2022-04-13', '2022-01-13', 8850.05, 'Comedy|Drama');
    insert into Mission (AgencyID, Codename, StartDate, ProjectedEnddate, ActualEnddate, Operationalcost, Notes) values (3, 'Zamit', '2008-11-20', '2022-01-05', '2021-09-14', 8358.84, 'Drama');
    insert into Mission (AgencyID, Codename, StartDate, ProjectedEnddate, ActualEnddate, Operationalcost, Notes) values (1, 'It', '2007-09-09', '2021-12-07', '2022-02-12', 6285.4, 'Drama|Thriller');
    insert into Mission (AgencyID, Codename, StartDate, ProjectedEnddate, ActualEnddate, Operationalcost, Notes) values (2, 'It', '2002-09-17', '2021-07-17', '2021-11-17', 1427.33, 'Crime|Drama|Thriller');
    insert into Mission (AgencyID, Codename, StartDate, ProjectedEnddate, ActualEnddate, Operationalcost, Notes) values (3, 'Namfix', '2014-12-10', '2021-10-28', '2021-05-10', 9635.47, 'Action|Sci-Fi|Thriller');
    insert into Mission (AgencyID, Codename, StartDate, ProjectedEnddate, ActualEnddate, Operationalcost, Notes) values (1, 'Kanlam', '2009-04-14', '2022-01-08', '2022-01-06', 9588.94, 'Drama|Thriller');
    insert into Mission (AgencyID, Codename, StartDate, ProjectedEnddate, ActualEnddate, Operationalcost, Notes) values (2, 'Tampflex', '2009-09-30', '2021-09-05', '2021-08-29', 5167.49, 'Drama|Mystery|Romance|Thriller');
    insert into Mission (AgencyID, Codename, StartDate, ProjectedEnddate, ActualEnddate, Operationalcost, Notes) values (3, 'Konklab', '2003-06-25', '2021-11-19', '2021-05-06', 2981.75, 'Western');

    insert into MissionAgent(MissionId, AgentId) VALUES (1, 3);
    insert into MissionAgent(MissionId, AgentId) VALUES (2, 2);
    insert into MissionAgent(MissionId, AgentId) VALUES (3, 1);
    insert into MissionAgent(MissionId, AgentId) VALUES (4, 2);
    insert into MissionAgent(MissionId, AgentId) VALUES (5, 3);
    insert into MissionAgent(MissionId, AgentId) VALUES (6, 4);
    insert into MissionAgent(MissionId, AgentId) VALUES (7, 5);
    insert into MissionAgent(MissionId, AgentId) VALUES (7, 6);
    insert into MissionAgent(MissionId, AgentId) VALUES (8, 7);
    insert into MissionAgent(MissionId, AgentId) VALUES (9,8);
    insert into MissionAgent(MissionId, AgentId) VALUES (10, 9);

    DBCC CHECKIDENT (SecurityClearance, RESEED, 0)
    insert into SecurityClearance (SecurityClearanceName) values ('Programmer III');
    insert into SecurityClearance (SecurityClearanceName) values ('Research Assistant I');
    insert into SecurityClearance (SecurityClearanceName) values ('Associate Professor');
    insert into SecurityClearance (SecurityClearanceName) values ('Sales Associate');
    insert into SecurityClearance (SecurityClearanceName) values ('Librarian');
    insert into SecurityClearance (SecurityClearanceName) values ('Editor');
    insert into SecurityClearance (SecurityClearanceName) values ('Tax Accountant');
    insert into SecurityClearance (SecurityClearanceName) values ('Senior Sales Associate');
    insert into SecurityClearance (SecurityClearanceName) values ('Engineer III');
    insert into SecurityClearance (SecurityClearanceName) values ('Graphic Designer');

    insert into AgencyAgent (AgencyID, AgentID, SecurityClearanceID, BadgeID, ActivationDate, DeActivationDate, IsActive) values (1, 8, 4, '7547a8ca-fc56-4a2f-90f7-c9a22099225e', '9/10/1987', '10/7/2010', 0);
    insert into AgencyAgent (AgencyID, AgentID, SecurityClearanceID, BadgeID, ActivationDate, DeActivationDate, IsActive) values (3, 8, 9, '08e17c0e-5968-404c-ad4d-89373440e253', '7/16/1983', '5/1/2011', 1);
    insert into AgencyAgent (AgencyID, AgentID, SecurityClearanceID, BadgeID, ActivationDate, DeActivationDate, IsActive) values (2, 4, 8, 'de0fba4d-3d8f-4001-86b5-e029bf131134', '3/4/1979', '1/23/2012', 1);
    insert into AgencyAgent (AgencyID, AgentID, SecurityClearanceID, BadgeID, ActivationDate, DeActivationDate, IsActive) values (3, 8, 10, 'de15e9e6-f3be-46ff-acee-b8e3ee4e6fd3', '2/11/1992', '10/14/2016', 1);
    insert into AgencyAgent (AgencyID, AgentID, SecurityClearanceID, BadgeID, ActivationDate, DeActivationDate, IsActive) values (3, 4, 8, '16a7c1ad-4b97-4764-9f1a-fc33912d9a66', '1/13/1988', null, 0);
    insert into AgencyAgent (AgencyID, AgentID, SecurityClearanceID, BadgeID, ActivationDate, DeActivationDate, IsActive) values (3, 4, 5, 'b4b0c1e6-af04-4176-932d-0c4684129f61', '7/6/1983', '10/22/2010', 0);
    insert into AgencyAgent (AgencyID, AgentID, SecurityClearanceID, BadgeID, ActivationDate, DeActivationDate, IsActive) values (2, 1, 2, '3a31586c-747f-4785-b94f-f5d357ab1a8c', '1/14/1983', '4/9/2014', 0);
    insert into AgencyAgent (AgencyID, AgentID, SecurityClearanceID, BadgeID, ActivationDate, DeActivationDate, IsActive) values (3, 6, 1, '1baa78f7-b09b-4970-a4d5-8eddac509016', '5/18/1971', '11/19/2011', 1);
    insert into AgencyAgent (AgencyID, AgentID, SecurityClearanceID, BadgeID, ActivationDate, DeActivationDate, IsActive) values (2, 8, 6, '67de84d8-3d10-4b8e-870c-2b696146a092', '7/20/1984', null, 0);
    insert into AgencyAgent (AgencyID, AgentID, SecurityClearanceID, BadgeID, ActivationDate, DeActivationDate, IsActive) values (1, 1, 6, 'dd8a459a-6bbe-4d4f-b834-f8ec65fde081', '2/26/1983', '5/20/2012', 1);
END