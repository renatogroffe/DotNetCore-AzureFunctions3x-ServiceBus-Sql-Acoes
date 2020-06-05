using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using FunctionAppAcoes.Models;
using FunctionAppAcoes.Validators;
using FunctionAppAcoes.Data;

namespace FunctionAppAcoes
{
    public static class AcoesServiceBusTopicTrigger 
    {
        [FunctionName("AcoesServiceBusTopicTrigger")]
        public static void Run([ServiceBusTrigger("topic-acoes",
            "FunctionAppAcoesSqlServer",
            Connection = "AzureServiceBusConnection")]string mySbMsg,
            ILogger log)
        {
            log.LogInformation($"AcoesServiceBusTopicTrigger - Dados: {mySbMsg}");
            
            var acao = JsonSerializer.Deserialize<DadosAcao>(mySbMsg,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            var validationResult = new AcaoValidator().Validate(acao);
            
            if (validationResult.IsValid)
            {
                log.LogInformation($"AcoesServiceBusTopicTrigger - Dados pós formatação: {mySbMsg}");
                AcoesRepository.Save(acao);
                log.LogInformation("AcoesServiceBusTopicTrigger - Ação registrada com sucesso!");
            }
            else
            {
                log.LogInformation("AcoesServiceBusTopicTrigger - Dados inválidos para a Ação");
            }
        }
    }
}