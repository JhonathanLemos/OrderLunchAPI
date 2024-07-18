# Sistema de Gestão de Restaurante Simulado

Este sistema foi desenvolvido para simular as operações de um restaurante, utilizando tecnologias avançadas e boas práticas de desenvolvimento.

## Padrões e Princípios
- **SOLID**: Conjunto de princípios para escrever código mais limpo e sustentável.
- **CQRS (Command Query Responsibility Segregation)**: Separação de operações de leitura e escrita.
- **Design Patterns**: Uso de padrões como Chain of Responsibility, Repository, Mediator e Service para abstração de lógica de negócios em CRUDs específicos.
- **Arquitetura em Camadas**: Organização do código em camadas distintas (apresentação, aplicação, domínio, infraestrutura).

## Tecnologias Utilizadas
- **ASP.NET Core**: Framework utilizado para o desenvolvimento da aplicação web.
- **Entity Framework Core**: ORM utilizado para interação com o banco de dados.
- **Identity Framework**: Framework para gerenciamento de identidades e autenticação.
- **JWT (JSON Web Tokens)**: Autenticação stateless através de tokens.
- **Redis**: Sistema de cache distribuído para armazenamento temporário de dados.
- **RabbitMQ**: Sistema de mensageria assíncrona para comunicação entre serviços.
- **SQL Server**: Banco de dados relacional utilizado como armazenamento principal.
- **Docker**: Plataforma de contêineres para facilitar implantação e gerenciamento.
- **GitHub Actions**: Ferramenta de automação para CI/CD.
- **Swagger**: Framework para documentação interativa de APIs.
- **Report Generator e Coverlet**: Ferramentas para geração e análise de relatórios de cobertura de código.
- **Fluent Validation**: Biblioteca para validação robusta de dados de entrada.
- **XMLLint**: Ferramenta para análise e validação de arquivos XML.

## Funcionalidades do Projeto
- **CRUD de Ingredientes, Lanches e Pedidos**: Implementação de operações CRUD para gerenciar Ingredientes, Lanches e Pedidos.
- **Autenticação e Autorização**: Utilização de JWT para autenticação. Autorização com permissões personalizadas para diferentes grupos de roles.
- **Notificações**: Envio de emails para notificação sobre o estado do pedido, utilizando RabbitMQ para gerenciamento interno. Ao receber mensagem do RabbitMQ, envia email para o cliente.
- **Logging Personalizado**: Implementação de logging detalhado, incluindo OnActionExecuted e OnActionExecuting para registrar ações, métodos e controllers que geram erros.
- **Validação**: Controle do ModelState para validação global. Utilização de Fluent Validation para validação robusta.
- **Swagger**: Documentação detalhada da API utilizando Swagger.
- **Filtros Personalizados**: Implementação de filtros personalizados como WhereIf para consultas dinâmicas. Manipulação customizada de requisições HTTP.
- **Paginação Automatizada**: Utilização de classes genéricas para paginação automatizada, otimizando consultas através de IQueryable.
- **Testes**: Desenvolvimento de testes unitários e de integração com xUnit. Uso de banco de dados em memória para testes.
- **Caching**: Cache distribuído com Redis para armazenamento temporário, otimizando o desempenho.

## Integração Contínua e Entrega Contínua (CI/CD)
Automação de testes e implantação utilizando GitHub Actions para garantir qualidade e rapidez no desenvolvimento.

## Documentação da API
Detalhamento das APIs utilizando Swagger para facilitar consumo e entendimento.

## Mapeamento Eficiente com Mappers
Eficiência no mapeamento entre objetos do sistema e fontes de dados.

## Passo a Passo para Configuração

1. Clone o repositório:
    ```bash
    git clone https://github.com/JhonathanLemos/LanchesAPI.git
    ```

2. Certifique-se de ter o SQL Server configurado e acessível.

3. Crie e inicie os containers Docker para RabbitMQ e Redis:
    ```bash
    docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
    docker run -d --name redis -p 6379:6379 redis
    ```

4. Após rodar os comandos acima, você pode baixar o Redis Desktop Manager em:
    [AnotherRedisDesktopManager](https://github.com/qishibo/AnotherRedisDesktopManager/releases)

5. Rode os containers no Docker Desktop.

6. Abra o projeto clonado no Visual Studio ou no editor de sua preferência.

7. Abra o Package Manager Console e execute o comando para atualizar o banco de dados:
    ```bash
    update-database
    ```

8. Após a atualização do banco de dados, você pode rodar o projeto.
