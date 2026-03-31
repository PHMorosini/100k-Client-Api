using _100k_Client_Api.Application.DTO;
using _100k_Client_Api.Application.Process.Interface;
using _100k_Client_Api.Domain.User.Entity;
using System.Diagnostics;
using System.Text.Json;

namespace _100k_Client_Api.Application.Process.Service;
public class ProcessService : IProcessService
{
    public static List<User> UsuariosCadastrados = new List<User>();
    public List<ActiveUsersPerDayDTO> ObterActivesUserPerDay(List<User> Usuarios, int minimo = 1)
    {
        return Usuarios
            .SelectMany(x => x.Logs)
            .GroupBy(u => u.Date)
            .Select(g => new ActiveUsersPerDayDTO
            {
                Date = g.Key,
                Total = g.Count(),
            })
            .Where(x => x.Total >= minimo) 
            .OrderBy(a => a.Date)
            .ToList();
    }

    public List<User> ObterSuperUsers(List<User> Usuarios)
    {
        return Usuarios.Where(x => x.Score >= 900 && x.Active).ToList();
    }

    public List<TeamInsightsDto> ObterTeamInsights(List<User> Usuarios)
    {
        return Usuarios
                .GroupBy(u => u.Team.Name)
                .Select(g => new TeamInsightsDto
                {
                    Name = g.Key,
                    TotalMembers = g.Count(),
                    Leaders = g.Count(u => u.Team.Leader),
                    CompletedProjects = g
                        .SelectMany(u => u.Team.Projects)
                        .Count(p => p.Completed),
                    ActivePercentual = g.Any()
                        ? Math.Round((decimal)g.Count(u => u.Active) * 100 / g.Count(), 1)
                        : 0
                })
                .ToList();
    }

    public List<TopCountriesDTO> ObterTopCountries(List<User> Usuarios)
    {

        var superUsuarios = ObterSuperUsers(Usuarios);

        return superUsuarios
            .GroupBy(u => u.Country)
            .Select(g => new TopCountriesDTO
            {
                Country = g.Key,
                Total = g.Count(),
            })
            .OrderByDescending(c => c.Total)
            .Take(5)
            .ToList();

    }

    public void PersistirDadosLista(string jsonDados)
    {
        var usuario = JsonSerializer.Deserialize<List<User>>(jsonDados, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        UsuariosCadastrados = usuario;
    }

    public int ObterQuantidadeUsuarios()
    {
        return UsuariosCadastrados.Count;
    }
}
