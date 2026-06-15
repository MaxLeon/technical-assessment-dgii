using DGII.Domain.Entities;
using DGII.Domain.Enums;
using Xunit;
using DGII.Infrastructure.Data;
using DGII.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Testcontainers.PostgreSql;

namespace DGII.Infrastructure.Tests.Repositories;

public class ContribuyenteRepositoryTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .Build();

    private AppDbContext _context = default!;
    private ContribuyenteRepository _repository = default!;

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(_postgres.GetConnectionString())
            .Options;

        _context = new AppDbContext(options);
        await _context.Database.MigrateAsync();

        var loggerMock = new Mock<ILogger<ContribuyenteRepository>>();
        _repository = new ContribuyenteRepository(_context, loggerMock.Object);
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _postgres.DisposeAsync();
    }

    [Fact]
    public async Task CreateAsync_ConContribuyenteValido_DebePersistitEnDb()
    {
        // GIVEN
        var contribuyente = Contribuyente.Create("98754321012", "JUAN PEREZ", TipoContribuyente.PersonaFisica, EstatusContribuyente.Activo);

        // WHEN
        await _repository.CreateAsync(contribuyente);

        // THEN
        var guardado = await _repository.GetByRNCCedula("98754321012");
        guardado.Should().NotBeNull();

        // AND
        guardado!.Nombre.Should().Be("JUAN PEREZ");
        guardado.Tipo.Should().Be(TipoContribuyente.PersonaFisica);
        guardado.Estatus.Should().Be(EstatusContribuyente.Activo);
    }

    [Fact]
    public async Task HasContribuyenteAsync_ConRncRegistrado_DebeRetornarTrue()
    {
        // GIVEN
        var contribuyente = Contribuyente.Create("98754321012", "JUAN PEREZ", TipoContribuyente.PersonaFisica, EstatusContribuyente.Activo);
        await _repository.CreateAsync(contribuyente);

        // WHEN
        var existe = await _repository.HasContribuyenteAsync("98754321012");

        // THEN
        existe.Should().BeTrue();
    }

    [Fact]
    public async Task HasContribuyenteAsync_ConRncNoRegistrado_DebeRetornarFalse()
    {
        // GIVEN - DB vacía para este RNC

        // WHEN
        var existe = await _repository.HasContribuyenteAsync("00000000000");

        // THEN
        existe.Should().BeFalse();
    }

    [Fact]
    public async Task GetAllAsync_ConMultiplesContribuyentes_DebeRetornarTodos()
    {
        // GIVEN
        await _repository.CreateAsync(Contribuyente.Create("98754321012", "JUAN PEREZ", TipoContribuyente.PersonaFisica, EstatusContribuyente.Activo));
        await _repository.CreateAsync(Contribuyente.Create("123456789", "EMPRESA SA", TipoContribuyente.PersonaJuridica, EstatusContribuyente.Activo));

        // WHEN
        var todos = await _repository.GetAllAsync();

        // THEN
        todos.Should().NotBeNull();
        todos.Should().HaveCount(2);

        // AND
        todos.Should().Contain(c => c.RncCedula == "98754321012");
        todos.Should().Contain(c => c.RncCedula == "123456789");
    }
}
