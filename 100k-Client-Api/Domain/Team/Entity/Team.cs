namespace _100k_Client_Api.Domain.Team.Entity;

using System.Text.Json.Serialization;
using ProjectEntity = _100k_Client_Api.Domain.Project.Entity.Project;
public class Team
{
    public string Name { get; set; }
    public bool Leader { get; set; }
    public List<ProjectEntity> Projects { get; set; }

    public Team(string name, bool leader, List<ProjectEntity> projects)
    {
        Name = name;
        Leader = leader;
        Projects = projects;
    }
    [JsonConstructor]
    public Team() { }
}