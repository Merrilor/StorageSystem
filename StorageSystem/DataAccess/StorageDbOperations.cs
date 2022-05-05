using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageSystem.Model;

namespace StorageSystem.DataAccess
{
    public static class StorageDbOperations
    {

        private static StorageEntities _Entities;


        static StorageDbOperations()
        {

            if(_Entities is null)
                _Entities = new StorageEntities();

        }


        public async static Task<bool> TryLoginUser(string login, string password)
        {

            var user = await _Entities.User.SingleOrDefaultAsync(u=> u.Login == login && u.Password == password);           

            if (user is null)
                return false;

            CurrentUser.Id = user.UserId;
            CurrentUser.Login = user.Login;
            CurrentUser.Role = (UserRole)Enum.Parse(typeof(UserRole), user.Role.Name);

            var currentDate = await _Entities.Database.SqlQuery<DateTime>("SELECT getdate()").ToAsyncEnumerable().First();

            user.LastLoginDate = currentDate;

            

            _Entities.LoginHistory.Add(new LoginHistory
            {
                LoginDatetime = currentDate,
                UserId = user.UserId

            });

            _Entities.SaveChanges();

            return true;


        }


        public async static Task<List<WarehouseUnit>> GetAvailableWarehouses()
        {

            return await _Entities.WarehouseUnit.Where(wh => wh.IsAvailable == true).ToListAsync();

        }


        public async static Task<List<WarehouseUnit>> GetAllWarehouses()
        {

            return await _Entities.WarehouseUnit.ToListAsync();

        }


    }
}
