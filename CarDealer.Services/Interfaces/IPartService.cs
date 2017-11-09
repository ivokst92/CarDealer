﻿namespace CarDealer.Services.Interfaces
{
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;

    public interface IPartService
    {
        IEnumerable<PartModel> All(int page, int size = 25);

        int Total();
        void Create(string name, double price, int quantity, int supplierId);
    }
}