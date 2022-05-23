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
     
        private static bool _Loaded = false;

        private static StorageEntities _persistentEntities = new StorageEntities();
       
        static StorageDbOperations()
        {

            

        }

        public async static Task SavePersistentChanges()
        {

            await _persistentEntities.SaveChangesAsync();

        }
     

        public async  static Task SaveChanges(StorageEntities entities)
        {


            await entities.SaveChangesAsync();


        }


        public async static Task<bool> TryLoginUser(string login, string password)
        {

            using(var entities = EntityProvider.CreateEntities())
            {


                var user = await entities.User.SingleOrDefaultAsync(u => u.Login == login && u.Password == password);

                if (user is null)
                    return false;

                CurrentUser.Id = user.UserId;
                CurrentUser.Login = user.Login;
                CurrentUser.Role = (UserRole)Enum.Parse(typeof(UserRole), user.Role.Name);
                if (user.Photo != null)
                {
                    CurrentUser.UserImagePath = user.Photo;
                }

                var currentDate = await entities.Database.SqlQuery<DateTime>("SELECT getdate()").ToAsyncEnumerable().First();

                user.LastLoginDate = currentDate;



                entities.LoginHistory.Add(new LoginHistory
                {
                    LoginDatetime = currentDate,
                    UserId = user.UserId

                });

                await SaveChanges(entities);

                return true;


            }


        }

        public static void LogoutUser()
        {

            using (var entities = EntityProvider.CreateEntities())
            {


                if (CurrentUser.Id == 0)
                    return;

                var currentUser = entities.User.Single(u => u.UserId == CurrentUser.Id);

                currentUser.LoginHistory.First().ExitDatetime = entities.Database.SqlQuery<DateTime>("SELECT getdate()").AsEnumerable().First();



                 entities.SaveChanges();

            }

        }


        public async static Task<List<WarehouseUnit>> GetAvailableWarehouses()
        {
            using (var entities = EntityProvider.CreateEntities())
            {

                return await entities.WarehouseUnit.Where(wh => wh.IsAvailable == true).ToListAsync();

            }
        }

        public async static Task<List<WarehouseUnit>> GetWarehouseRange(int minCode, int maxCode)
        {

            

                return await _persistentEntities.WarehouseUnit
                .Where(wh => wh.Product.Code > minCode && wh.Product.Code < maxCode)
                .ToListAsync();

            

        }


        public static List<WarehouseUnit> GetAllWarehouses()
        {
            using (var entities = EntityProvider.CreateEntities())
            {

                return entities.WarehouseUnit.ToList();

            }

        }

        public async static Task<List<Role>> GetAllRoles()
        {
            using (var entities = EntityProvider.CreateEntities())
            {
                return await entities.Role.ToListAsync();
            }

        }

        public async static Task<WarehouseUnit> GetWarehouseUnit(int id)
        {
            using (var entities = EntityProvider.CreateEntities())
            {

                return await entities.WarehouseUnit.SingleAsync(wh => wh.WarehouseUnitId == id);

            }
        }

        public static async Task<List<Product>> GetProductRange(int minCode, int maxCode)
        {
            using (var entities = EntityProvider.CreateEntities())
            {

                return await entities.Product.Where(p => p.Code >= minCode && p.Code <= maxCode)
                    .Include(p=> p.ProductImage)
                    .Include(p=> p.ProductCategory)
                    .Include(p=> p.ProductType)
                    .Include(p=> p.ProductCategory.Select(pc=> pc.Category))
                    .ToListAsync();


            }
        }


        public async static void AddNewUser(User newUser)
        {
            using (var entities = EntityProvider.CreateEntities())
            {

                newUser.RegistrationDate = entities.Database.SqlQuery<DateTime>("SELECT getdate()").AsEnumerable().First();

                entities.User.Add(newUser);

                await SaveChanges(entities);

            }
            
        }


        public async static Task AddNewProduct(Product newProduct)
        {
            using (var entities = EntityProvider.CreateEntities())
            {

                entities.Product.Add(newProduct);

                await SaveChanges(entities);

            }


        }

        public async static Task AddNewProduct(Product newProduct, List<ProductImage> productImages, List<ProductCategory> productCategories)
        {

            using (var entities = EntityProvider.CreateEntities())
            {
                entities.Product.Add(newProduct);

                foreach (var image in productImages)
                {
                    image.ProductId = newProduct.ProductId;

                    entities.ProductImage.Add(image);

                }

                foreach (var category in productCategories)
                {
                    category.ProductId = newProduct.ProductId;

                    entities.ProductCategory.Add(category);
                }


                await SaveChanges(entities);

            }
        }

        public async static Task EditProduct(Product editedProduct, List<ProductImage> newProductImages, List<ProductCategory> newProductCategories)
        {
            using (var entities = EntityProvider.CreateEntities())
            {

                entities.Entry(editedProduct).State = EntityState.Modified;

                var oldProductImages = entities.ProductImage.Where(pi => pi.ProductId == editedProduct.ProductId).ToList();
                entities.ProductImage.RemoveRange(oldProductImages);

                var oldCategories = entities.ProductCategory.Where(pc => pc.ProductId == editedProduct.ProductId).ToList();
                entities.ProductCategory.RemoveRange(oldCategories);

                await SaveChanges(entities);

                foreach (var image in newProductImages)
                {
                    
                    image.ProductId = editedProduct.ProductId;
                    entities.ProductImage.Add(image);

                }

                await SaveChanges(entities);

                foreach (var category in newProductCategories)
                {
                    category.ProductId = editedProduct.ProductId;
                    category.Product = null;
                    category.Category = null;

                    entities.ProductCategory.Add(category);
                }


                await SaveChanges(entities);

            }
        }

        public async static Task< List<string>> GetAllBrands()
        {
            using (var entities = EntityProvider.CreateEntities())
            {

                return await entities.Product.Select(p => p.BrandName).Distinct().ToListAsync();

            }

        }

        public async static Task<List<ProductType>> GetAllProductTypes()
        {
            using (var entities = EntityProvider.CreateEntities())
            {

                return await entities.ProductType.ToListAsync();
            }
        }

        public async  static Task<Product> GetLastProduct()
        {
            using (var entities = EntityProvider.CreateEntities())
            {

                return await entities.Product.OrderByDescending(p => p.Code).FirstAsync();

            }
        }

        public async static Task<Product> GetProduct(int productId)
        {
            using (var entities = EntityProvider.CreateEntities())
            {
                return await entities.Product
                    .Include(p=> p.ProductCategory)
                    .Include(p=> p.ProductType)
                    .Include(p=> p.ProductImage)
                    .Include(p=> p.ProductCategory.Select(pc=> pc.Category))
                    .SingleOrDefaultAsync(p => p.ProductId == productId);

            }
        }

        public async static Task<List<Category>> GetCategories()
        {

            using (var entities = EntityProvider.CreateEntities())
            {
                return await entities.Category.ToListAsync();                   
            }


        }

        public async static Task<bool> TryAddNewCategory(Category newCategory)
        {

            using (var entities = EntityProvider.CreateEntities())
            {

                if(entities.Category.Where(c=> c.Name == newCategory.Name).Count() != 0)
                {
                    return false;
                }

                entities.Category.Add(newCategory);

                await SaveChanges(entities);

                return true;

            }
        }

        public async static Task<List<User>> GetAllUsers()
        {

            using (var entities = EntityProvider.CreateEntities())
            {

                return await entities.User.Include(u => u.Role).ToListAsync();

            }


        }


       

    }
}
