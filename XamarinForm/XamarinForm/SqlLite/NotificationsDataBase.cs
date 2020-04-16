using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinForm.DependencyServices;

namespace XamarinForm.SqlLite
{
    /// <summary>
    /// 通知数据库
    /// </summary>
    public class NotificationsDataBase
    {
        SQLiteAsyncConnection database;
        public NotificationsDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Notification>().Wait();
        }

        /// <summary>
        /// 获取所有通知
        /// </summary>
        /// <returns></returns>
        public Task<List<Notification>> GetItemsAsync()
        {
            return database.Table<Notification>().ToListAsync();
        }

        /// <summary>
        /// 获取未完成的通知
        /// </summary>
        /// <returns></returns>
        public Task<List<Notification>> GetItemsNotDoneAsync()
        {
            return database.Table<Notification>().Where(p=>!p.IsScheduled).ToListAsync();
        }
        /// <summary>
        /// 获取特定的通知
        /// </summary>
        /// <param name="id">通知ID</param>
        /// <returns></returns>
        public Task<Notification> GetItemAsync(int id)
        {
            return database.Table<Notification>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 更改特定的通知
        /// </summary>
        /// <param name="item">通知</param>
        /// <returns></returns>
        public Task<int> SaveItemAsync(Notification item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        /// <summary>
        /// 删除特定的通知
        /// </summary>
        /// <param name="item">通知</param>
        /// <returns></returns>
        public Task<int> DeleteItemAsync(Notification item)
        {
            return database.DeleteAsync(item);
        }
    }
}
