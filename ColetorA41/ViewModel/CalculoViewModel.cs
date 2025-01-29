using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ColetorA41.Models;

using ColetorA41.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ColetorA41.Views.Calculo;

namespace ColetorA41.ViewModel
{
    public partial class CalculoViewModel : BaseViewModel
    {
        private readonly TotvsService _service;
        private readonly TotvsService46 _service46;
       
      
        public CalculoViewModel(TotvsService totvsService, 
                                TotvsService46 totvsService46)
        {
            _service = totvsService;
            _service46 = totvsService46;
            this.ObterEstabelecimentos();
        }

        private Estabelecimento _estabSelecionado;
        public Estabelecimento EstabSelecionado
        {
            get => _estabSelecionado;
            set
            {
                if (_estabSelecionado != value)
                {
                    _estabSelecionado = value;
                     this.ObterTecnicosEstab();
                     this.ObterTransporte();
                }
            }
        }

        private Tecnico _tecnicoSelecionado;
        public Tecnico TecnicoSelecionado
        {
            get => _tecnicoSelecionado;
            set
            {
                if (_tecnicoSelecionado != value)
                {
                    _tecnicoSelecionado = value;
                    this.ObterEntrega();
                    
                }
            }
        }

        public async void ObterEstabelecimentos()
        {
            this.IsBusy = true;
            var lista = await _service.ObterEstabelecimentos();

            this.listaEstab.Clear();
            foreach (var item in lista) 
            {
                this.listaEstab.Add(item);
            }
            this.IsBusy = false;
        }

        
        [ObservableProperty]
        int nrProcessSelecionado;

        public ObservableCollection<Estabelecimento> listaEstab { get; private set; } = new();

        public async void ObterTecnicosEstab()
        {
            this.IsBusy = true;
            var lista = await _service.ObterTecEstab(this._estabSelecionado.codEstab);

            this.listaTecnico.Clear();
            foreach (var item in lista)
            {
                this.listaTecnico.Add(item);
            }
            this.IsBusy = false;
        }

        public ObservableCollection<Tecnico> listaTecnico { get; private set; } = new();

        public ObservableCollection<Enc> listaEnc { get; private set; } = new();

        [ObservableProperty]
        string etiquetaEnc;

        //Dados da Nota
        [ObservableProperty]
        string serieEntra;

        [ObservableProperty]
        string serieSaida;

        [ObservableProperty]
        string rpw;

        [ObservableProperty]
        string entrega;

        [ObservableProperty]
        Transporte transpEntraSelecionado;

        [ObservableProperty]
        ParamEstabel parametroSelecionado;

        [ObservableProperty]
        Transporte transpSaidaSelecionado;

        [ObservableProperty]
        Entrega entregaSelecionada;

        //Login Almoxarifado
        [ObservableProperty]
        int usuarioAlmoxa;

        [ObservableProperty]
        string senhaAlmoxa;

        [ObservableProperty]
        string tipoCalculo;

        [ObservableProperty]
        string numEnc;

        

        public ObservableCollection<Transporte> listaTransporte { get; private set; } = new();

        public async void ObterTransporte()
        {
            this.IsBusy = true;
            var lista = await _service.ObterTransportes();

            this.listaTransporte.Clear();
            foreach (var item in lista)
            {
                this.listaTransporte.Add(item);
            }
            this.IsBusy = false;
        }

        public ObservableCollection<Entrega> listaEntrega { get; private set; } = new();

        public async void ObterEntrega()
        {
            this.IsBusy = true;
            var lista = await _service.ObterEntrega(this.TecnicoSelecionado.codTec, this.EstabSelecionado.codEstab);

            this.listaEntrega.Clear();
            foreach (var item in lista)
            {
                this.listaEntrega.Add(item);
            }
            this.IsBusy = false;
        }

        public async void ObterParametrosEstab()
        {
            this.IsBusy = true;
            var parametro = await _service.ObterParametrosEstab(this._estabSelecionado.codEstab);
            if (parametro != null)
            {
                this.TranspEntraSelecionado = this.listaTransporte.Where(item => item.codTransp == parametro.codTranspEntra).FirstOrDefault();
                this.TranspSaidaSelecionado = this.listaTransporte.Where(item => item.codTransp == parametro.codTranspSai).FirstOrDefault();
                this.EntregaSelecionada = this.listaEntrega.Where(item => item.codEntrega == parametro.codEntrega).FirstOrDefault();
                this.SerieEntra = parametro.serieEntra;
                this.SerieSaida = parametro.serieSai;
                this.Entrega = parametro.codEntrega;
                this.Rpw = parametro.rpw;
            }
            this.IsBusy = false;
        }

        //Extrakit
        [RelayCommand]
        private void SelecionarTodosExtrakit()
        {
            //this.listaExtrakitSelecionados.Clear();
            foreach (var item in this.listaExtrakit)
            {
                this.listaExtrakitSelecionados.Add(item);
            }
        }

        public ObservableCollection<Extrakit> listaExtrakit { get; private set; } = new();
        public ObservableCollection<Extrakit> listaExtrakitSelecionados { get;  set; } = new ();
        public async void ObterExtrakit()
        {
            this.IsBusy = true;
            this.listaExtrakit.Clear();
            var lista = await _service.ObterExtrakit(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec, this.entregaSelecionada.nrProcesso);
            foreach (var item in lista)
            {
                this.listaExtrakit.Add(item);
            }
            this.IsBusy = false;
        }

        public async void ObterDados()
        {
            this.IsBusy = true;
            //Gerar Numero de Processo se for preciso
            var lista = await _service46.ObterDados(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec);
            //Obter Numero Gerado
            this.NrProcessSelecionado = await _service.ObterNrProcesso(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec);
            this.IsBusy = false;
        }

        //Calculo
        public ObservableCollection<Calculo> listaCalculo { get; private set; } = new();
        public ObservableCollection<Semsaldo> listaSemSaldo { get; private set; } = new();
        public ObservableCollection<Models.Resumo> listaResumo { get; private set; } = new();

        public async Task<bool> LoginAlmoxa()
        {
            this.IsBusy = true;
            var ok = await _service.LoginAlmoxa(this.EstabSelecionado.codEstab,
                                                this.UsuarioAlmoxa,
                                                this.SenhaAlmoxa);
            this.IsBusy = false;
            return ok;
        }

        public async void PrepararCalculo()
        {
            this.IsBusy = true;
            //Gerar Numero de Processo se for preciso
            var calculo = await _service.PrepararCalculo(this.EstabSelecionado.codEstab, 
                                                         this.TecnicoSelecionado.codTec, 
                                                         this.NrProcessSelecionado, 
                                                         this.listaExtrakitSelecionados.ToList());

            //Montar Calculo
            this.listaCalculo.Clear();
            foreach (var item in calculo.items)
            {
                this.listaCalculo.Add(item);
            }

            //Montar Sem Saldo
            this.listaSemSaldo.Clear();
            foreach (var item in calculo.semsaldo)
            {
                this.listaSemSaldo.Add(item);
            }

            //Montar Resumo
            this.MontarResumo();

            this.IsBusy = false;
        }

        public void MontarResumo()
        {
          
            this.listaResumo.Clear();
            this.listaResumo.Add(new()
            {
                id = 1,
                titulo = "Visão Geral",
                descricao = $"{1}ETs fora do processo",
                quantidade = this.listaCalculo.Count(item => !item.soEntrada)
            });

            this.listaResumo.Add(new()
            {
                id = 2,
                titulo = "Renovações",
                descricao = "Saídas e Entradas",
                quantidade = this.listaCalculo.Count(item => item.qtRenovar > 0)
            });

            this.listaResumo.Add(new()
            {
                id = 3,
                titulo = "Pagamentos",
                descricao = "Saídas",
                quantidade = this.listaCalculo.Count(item => item.qtPagar > 0 && !item.soEntrada)
            });

            this.listaResumo.Add(new()
            {
                id = 4,
                titulo = "Somente Entrada",
                descricao = "Entradas + ET + Ruins",
                quantidade = this.listaCalculo.Count(item => item.soEntrada)
            });

            this.listaResumo.Add(new()
            {
                id = 5,
                titulo = "Extrakit",
                descricao = "De Kit para ET",
                quantidade = this.listaCalculo.Count(item => item.qtExtrakit > 0)
            });

            this.listaResumo.Add(new()
            {
                id = 6,
                titulo = "Sem Saldo",
                descricao = "Itens/Alternativos Sem Saldo",
                quantidade = this.listaSemSaldo.Count(item=>item.qtPagar > 0)
            });
        }

        public Command SelectedChangedCommand
        {
            get
            {
                return new Command((sender) =>
                {
                   // Extrakit et = sender as Extrakit;

                    //Console.WriteLine("Select item in Colletionview" + et.itCodigo);
                });
            }
        }

        [RelayCommand]
        private void SelectionChanged()
        {
            foreach (var p in listaExtrakitSelecionados)
            {
                if (p is Extrakit person)
                {
                    Console.WriteLine($"{person.itCodigo} is selected");
                }
            }
        }

        [RelayCommand]
        async Task ChamarEstabTec()
        {
            //await Shell.Current.DisplayAlert("Aqui", "Entrou", "OK");
            await Shell.Current.GoToAsync($"/{nameof(EstabTec)}");
        }

        [RelayCommand]
        async Task ChamarResumoDetalhe()
        {
            await Shell.Current.GoToAsync($"{nameof(ResumoDetalhe)}");
        }

        [RelayCommand]
        async Task ChamarDadosNF()
        {
            await Shell.Current.GoToAsync($"{nameof(DadosNF)}");
        }

        [RelayCommand]
        async Task ChamarExtrakit()
        {
            //await Shell.Current.DisplayAlert("Aqui", "Entrou", "OK");
            await Shell.Current.GoToAsync($"{nameof(ExtrakitView)}");
        }

        [RelayCommand]
        async Task ChamarResumo()
        {
            await Shell.Current.GoToAsync($"{nameof(Views.Calculo.Resumo)}");
        }

        [RelayCommand]
        async Task ChamarLeituraENC()
        {
            await Shell.Current.GoToAsync($"{nameof(LeituraENC)}");
        }

        [RelayCommand]
        async Task LeituraENC(string numEnc)
        {
            //var lista = await _service46.LeituraEnc(this.EstabSelecionado.codEstab, this.TecnicoSelecionado.codTec, numEnc, this.entregaSelecionada.nrProcesso.ToString());
            //await Shell.Current.DisplayAlert("Aqui", numEnc, "OK");
            this.Mock();
            this.NumEnc = string.Empty;
        }

        public async void Mock()
        {
            this.IsBusy = true;
            this.listaEnc.Add(new Enc {numEnc="12345", chamado = "11111", flag = "OK", itCodigo = "85.111.00024-2b", numOS=8540, mensagem="Mensagem: OK" });
            this.listaEnc.Add(new Enc {numEnc = "6789", chamado = "", flag = "ERRO", itCodigo = "ENC inválida", itDescricao = "", numOS = 0, mensagem = "" });
            this.IsBusy = false;
        }




    }
}
