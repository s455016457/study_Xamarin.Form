using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using XamarinForm.Models;

[assembly: Xamarin.Forms.Dependency(typeof(XamarinForm.Services.MockDataStore))]
namespace XamarinForm.Services
{
    public class MockDataStore : IDataStore<MenuItem>
    {
        List<MenuItem> items;

        public MockDataStore()
        {
            items = new List<MenuItem>();
            var mockItems = new List<MenuItem>
            {
                new MenuItem ( Guid.NewGuid().ToString(),  "First item", "This is an item description." ),
                new MenuItem ( Guid.NewGuid().ToString(),  "Second item", "This is an item description." ),
                new MenuItem ( Guid.NewGuid().ToString(),  "Third item", "This is an item description." ),
                new MenuItem ( Guid.NewGuid().ToString(),  "Fourth item", "This is an item description." ),
                new MenuItem ( Guid.NewGuid().ToString(),  "Fifth item", "This is an item description." ),
                new MenuItem ( Guid.NewGuid().ToString(),  "Sixth item", "This is an item description." ),
                new MenuItem ( Guid.NewGuid().ToString(),  "First item", "This is an item description." ),
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(MenuItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(MenuItem item)
        {
            var _item = items.Where((MenuItem arg) => arg.MenuItemId == item.MenuItemId).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(MenuItem item)
        {
            var _item = items.Where((MenuItem arg) => arg.MenuItemId == item.MenuItemId).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<MenuItem> GetItemAsync(string MenuItemId)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.MenuItemId == MenuItemId));
        }

        public async Task<IEnumerable<MenuItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}