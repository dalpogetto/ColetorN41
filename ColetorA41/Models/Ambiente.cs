using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Models
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
                return "https://hawebdev.dieboldnixdorf.com.br:8543/";        // Desenv
                // return "https://hawebdev.dieboldnixdorf.com.br:8543/";     // -> Projetos
                // return "https://brspupapl01.ad.diebold.com:8243/";     // -> Homolog
                //return "https://totvsapp.dieboldnixdorf.com.br:8143/";  // -> Produção 
            }
        }
        public static string AliasAppServer { get => "interfcol"; }
        public static string UsuarioSenhaBase64 { get; set; }

    }
}
