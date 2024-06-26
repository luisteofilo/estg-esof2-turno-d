﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dtos.Taxonomias;

namespace Frontend.Services
{
    public interface ITaxonomiasService
    {
        Task<List<VerticalDto>> GetTaxonomiasAsync();
    }
}