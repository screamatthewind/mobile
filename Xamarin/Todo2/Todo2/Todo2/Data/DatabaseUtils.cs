using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using App1;

namespace Todo2
{
	public class DatabaseUtils
	{
		readonly SQLiteAsyncConnection database;

		public DatabaseUtils(string dbPath)
		{
			database = new SQLiteAsyncConnection(dbPath);
		}

		//public Task<List<TodoItem>> GetItemsAsync()
		//{
		//	return database.Table<TodoItem>().ToListAsync();
		//}

		//public Task<List<TodoItem>> GetItemsNotDoneAsync()
		//{
		//	return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
		//}

		//public Task<TodoItem> GetItemAsync(int id)
		//{
		//	return database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
		//}

		//public Task<int> SaveItemAsync(TodoItem item)
		//{
		//	if (item.ID != 0)
		//	{
		//		return database.UpdateAsync(item);
		//	}
		//	else {
		//		return database.InsertAsync(item);
		//	}
		//}

		//public Task<int> DeleteItemAsync(TodoItem item)
		//{
		//	return database.DeleteAsync(item);
		//}

        public Task<int> SaveLocationAsync(Location location)
        {
			if (location.Id != 0)
			{
				return database.UpdateAsync(location);
			}
			else {
				return database.InsertAsync(location);
			}
        }

        public Task<int> DeleteLocationAsync(Location location)
		{
			return database.DeleteAsync(location);
		}

        public Task<int> SaveContactAsync(Contact contact)
        {
			if (contact.Id != 0)
			{
				return database.UpdateAsync(contact);
			}
			else {
				return database.InsertAsync(contact);
			}
        }

        public Task<int> DeleteContactAsync(Contact contact)
		{
			return database.DeleteAsync(contact);
		}

        public Task<List<Location>> getLocationsAsync()
		{
			return database.QueryAsync<Location>("SELECT * FROM [Location] ORDER BY locationName");
		}

        public Task<List<Contact>> getContactsByLocationIdAsync(long id)
		{
            return database.Table<Contact>().Where(i => i.locationId == id).ToListAsync();
		}

        public void ResetLocationContactDatabase()
        {
            database.DropTableAsync<Location>().Wait();
            database.DropTableAsync<Contact>().Wait();

            database.CreateTableAsync<Location>().Wait();
            database.CreateTableAsync<Contact>().Wait();
        }
        
    }
}

