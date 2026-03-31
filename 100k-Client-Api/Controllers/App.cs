using _100k_Client_Api.Application.Process.Interface;
using _100k_Client_Api.Domain.User.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _100k_Client_Api.Controllers;

public class App : Controller
{
    private readonly IProcessService _processService;
    public static List<User> UsuariosCadastrados = new List<User>();
    public App(IProcessService processService)
    {
        _processService = processService;
    }

    [HttpPost("/users")]
    public IActionResult Users([FromBody] List<User> usuarios)
    {
        Stopwatch cronometro = new Stopwatch();
        cronometro.Start();
        UsuariosCadastrados = usuarios;
        cronometro.Stop();

        var response = new
        {
            quantidadeClientes = UsuariosCadastrados.Count(),
            tempoProcessamento = cronometro.ElapsedMilliseconds
        };

        return Ok(response);
    }

    [HttpGet("/superusers")]
    public IActionResult SuperUsers()
    {
        Stopwatch cronometro = new Stopwatch();
        cronometro.Start();

        var superUsers = _processService.ObterSuperUsers(UsuariosCadastrados);
        cronometro.Stop();
        var response = new
        {
            usuarios = superUsers,
            tempoProcessamento = cronometro.ElapsedMilliseconds
        };

        return Ok(response);
    }
    [HttpGet("/top-countries")]
    public IActionResult TopCountries()
    {
        Stopwatch cronometro = new Stopwatch();
        cronometro.Start();

        var countries = _processService.ObterTopCountries(UsuariosCadastrados);
        cronometro.Stop();
        var response = new
        {
            Paises = countries,
            tempoProcessamento = cronometro.ElapsedMilliseconds
        };

        return Ok(response);
    }

    [HttpGet("/active-users-per-day")]
    public IActionResult ActiveUsersPerDay([FromQuery] int min = 1)
    {
        Stopwatch cronometro = new Stopwatch();
        cronometro.Start();

        var users = _processService.ObterActivesUserPerDay(UsuariosCadastrados,min);
        cronometro.Stop();
        var response = new
        {
            Usuarios = users,
            tempoProcessamento = cronometro.ElapsedMilliseconds
        };

        return Ok(response);
    }

    [HttpGet("/team-insights")]
    public IActionResult TeamInsights()
    {
        Stopwatch cronometro = new Stopwatch();
        cronometro.Start();

        var insights = _processService.ObterTeamInsights(UsuariosCadastrados);
        cronometro.Stop();
        var response = new
        {
            Result = insights,
            tempoProcessamento = cronometro.ElapsedMilliseconds
        };

        return Ok(response);
    }
}
