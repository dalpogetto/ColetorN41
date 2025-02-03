using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ColetorA41.Models
{
    public class LabelResumo: INotifyPropertyChanged
    {
        private int geral;
        private int geralExtrakit;
        private int pagto;
        private int renovacao;
        private int somenteEntrada;
        private int extrakit;
        private int semSaldo;

        public int Geral { get => geral; set => SetProperty(ref geral, value); }

        public int GeralExtrakit { get => geralExtrakit; set => SetProperty(ref geralExtrakit, value); }
        public int Pagto { get => pagto; set => SetProperty(ref pagto, value); }

        public int Renovacao { get => renovacao; set => SetProperty(ref renovacao, value); }

        public int SomenteEntrada { get => somenteEntrada; set => SetProperty(ref somenteEntrada, value); }

        public int Extrakit { get => extrakit; set => SetProperty(ref extrakit, value); }

        public int SemSaldo { get => semSaldo; set => SetProperty(ref semSaldo, value); }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    
    }
