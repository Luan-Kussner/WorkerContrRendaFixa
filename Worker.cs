using Microsoft.EntityFrameworkCore;
using WorkerContrRendaFixa.Data;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _startTime = new TimeSpan(0, 0, 0);  // inicia a meia noite 00:00
    private readonly TimeSpan _endTime = new TimeSpan(6, 0, 0);    // finaliza as 06:00 da manha

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var currentTime = DateTime.UtcNow.TimeOfDay;

            if (currentTime >= _startTime && currentTime <= _endTime)
            {
                _logger.LogInformation("Serviço em execução ás: {tempo}", DateTimeOffset.Now);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var today = DateTime.UtcNow.Date;
                    var yesterday = today.AddDays(-1);

                    // Verifica contratações do dia anterior
                    var contratacoesDoDiaAnterior = await dbContext.Contratacoes
                        .Include(c => c.Pagamentos)
                        .Where(c => c.DataContratacao.Date == yesterday)
                        .ToListAsync();

                    foreach (var contratacao in contratacoesDoDiaAnterior)
                    {
                        if (!contratacao.Pago)
                        {
                            var totalPagamentos = contratacao.Pagamentos.Sum(p => p.Valor);
                            var totalDevido = contratacao.Quantidade * contratacao.PrecoUnitario - contratacao.Desconto;

                            if (totalPagamentos >= totalDevido)
                            {
                                contratacao.Pago = true;
                            }
                        }
                    }

                    // Exclui contratações não pagas integralmente antes das 10:00 do dia seguinte
                    if (DateTime.UtcNow.TimeOfDay < new TimeSpan(10, 0, 0))
                    {
                        var contratacoesNaoPagas = await dbContext.Contratacoes
                            .Where(c => !c.Pago && c.DataContratacao.Date < today)
                            .ToListAsync();

                        dbContext.Contratacoes.RemoveRange(contratacoesNaoPagas);
                    }

                    await dbContext.SaveChangesAsync();
                }
            }
            else
            {
                _logger.LogInformation("Worker sleeping at: {time}", DateTimeOffset.Now);
            }

            // espera 10 min para executar novamente
            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}
