Contratação de Renda Fixa WORKER

WorkerContrRendaFixa

O WorkerContrRendaFixa é um serviço desenvolvido em .NET 8.0 que executa tarefas em segundo plano para o sistema contrRendaFixa.
Sua principal responsabilidade é processar as contratações financeiras que não foram pagas integralmente no dia anterior, garantindo que apenas contratações pagas permaneçam no sistema.

Funcionalidades

Execução Diária: O Worker é executado diariamente à meia-noite (00:00).

Processamento de Contratações Não Pagas: Identifica e exclui contratações do dia anterior que não foram pagas integralmente até as 23:59 do dia da contratação.

Tecnologias Utilizadas

.NET 8.0

Entity Framework Core

PostgreSQL

Hosted Services em ASP.NET Core
