# Projeto FIAP Oficina 🔧🧰🚗

## Início rápido 🚀
Para rodar o projeto rapidamente execute os passos abaixo:

- Clone o repositório
> git clone https://github.com/josehteixeira/FIAPOficina.git
- Crie um arquivo .env para definir as configurações necessárias para subir o sistema, utilize o modelo abaixo:
```
SMTP_Server=ENDERECO DO SERVIDOR SMTP
SMTP_Port=PORTA DO SERVIDOR SMTP
SMTP_User=USUARIO PARA ENVIO DE EMAIL
SMTP_Password=SENHA DO USUARIO
JWT_KEY=CHAVE PARA GERACAO DO TOKEN JWT (EX: xNCuW3nX1QKfGc5B6lH+GpuzYH3X7OZj+o1pAdyLfV4=)
JWT_Issuer=FIAPOficina
JWT_Audience=FIAPOficina-clients
```
- No diretório principal, faça o build da imagem do docker
> docker build -t fiapoficina-api .
- Execute o docker compose, subindo a aplicação e banco de dados
> docker compose up -d

Se tudo der certo, você poderá acessar a documentação da API no swagger no endereço http://localhost:8080/swagger/index.html

## Sobre o Projeto 💡

O projeto **FIAP Oficina** é uma API desenvolvida em .NET, com o objetivo de gerenciar ordens de serviço de uma oficina mecânica, permitindo o controle de clientes, veículos, serviços e materiais utilizados. 

Permite gerenciar todo o fluxo de uma ordem de serviço, recebendo a demanda do cliente, passando por diagnóstico, enviando um orçamento por email para o cliente aprovar ou rejeitar, por fim executando, finalizando e entregando a ordem de serviço. 

Também faz controle do estoque dos materiais usados nos serviços, atualizando a quantidade disponível conforme os materiais são utilizados nos serviços.

## Tecnologias Utilizadas 💻

O projeto utiliza as seguintes tecnologias:

| Categoria | Tecnologias |
|------------|--------------|
| Backend | .NET 8, Entity Framework Core, ASP.NET Core Web API, Swagger |
| Banco de Dados | PostgreSQL |
| Containerização | Docker, Docker Compose |
| Outras | MailKit |

## Arquitetura 📐

O projeto está organizado em camadas seguindo os princípios de Clean Architecture, portanto possui os projetos principais *Domain*, *Application*, *Infrastructure* e *Api*.

FIAPOficina/<br>
├── FIAPOficina.Api/<br>
├── FIAPOficina.Application/<br>
├── FIAPOficina.Domain/<br>
└── FIAPOficina.Infrastructure/<br>

A API do projeto está documentada usando o Swagger e existem projetos de testes unitários.

### Domain

A camada de domínio define as principais entidades do sistema, como *Clients*, *Vehicles*, *ServiceOrders*, centralizando as regras para manter a consistência desses dados. Também define interfaces *IRepository*, onde são mapeados os métodos que podem ser aplicados a essas entidades, no que tange a manipulação delas em um banco de dados.

### Infrastructure

Neste projeto ficam as interações com partes externas ao sistema, como banco de dados e envio de email. Aqui são definidas conexões com banco de dados e são implementadas as interfaces *IRepositories* definidas nos projetos *Domain* ou *Application*. Também fica a definição do banco de dados, com os *mappings* das entidades e as *migrations* com os comandos que definem a base de dados.

### Application

Toda a regra do negócio fica centralizada neste projeto, bem organizada em *Commands* que definem os parâmetros para execução das lógicas que são implementadas em *Handlers* e que são expostos através de *Services*.

### API

Este é o projeto de inicialização da aplicação, contendo todas as configurações do sistema, inicializações e injeções de dependências. Também contém as *Controllers* que disponibilizam as rotas que expõe os meios para utilização do sistema.
