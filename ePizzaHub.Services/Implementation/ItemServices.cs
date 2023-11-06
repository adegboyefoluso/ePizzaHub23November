using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementation
{
    public class ItemServices : Service<Item>, IItemService
    {
        private IRepository<Item> _repository;
        public ItemServices(IRepository<Item> itemrepo) : base(itemrepo)
        {
            _repository = itemrepo;
        }
        public IEnumerable<ItemModel> GetItems()
        {
            var data = _repository.GetAll().OrderBy(x => x.CategoryId).Select(u => new ItemModel
            {
                Id = u.Id,
                Name = u.Name,
                CategoryId = u.CategoryId,
                Description = u.Description,
                ImageUrl = u.ImageUrl,
                ItemTypeId = u.ItemTypeId,
                UnitPrice = u.UnitPrice


            });
            return data;
        }
    }
}
