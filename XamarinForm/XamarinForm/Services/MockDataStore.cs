using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using XamarinForm.Models;

[assembly: Xamarin.Forms.Dependency(typeof(XamarinForm.Services.MockDataStore))]
namespace XamarinForm.Services
{
    public class MockDataStore : IDataStore<DataItem>
    {
        List<DataItem> items;

        public MockDataStore()
        {
            items = new List<DataItem>();
            var mockItems = new List<DataItem>
            {
                new DataItem ( Guid.NewGuid().ToString(),  "First item", "This is an item description." ),
                new DataItem ( Guid.NewGuid().ToString(),  "Second item", "This is an item description." ),
                new DataItem ( Guid.NewGuid().ToString(),  "Third item", "This is an item description." ),
                new DataItem ( Guid.NewGuid().ToString(),  "Fourth item", "This is an item description." ),
                new DataItem ( Guid.NewGuid().ToString(),  "Fifth item", "This is an item description." ),
                new DataItem ( Guid.NewGuid().ToString(),  "Sixth item", "This is an item description." ),
                new DataItem ( Guid.NewGuid().ToString(),  "First item", "This is an item description." ),
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(DataItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(DataItem item)
        {
            var _item = items.Where((DataItem arg) => arg.MenuItemId == item.MenuItemId).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(DataItem item)
        {
            var _item = items.Where((DataItem arg) => arg.MenuItemId == item.MenuItemId).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<DataItem> GetItemAsync(string MenuItemId)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.MenuItemId == MenuItemId));
        }

        public async Task<IList<DataItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}