# RKC.Cursos

## TAGs:
  1.0.0 - Ok não possui Docker;
  1.0.1 - Não usar com Docker;
  1.0.2 - Ok para Docker;


- [Instalação](#ancora1)
  - [Rodando no Console](#ancora7)
  - [Rodando no Docker](#ancora8)
- [Login e Autenticação](#ancora2)
- [Api](#ancora3)
  - [Módulos](#ancora4)
  - [Aulas](#ancora5)
- [Modelos e Dtos](#ancora6)


<a id="ancora1"></a>
## Instalação

O projeto pode ser rodado localmente via console ou pelo Docker, ele usa banco de dados PostgreSQL e .NET 5.0. Ele poderá ser acessado no localhost na porta **50000** ou **88** se for rodado pelo docker.

<a id="ancora7"></a>
#### Rodando no console

| :exclamation:  Importante               |
|-----------------------------------------|
O tuturia a baxio subentende que não há uma configuração PostgreSQL e .NET já instalado na máquina. Caso já haja ou queira usar valores diferentes deve ser alterado valores na configurações no serviço e pode ser pulado etapas de configuração de PostgreSQL ou .NET 5.

1. Fazer clone do projeto: `git clone git@github.com:RafaelKC/RKC.Cursos.git`;
2. Instale o [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0);
3. Baixar o [PostgreSQL](https://www.postgresql.org/download/) e instalar:
   1. Porta padrão do PostgreSQL **(5432)**;
   2. Senha ``root``;
4. Criar o _database_ ``RKC_Cursos`` no PostgreSQL:
   1. Usuário `postgres`;
5. **Caso utilizar valores diferetes:** configurar _ConnectionStrings_ nos arquivos _appsettings.json_ e _appsettings.Development.json_ com os valores definidos;
   1. "Server=_localhost ou ip,porta_; Host=_localhost ou ip_; Port=_porta_; Database=_nome do databse_; User Id=_usuário_; Password=_senha_;
6. Para executar o serviço rode pelo console na raiz do projeto ``dotnet run --project ./``;
7. O serviço estará rodando na porta **50000**;

<a id="ancora8"></a>
#### Rodando no Docker
Roda no Docker o extremamente mais simples, poucos passos necessários mas necessita de ter o [Docker](https://www.docker.com/) intalado na máquina.

1. Fazer clone do projeto: `git clone git@github.com:RafaelKC/RKC.Cursos.git`;
2. Pelo console na pasta raiz do projeto rode `docker-compose up -d`;
3. Quando terminar de suber e criar os containers o serviço estará rodando na porta **88**;
4. O _database_ poderá ser acessado pela porta **8400**;

<a id="ancora2"></a>
## Usuários e Authenticação

* Ao ser realizado a primeira request para o serviço, para qualque endpoint, será criada as tabelas no _database_ e criado um usuário _SystemAdmin_ no banco;
* O usuário _SystemAdmin_ é o unico com autorização para criar e atualizar outros usuário, mas apenas usuários _CursosAdmin_;
* O usuário _SystemAdmin_ não pode ser atualizado e nenhum usuário deletado;
* O usuário _CursosAdmin_ e _SystemAdmin_ tem permissões de gerenciar Módulos e Aulas;
* Para o primeiro Login no serviço usar as credenciais do _SystemAdmin_ criado no início da execução: 
  * Email: "system@admin.com";
  * UserName: "System Admin";
  * Senha: "admin123";
  

#### Endpoints de User:
| Nome | Endpoint | Método | Descrição | Input | Output | Autorizado |
|------|--------|----------|-----------|-------|--------|------------|
|Create|`/cursos/user`|POST|Cria o usuário recebido no body, caso não seja setado _UserName_ ele seta FirstName + _LastName_ como _UserName_|Body: [UserInput](#ancoraUserInput)||_SystemAdmin_|
|Get|`/cursos/user/{id:guid}`|GET|Retorna o usuário com Id igual ao enviado na rota|Rota: UserId (Guid)|[UserOutput](#ancoraUserOutput)|_SystemAdmin_ e _CursosAdmin_|
|GetList|`/cursos/user`|GET|Retorna uma lista de usuários ordenados pelo _FirstName_|Parâmetros: [UserGetListInput](#ancoraUserGetListInput)| List de [UserOutput](#ancoraUserOutput)|_SystemAdmin_ e _CursosAdmin_|
|Update|`/cursos/user/{id:guid}`|PUT|Edita o usuário com Id igual ao enviado na rota com as informações do body. Pode inativar ou dar ou remover status de _SystemAdmin_ a um usuário. Não altera credenciais e não pode ser usado com o "System Admin"|Rota: Guid, <br> Body: [UserOutput](#ancoraUserOutput) ||_SystemAdmin_|


#### Endpoints de Autenticação
| Nome | Endpoint | Método | Descrição | Input | Output |
|------|--------|----------|-----------|-------|--------|
|Login|`/cursos/authentication/login`|POST|Não necessita de autenticação, valida as credenciais enviads no body e gera o _accesToken_|Body: [LoginInput](#ancoraLoginInput)|[LoginOutput](#ancoraLoginOutput)|

<a id="ancora3"></a>
## Api

<a id="ancora4"></a>

| :exclamation:  Importante               |
|-----------------------------------------|
|Todos daqui para baixo necessitam de autenticação, _SystemAdmin_ ou _CursosAdmin_|

#### Módulos
| Nome | Endpoint | Método | Descrição | Input | Output |
|------|----------|--------|-----------|-------|--------|
|Create|`/cursos/modulos`|POST|Cria o modulo recebido no body|Body: [ModuloInput](#ancoraModuloInput)||
|Get|`/cursos/modulos/{id:guid}`|GET|Retorna o módulo com Id igual ao recebido na rota||[ModuloOutput](#ancoraModuloOutput)|
|GetList|`/cursos/modulos`|GET|Retorna lista de módulos ordenado pelo nome do módulo e podendo ser filtrado pelo mesmo|Parâmetros: nomeFilter (string)|List de [ModuloOutput](#ancoraModuloOutput)|
|Update|`/cursos/modulos/{id:guid}`|PUT|Atualiza o módulo com Id recebido na rota com o módulo recebido no body|Rota: ModuloId (Guid) Body: [ModuloInput](#ancoraModuloInput)||
|Delete|`/cursos/modulos/{id:guid}`|DELETE|Deleta o módulo com Id igual ao recebido na rota|Rota: IdModulo(Guid)||

<a id="ancora5"></a>
#### Aulas
| Nome | Endpoint | Método | Descrição | Input | Output |
|------|----------|--------|-----------|-------|--------|
|Create|`/cursos/modulos/{idModulo:guid}/aulas`|POST|Cria aula recebida no body para o módulo com Id recebido na rota|Rota: ModuloId (Guid) Body: [AulaInput](#ancoraAulaInput)||
|Get|`/cursos/modulos/{idModulo:guid}/aulas/{idAula:Guid}`|GET|Retorna a aula com Id e ModuloId igual aos recebido na rota|Rota: AulaId (Guid) e ModuloId (Guid) |[AulaOutput](#ancoraAulaOutput)|
|GetList|`/cursos/modulos/{idModulo:guid}/aulas`|GET|Retorna lista de aulas ordenada pelo nome da aula e podendo ser filtrado pelo mesmo, aulas do módulo com o Id recebido na rota|Rota: ModuloId (Guid) Parâmetro: nomeFilter(string)|List de [AulaOutput](#ancoraAulaOutput)|
|Update|`/cursos/modulos/{idModulo:guid}/aulas/{idAula:Guid}`|PUT|Atualiza aula com Id e ModuloId igual aos recebidos na rota com a aula recebida no body|Rota: AulaId (Guid) e ModuloId (Guid) Body: [AulaInput](#ancoraAulaInput)||
|Delete|`/cursos/modulos/{idModulo:guid}/aulas/{idAula:Guid}`|DELETE|Deleta a aula com Id e ModuloId igual aos recebidos na rota|Rota: AulaId (Guid) e ModuloId (Guid)||

<a id="ancora6"></a>
## Modelos e Dtos

<a id="ancoraUserInput"></a>
#### UserInput
    Id: Guid;
    FirstName: String;
    LastName: string;
    UserName: string; 
    Email: string; 
    Role: UserRole; 
    IsInactive: bool; 
    Password: string;
    
<a id="ancoraUserOutput"></a>
#### UserOutput
    Id: Guid;
    FirstName: String;
    LastName: string;
    UserName: string; 
    Email: string; 
    Role: UserRole; 
    IsInactive: bool; 
    
<a id="ancoraUserGetListInput"></a>
#### UserGetListInput
    FilterByUserName: strign;
    GetInactivesToo: bool;
    FilterByUserRole: UserRole?;
    
<a id="ancoraUserRole"></a>
#### UserRole (Enum)
    SystemAdmin = 0;
    CursosAdmin = 1;
    
<a id="ancoraLoginInput"></a>
#### LoginInput
    EmailOrUserName: string;
    Password: string;
    
<a id="ancoraLoginOutput"></a>
#### LoginOutput
    AccessToken: string;
    User: UserOutput 

<a id="ancoraModuloInput"></a>
#### ModuloInput
    Id: Guid;
    Nome: string;
    
<a id="ancoraModuloOutput"></a>
#### ModuloOutput
    Id: Guid;
    Nome: string;
    TotalAulas: int;
    TotalHorasAula: int;
    
<a id="ancoraAulaInput"></a>
#### AulaInput
    Id: Guid;
    ModuloId: Guid;
    Nome: string;
    Duracao: int:
    DataAcontecer: DateTime
    
<a id="ancoraAulaOutput"></a>
#### AulaOutput
    Id: Guid;
    ModuloId: Guid;
    Nome: string;
    Duracao: int:
    DataAcontecer: DateTime
    
