# RKC.Cursos

- [Instalação](#ancora1)
  - [Rodando no Console](#ancora7)
- [Login e Autenticação](#ancora2)
- [Api](#ancora3)
  - [Módulos](#ancora4)
  - [Aulas](#ancora5)
- [Modelos e Dtos](#ancora6)


<a id="ancora1"></a>
## Instalação

O projeto pode ser rodado localmente via console, ele usa banco de dados PostgreSQL e .NET 5.0;

| :exclamation:  Importante               |
|-----------------------------------------|
O tuturia a baxio subentende que não há uma configuração PostgreSQL e .NET já instalado na máquina. Caso já haja ou queira usar valores diferentes deve ser alterado valores na configurações no serviço e pode ser pulado etapas de configuração de PostgreSQL.

1. Fazer clone do projeto: `git clone git@github.com:RafaelKC/RKC.Cursos.git`;
2. Instale o [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0);
3. Baixar o [PostgreSQL](https://www.postgresql.org/download/) e instalar:
   1. Porta padrão do PostgreSQL **(5432)**;
   2. Senha ``root``;
4. Criar o _database_ ``RKC.Cursos`` no PostgreSQL:
   1. Usuário `postgres`;
5. **Caso utilizar valores diferetes:** configurar _ConnectionStrings_ nos arquivos _appsettings.json_ e _appsettings.Development.json_ com os valores definidos;
   1. "Server=_localhost ou ip_,_porta_; Host=_localhost ou ip_; Port=_porta_; Database=_nome do databse_; User Id=_usuário_; Password=_senha_;"

<a id="ancora7"></a>
#### Rodando no console

* Para executar o serviço rode pelo console na raiz do projeto ``dotnet run --project ./``;

<a id="ancora2"></a>
## Usuários e Authenticação

* Ao ser realizado a primeira request para o serviço, para qualque endpoinr, será criada as tabelas no _database_ e criado um usuário _SystemAdmin_ no banco;
* O usuário _SystemAdmin_ é o unico com autorização para criar e atualizar outros usuário, mas apenas usuários _CursoAdmin_;
* O usuário _SystemAdmin_ não pode ser atualizado e nenhum usuário deletado;
* O usuário _CursoAdmin_ e _SystemAdmin_ tem permissões de gerenciar Módulos e Aulas;
* Para o primeiro Login no serviço usar as credenciais do _SystemAdmin_ criado no iníci da execução: 
  * Email: "system@admin.com";
  * UserName: "System Admin";
  * Senha: "admin123";
  

#### Endpoints de User:
| Nome | Endpoint | Método | Descrição | Input | Output | Autorizado |
|------|--------|----------|-----------|-------|--------|------------|
|Create|`/cursos/user`|POST|Cria o usuário recebido no body, caso não seja setado _UserName_ ele seta FirstName + _LastName_ como _UserName_|Body: [UserInput](#ancoraUserInput)||_SystemAdmin_|
|Get|`/cursos/user/{id:guid}`|GET|Retorna o usuário com Id igual ao enviado na rota|Rota: Guid|[UserOutput](#ancoraUserOutput)|_SystemAdmin_ e _CursoAdmin_|
|GetList|`/cursos/user`|GET|Retorna uma lista de usuários ordenados pelo _FirstName_|Parâmetros: [UserGetListInput](#UserGetListInput)| List de [UserOutput](#ancoraUserOutput)|_SystemAdmin_ e _CursoAdmin_|
|Update|`/cursos/user/{id:guid}`|PUT|Edita o usuário com Id igual ao enviado na rota com as informações do body. Pode inativar ou dar ou remover status de _SystemAdmin_ a um usuário. Não altera credenciais e não pode ser usado com o "System Admin"|Rota: Guid, <br> Body: [UserOutput](#ancoraUserOutput) ||_SystemAdmin_|


#### Endpoints de Autenticação
| Nome | Endpoint | Método | Descrição | Input | Output |
|------|--------|----------|-----------|-------|--------|
|Login|`cursos/authentication/login`|POST|Não necessita de autenticação, valida as credenciais enviads no body e gera o _accesToken_|Body: [LoginInput](#ancoraLoginInput)|[LoginOutput](#ancoraLoginOutput)|

## Módulos
* Create `cursos/modulos` (POST) recebe ModuloInput, usado para criar modulos;
* Get `cursos/modulos/{id:guid}` (GET) recebe id do módulo e retorna um ModuloOutput;
* GetList `cursos/modulos` (GET) pode receber pela query uma string para filtrar por nome, retorna lista de ModulosOutput;
* Update `cursos/modulos/{id:guid}` (PUT) recebe id do módulo e ModuloInput, atualiza modulo;
* Delete `cursos/modulos/{id:guid}` (DELETE) recebe id do módulo e deleta modulo;

## Aulas
* Create `cursos/modulos/{idModulo:guid}/aulas` (POST) recebe AulaInput e id do modulo, cria aula para esse modulo;
* Get `cursos/modulos/{idModulo:guid}/aulas/{idAula:Guid}` (GET) recebe id do modulo e de aula, retorn AulaOutput com esse id de aula e modulo;
* GetList `cursos/modulos/{idModulo:guid}/aulas` (GET) recebe id do modulo e pode aulaFiltro, retorna AulasOutput desse modulo;
* Update `cursos/modulos/{idModulo:guid}/aulas/{idAula:Guid}` (PUT) recebe AulaInput, id do modulo e de aula, atualiza aula com esse id e moduloId;
* Delete `cursos/modulos/{idModulo:guid}/aulas/{idAula:Guid}` (DELETE) recebe id do modulo e de aula, delete aula com esse id e moduloId;
