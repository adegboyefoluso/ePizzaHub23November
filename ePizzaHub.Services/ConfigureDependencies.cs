using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Repositories.Implementation;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Implementation;
using ePizzaHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services
{
    //dependency registring  services
   public static class ConfigureDependencies
    {
        public  static void  RegisteredServices(IServiceCollection services, IConfiguration configuratin) 
        {

            //database
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(configuratin.GetConnectionString("DefaultConnection"));
            });

            //repository
            services.AddScoped<IRepository<CartItem>,Repository<CartItem>>();
            services.AddScoped<IRepository<PaymentDetail>, Repository<PaymentDetail>>();


            services.AddScoped<IRepository<User>, Repository<User>>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRepository<Item>, Repository<Item>>();

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //Services

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IItemService, ItemServices>();
            services.AddScoped<ICartServices, CartService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();

        }
    }
}
