using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelando.Dados.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Clientes",
                columns: table => new
                {
                    Id_Cliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Clientes", x => x.Id_Cliente);
                });

            migrationBuilder.CreateTable(
                name: "TB_Especialidades",
                columns: table => new
                {
                    ID_Especialidade = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DS_Especialidade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Especialidades", x => x.ID_Especialidade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Profissionais",
                columns: table => new
                {
                    Id_Profissional = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Profissionais", x => x.Id_Profissional);
                });

            migrationBuilder.CreateTable(
                name: "TB_Projetos",
                columns: table => new
                {
                    Id_Projeto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DS_Projeto = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Cliente = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.Id_Projeto);
                    table.ForeignKey(
                        name: "FK_TB_Projetos_TB_Clientes_ID_Cliente",
                        column: x => x.ID_Cliente,
                        principalTable: "TB_Clientes",
                        principalColumn: "Id_Cliente");
                });

            migrationBuilder.CreateTable(
                name: "TB_Especialidade_Profissional",
                columns: table => new
                {
                    Id_Especialidade = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Profissional = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Especialidade_Profissional", x => new { x.Id_Especialidade, x.Id_Profissional });
                    table.ForeignKey(
                        name: "FK_TB_Especialidade_Profissional_TB_Especialidades_Id_Especialidade",
                        column: x => x.Id_Especialidade,
                        principalTable: "TB_Especialidades",
                        principalColumn: "ID_Especialidade",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Especialidade_Profissional_TB_Profissionais_Id_Profissional",
                        column: x => x.Id_Profissional,
                        principalTable: "TB_Profissionais",
                        principalColumn: "Id_Profissional",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Especialidade_Projeto",
                columns: table => new
                {
                    Id_Especialidade = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Projeto = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Especialidade_Projeto", x => new { x.Id_Especialidade, x.Id_Projeto });
                    table.ForeignKey(
                        name: "FK_TB_Especialidade_Projeto_TB_Especialidades_Id_Especialidade",
                        column: x => x.Id_Especialidade,
                        principalTable: "TB_Especialidades",
                        principalColumn: "ID_Especialidade",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Especialidade_Projeto_TB_Projetos_Id_Projeto",
                        column: x => x.Id_Projeto,
                        principalTable: "TB_Projetos",
                        principalColumn: "Id_Projeto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Servicos",
                columns: table => new
                {
                    ID_Servico = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DS_Projeto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Projeto = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Servicos", x => x.ID_Servico);
                    table.ForeignKey(
                        name: "FK_TB_Servicos_TB_Projetos_ID_Projeto",
                        column: x => x.ID_Projeto,
                        principalTable: "TB_Projetos",
                        principalColumn: "Id_Projeto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Candidaturas",
                columns: table => new
                {
                    Id_Candidatura = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor_Proposto = table.Column<double>(type: "float", nullable: true),
                    DS_Proposta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duracao_Proposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Servico = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Candidaturas", x => x.Id_Candidatura);
                    table.ForeignKey(
                        name: "FK_TB_Candidaturas_TB_Servicos_ID_Servico",
                        column: x => x.ID_Servico,
                        principalTable: "TB_Servicos",
                        principalColumn: "ID_Servico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Contratos",
                columns: table => new
                {
                    Id_Contrato = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Data_Inicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data_Encerramento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_Servico = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Profissional = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Contratos", x => x.Id_Contrato);
                    table.ForeignKey(
                        name: "FK_TB_Contratos_TB_Profissionais_ID_Profissional",
                        column: x => x.ID_Profissional,
                        principalTable: "TB_Profissionais",
                        principalColumn: "Id_Profissional",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Contratos_TB_Servicos_Id_Contrato",
                        column: x => x.Id_Contrato,
                        principalTable: "TB_Servicos",
                        principalColumn: "ID_Servico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Candidaturas_ID_Servico",
                table: "TB_Candidaturas",
                column: "ID_Servico");
            
            migrationBuilder.CreateIndex(
                name: "IX_TB_Contratos_ID_Profissional",
                table: "TB_Contratos",
                column: "ID_Profissional");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Especialidade_Profissional_Id_Profissional",
                table: "TB_Especialidade_Profissional",
                column: "Id_Profissional");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Especialidade_Projeto_Id_Projeto",
                table: "TB_Especialidade_Projeto",
                column: "Id_Projeto");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Projetos_ID_Cliente",
                table: "TB_Projetos",
                column: "ID_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Servicos_ID_Projeto",
                table: "TB_Servicos",
                column: "ID_Projeto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Candidaturas");

            migrationBuilder.DropTable(
                name: "TB_Contratos");

            migrationBuilder.DropTable(
                name: "TB_Especialidade_Profissional");

            migrationBuilder.DropTable(
                name: "TB_Especialidade_Projeto");

            migrationBuilder.DropTable(
                name: "TB_Servicos");

            migrationBuilder.DropTable(
                name: "TB_Profissionais");

            migrationBuilder.DropTable(
                name: "TB_Especialidades");

            migrationBuilder.DropTable(
                name: "TB_Projetos");

            migrationBuilder.DropTable(
                name: "TB_Clientes");
        }
    }
}
