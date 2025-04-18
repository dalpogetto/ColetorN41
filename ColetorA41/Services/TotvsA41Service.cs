﻿using ColetorA41.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Services
{
    public class TotvsA41Service : BaseService
    {
        public TotvsA41Service(IHttpClientFactory httpClientFactory, IConfiguration config) : base(httpClientFactory, config)
        {
        }

        public async Task<List<Estabelecimento>?> ObterEstabelecimentos()
        {
            var lista = await GetAsync<List<Estabelecimento>>("ObterEstab");
            return lista;
        }

        public async Task<List<Estabelecimento>?> ObterEmitentesDoEstabelecimento(string id)
        {
            return await GetAsync<List<Estabelecimento>>($"ObterTecEstab/{id}");
        }

    }
}
