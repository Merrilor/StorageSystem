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

        private static bool _Loaded = false;
       

        static StorageDbOperations()
        {

            if(_Entities is null)
                _Entities = new StorageEntities();

        }
     

        public async  static Task SaveChanges()
        {


            await _Entities.SaveChangesAsync();


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

            if (CurrentUser.Id == 0)
                return;

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

        public static async Task<List<Product>> GetProductRange(int minCode, int maxCode)
        {


            return await _Entities.Product.Where(p => p.Code >= minCode && p.Code <= maxCode).ToListAsync();



        }


        public async static void AddNewUser(User newUser)
        {

            newUser.RegistrationDate = _Entities.Database.SqlQuery<DateTime>("SELECT getdate()").AsEnumerable().First();

            _Entities.User.Add(newUser);

            SaveChanges();

            
        }


        public async static Task AddNewProduct(Product newProduct)
        {


            _Entities.Product.Add(newProduct);

            await SaveChanges();


        }

        public async static Task EditProduct(Product editedProduct)
        {

            await SaveChanges();


        }

        public async static Task< List<string>> GetAllBrands()
        {

            return await _Entities.Product.Select(p => p.BrandName).Distinct().ToListAsync();


        }

        public async static Task<List<ProductType>> GetAllProductTypes()
        {

            return await _Entities.ProductType.ToListAsync();

        }

        public  static Product GetLastProduct()
        {
            return  _Entities.Product.OrderByDescending(p=> p.Code).First();
        }

        public async static Task<Product> GetProduct(int productId)
        {

            return await _Entities.Product.SingleOrDefaultAsync(p => p.ProductId == productId); 


        }


    }
}
