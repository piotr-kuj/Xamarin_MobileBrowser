using Emby.ApiClient.Data;
using MediaBrowser.Model.Sync;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace heymobile.Web
{
    public class Item
    {
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class ItemRepository : IItemRepository
    {
        private static ConcurrentDictionary<string, Item> items =
            new ConcurrentDictionary<string, Item>();

        
        public ItemRepository()
        {
       
        }

        Task IItemRepository.AddOrUpdate(LocalItem item)
        {
            throw new NotImplementedException();
        }

        Task IItemRepository.Delete(string id)
        {
            throw new NotImplementedException();
        }

        Task<LocalItem> IItemRepository.Get(string id)
        {
            throw new NotImplementedException();
        }

        Task<List<LocalItemInfo>> IItemRepository.GetAlbumArtists(string serverId, string userId)
        {
            throw new NotImplementedException();
        }

        Task<List<LocalItem>> IItemRepository.GetItems(LocalItemQuery query)
        {
            throw new NotImplementedException();
        }

        Task<List<string>> IItemRepository.GetItemTypes(string serverId, string userId)
        {
            throw new NotImplementedException();
        }

        Task<List<LocalItemInfo>> IItemRepository.GetPhotoAlbums(string serverId, string userId)
        {
            throw new NotImplementedException();
        }

        Task<List<string>> IItemRepository.GetServerItemIds(string serverId)
        {
            throw new NotImplementedException();
        }

        Task<List<LocalItemInfo>> IItemRepository.GetTvSeries(string serverId, string userId)
        {
            throw new NotImplementedException();
        }

    }

        public class Startup
        {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IItemRepository, ItemRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        }
}
