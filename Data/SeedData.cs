using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Website___Ricardo.Models;

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

        internal static void PreencheDadosCV(ListaCV bd)
        {
            InsereEmpresa(bd);
            InsereCargo(bd);
            InsereCliente(bd);
        }

        private static void InsereCliente(ListaCV bd)
        {
            if (!bd.Cliente.Any(c => c.Email == NOME_UTILIZADOR_CLIENTE))
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

        private static void InsereEmpresa(ListaCV bd)
        {
            GaranteExisteEmpresa(bd, "Upskill");
            GaranteExisteEmpresa(bd, "Camel");
            GaranteExisteEmpresa(bd, "Quantum CBD");
        }
        private static void GaranteExisteEmpresa(ListaCV bd, string nome)
        {
            Empresa empresa = bd.Empresa.FirstOrDefault(c => c.Nome == nome);
            if (empresa == null)
            {
                empresa = new Empresa { Nome = nome };
                bd.Empresa.Add(empresa);
                bd.SaveChanges();
            }
        }


        private static void InsereCargo(ListaCV bd)
        {
            if (bd.Cargo.Any()) return;

            Empresa empresaUpskill = bd.Empresa.FirstOrDefault(c => c.Nome == "Upskill");
            Empresa empresaCamel = bd.Empresa.FirstOrDefault(c => c.Nome == "Camel");

            bd.Cargo.AddRange(new Cargo[]
            {
                new Cargo
                {
                    Titulo = "Estudante",
                    Descricao = "Estudante no curso Upskill",
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
        private static void InsereCargoTESTE(ListaCV bd)
        {
            Empresa empresaUpskill = bd.Empresa.FirstOrDefault(c => c.Nome == "UpSkill");

            for (int i = 0; i < 1000; i++)
            {
                bd.Cargo.Add(new Cargo
                {
                    Nome = "Cargo " + i,
                    Empresa = empresaUpskill,
                });
            }

            bd.SaveChanges();
        }

        internal static async Task InsereUtilizadorAsync(UserManager<IdentityUser> gestorUtilizadores)
        {
            IdentityUser cliente = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, NOME_UTILIZADOR_CLIENTE, "Pass321");
            await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, cliente, ROLE_CLIENTE);
        }
        internal static async Task InsereRolesAsync(RoleManager<IdentityRole> gestorRoles)
        {
            await CriaRoleSeNecessario(gestorRoles, ROLE_ADMINISTRADOR);
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
            await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, utilizador, ROLE_ADMINISTRADOR);
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
