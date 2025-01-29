using ColetorA41.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Utils
{
    public static class Ambiente
    {
        public static User UsuarioLogado { get; set; }
        public static bool UsuarioAutenticado { get; set; } = false;
        public static Empresa EmpresaSelecionada { get; set; } = new Empresa { CodEmpresa = "1" };
        public static Estabelecimento EstabelecimentoSelecionado { get; set; } = new Estabelecimento { codEstab = "101" };
        public static string VersaoAtual { get; set; }
        public static string PrefixoUrl
        {
            get
            {
                return @"https://hawebdev.dieboldnixdorf.com.br:8543/api/integracao/aat/v1/"; // Desenv
                
            }
        }
        public static string AliasAppServer { get => "interfcol"; }
        public static string UsuarioSenhaBase64 { get; set; }

    }
}
