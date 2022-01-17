# RKC.Cursos

## Usuários e Login
* Ao ser realizado a primeira request para o serviço será criada as tabelas e adicionará um usuário _SystemAdmin_ no banco;
* O usuário _SystemAdmin_ é o unico com autorização para criar e atualizar usuário, mas apenas usuários _CursoAdmin_;
* email: "system@admin.com", senha: "admin123", userName: "System Admin";
* Endpoints de User:
  * Create: `/cursos/user` (POST), utilizado para criar usuários, recebe UserInput, somente um usuário SystemAdmin pode criar;
  * Get: `/cursos/user/{id:guid}` (GET), retornar um usuário com Id igual ao passado como propriedade, Retornar UserOutput;
  * GetList: `/cursos/user` (GET), retorna uma lista de usuários ordenado pelo _FirstName_, recebe UserGetListInput por onde pode ser setado filtros;
  * Update: `/cursos/user/{id:guid}` (PUT), edita usuários, pode ser utilizado para inativar ou dar status de _SystemAdmin_ à um usuário _CursoAdmin_. Somente _SystemAdmin_ pode usar e recebe um UserInput;
  * 