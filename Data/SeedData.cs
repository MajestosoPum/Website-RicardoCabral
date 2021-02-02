using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website___Ricardo.Data
{
    public class SeedData
    {
        private const string NOME_UTILIZADOR_ADMIN = "admin@ipg.pt";
        private const string PASSWORD_UTILIZADOR_ADMIN = "Pass321";
        private const string NOME_UTILIZADOR_CLIENTE = "teste1@ipg.pt";

        private const string ROLE_ADMINISTRADOR = "Administrador";
        private const string ROLE_CLIENTE = "Cliente";
        private const string ROLE_GESTOR_INFO = "GestorInformação";

        internal static void PreencheDadosCV (ListaCVContext bd)
        {
            InsereEmpresa(bd);
            InsereCargo(bd);
            InsereCliente(bd);
        }

        private static void InsereCliente (ListaCVContext bd)
        {
            if(!bd.Cliente.Any(char => c.Email == NOME_UTILIZADOR_CLIENTE))
            {
                Cliente c = new Cliente
                {
                    Nome = "Teste",
                    Email = NOME_UTILIZADOR_CLIENTE,
                    Telefone = "911231231"
                };

                bd.Cliente.Add(c);
                bd.SaveChanges();
            }
        }

        private static void GaranteExisteEmpresa(ListaCVContext bd, string nome)
        {
            Empresa empresa = bd.Empresa.FirstOrDefault(c => c.Nome == nome);
            if (empresa == null)
            {
                empresa = new Empresa { Nome = nome };
                bd.Empresa.Add(empresa);
                bd.SaveChanges();
            }
        }
        private static void InsereCargo (ListaCVContext bd)
        {
            if (bd.Cargo.Any()) return;

            Empresa empresaUpskill = bd.Empresa.FirstOrDefault(c => c.Nome == "Upskill");
            Empresa empresaCamel = bd.Empresa.FirstOrDefault(c => c.Nome == "Camel");

            bd.Cargo.AddRange(new Cargo[]
            {
                new Cargo
                {
                    Nome = "Estudante",
                    Descricao = "Estudante EXTREMAMENTE aplicado no curso Upskill",
                    Empresa = empresaUpskill
                },
                new Cargo
                {
                    Nome = "Distribuidor",
                    Descricao = "Distribuidor de produtos da marca Camel",
                    Empresa = empresaCamel
                }
            });

            bd.SaveChanges();
        }

        internal static async Task InsereUtilizadorAsync(UserManager<IdentityUser> gestorUtilizadores)
        {
            IdentityUser cliente = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, NOME_UTILIZADOR_CLIENTE, "Pass321");
            await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, cliente, ROLE_CLIENTE);       
        }
        internal static async Task InsereRolesAsync(RoleManager<IdentityRole> gestorRoles)
        {
            await CriaRoleSeNecessario(gestorRoles, ROLE_ADIMINISTRADOR);
            await CriaRoleSeNecessario(gestorRoles, ROLE_CLIENTE);                      
        }

        private static async Task CriaRoleSeNecessario(RoleManager<IdentityRole> gestorRoles, string funcao)
        {
            if (!await gestorRoles.RoleExistsAsync(funcao))
            {
                IdentityRole role = new IdentityRole(funcao);
                await gestorRoles.CreateAsync(role);
            }
        }
        internal static async Task InsereAdministradorAsync(UserManager<IdentityUser> gestorUtilizadores)
        {
            IdentityUser utilizador = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, NOME_UTILIZADOR_ADMIN, PASSWORD_UTILIZADOR_ADMIN);
            await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, utilizador, ROLE_ADIMINISTRADOR);
        }

        private static async Task AdicionaUtilizadorRoleSeNecessario(UserManager<IdentityUser> gestorUtilizadores, IdentityUser utilizador, string role)
        {
            if (!await gestorUtilizadores.IsInRoleAsync(utilizador, role))
            {
                await gestorUtilizadores.AddToRoleAsync(utilizador, role);
            }
        }
        private static async Task<IdentityUser> CriaUtilizadorSeNaoExiste(UserManager<IdentityUser> gestorUtilizadores, string nomeUtilizador, string password)
        {
            IdentityUser utilizador = await gestorUtilizadores.FindByNameAsync(nomeUtilizador);

            if (utilizador == null)
            {
                utilizador = new IdentityUser(nomeUtilizador);
                await gestorUtilizadores.CreateAsync(utilizador, password);
            }

            return utilizador;
        }
    }
}
