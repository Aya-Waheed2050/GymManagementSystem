namespace DataAccess.Data.DataSeed
{
    public class GymContextSeeding
    {
        public static async Task<bool> SeedDateAsync(GymSystemDbContext dbContext)
        {
			try
			{
                var HasPlans = await dbContext.Plans.AnyAsync();
                var HasCategories = await dbContext.Categories.AnyAsync();


                if (!HasPlans)
                {
                    var Plans = await LoadDataFromJsonFileAsync<Plan>("plans.json");
                    
                    if (Plans.Any())
                    {
                        await dbContext.Plans.AddRangeAsync(Plans);
                    }
                }
                if (!HasCategories)
                {
                    var Categories = await LoadDataFromJsonFileAsync<Category>("categories.json");
                    if (Categories.Any())
                    {
                        await dbContext.Categories.AddRangeAsync(Categories);
                    }
                }
                return await dbContext.SaveChangesAsync() > 0;
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex);  
                throw;              
            }
        }

        private static async Task<List<T>> LoadDataFromJsonFileAsync<T>(string FileName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FileName);

            if (!File.Exists(FilePath))
                throw new FileNotFoundException("File Not Found", FilePath);

            string JsonData = await File.ReadAllTextAsync(FilePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<List<T>>(JsonData, options) ?? new List<T>();
        }



    }
}
