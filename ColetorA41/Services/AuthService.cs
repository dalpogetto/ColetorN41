using ColetorA41.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Services
{
    public class AuthService
    {
        private const string AuthStateKey = "AuthState";
        private const string InfoLogin = "UsuarioSenhaBase64";
        private readonly TotvsService _service;

        public AuthService(TotvsService totvsService)
        {
            _service = totvsService;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(2000);

            var authState = Preferences.Default.Get<bool>(AuthStateKey, false);
            var dadosLogin = Preferences.Default.Get(InfoLogin, String.Empty);
            //Setar Informacoes Usuario e Senha para Chamar APIs TOTVS
            Ambiente.UsuarioSenhaBase64 = dadosLogin;

            return authState;
        }
        public async Task<bool> Login(string usuario, string senha)
        {
            var byteArray = new UTF8Encoding().GetBytes($"{usuario}:{senha}");
            Ambiente.UsuarioSenhaBase64 = Convert.ToBase64String(byteArray);

            var srv = _service;                ;
            try
            {
                
                if (!await srv.Login("101"))
                {
                    await Shell.Current.DisplayAlert("Erro!", "Usuário e senha inválidos", "OK");
                    return default;
                }
                

                Preferences.Default.Set<bool>(AuthStateKey, true);
                // var byteArray = new UTF8Encoding().GetBytes($"{usuario}:{senha}");
                // Ambiente.UsuarioSenhaBase64 = Convert.ToBase64String(byteArray);
                Preferences.Default.Set(InfoLogin, Ambiente.UsuarioSenhaBase64);
                return true;
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Erro!", "Usuário e senha inválidos", "OK");
                return false;
            }
            
        }
        public void Logout() 
        {
            Preferences.Default.Clear();
        }
    }
}
