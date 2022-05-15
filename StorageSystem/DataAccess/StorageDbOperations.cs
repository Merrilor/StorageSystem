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

        public  static void SaveChanges()
        {


            _Entities.SaveChanges();


        }


        public async static Task<bool> TryLoginUser(string login, string password)
        {

            var user = await _Entities.User.SingleOrDefaultAsync(u=> u.Login == login && u.Password == password);           

            if (user is null)
                return false;

            CurrentUser.Id = user.UserId;
            CurrentUser.Login = user.Login;
            CurrentUser.Role = (UserRole)Enum.Parse(typeof(UserRole), user.Role.Name);
            if(user.Photo != null)
            {
                CurrentUser.UserImagePath = user.Photo;
            }

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

        public static void LogoutUser()
        {

            var currentUser = _Entities.User.Single(u => u.UserId == CurrentUser.Id);

            currentUser.LoginHistory.First().ExitDatetime = _Entities.Database.SqlQuery<DateTime>("SELECT getdate()").AsEnumerable().First();



            SaveChanges();

        }


        public async static Task<List<WarehouseUnit>> GetAvailableWarehouses()
        {

            return await _Entities.WarehouseUnit.Where(wh => wh.IsAvailable == true).ToListAsync();

        }

        public async static Task<List<WarehouseUnit>> GetWarehouseRange(int minCode, int maxCode)
        {



            return await _Entities.WarehouseUnit
                .Where(wh => wh.Product.Code > minCode && wh.Product.Code < maxCode)
                .ToListAsync();

        }


        public static List<WarehouseUnit> GetAllWarehouses()
        {

            return _Entities.WarehouseUnit.ToList();

        }

        public async static Task<List<Role>> GetAllRoles()
        {

            return await _Entities.Role.ToListAsync();


        }

        public async static Task<WarehouseUnit> GetWarehouseUnit(int id)
        {

            return await _Entities.WarehouseUnit.SingleAsync(wh => wh.WarehouseUnitId == id);


        }


        public async static void AddNewUser(User newUser)
        {

            newUser.RegistrationDate = _Entities.Database.SqlQuery<DateTime>("SELECT getdate()").AsEnumerable().First();

            _Entities.User.Add(newUser);

            SaveChanges();


        }


    }
}
