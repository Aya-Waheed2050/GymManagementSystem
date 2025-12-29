namespace DataAccess.Data.DataSeed
{
    public class IdentityDbContextSeeding
    {
        public static async Task<bool> SeedDataAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var HasUsers = await userManager.Users.AnyAsync();
                var HasRoles = await roleManager.Roles.AnyAsync();

                if (HasUsers && HasRoles) 
                    return false;

                if (!HasRoles)
                {
                var Roles = new List<IdentityRole>()
                {
                    new () { Name = "Admin"},
                    new () { Name = "SuperAdmin"},
                };

                    foreach (var role in Roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role.Name!))
                        {
                            await roleManager.CreateAsync(role);
                        }
                    }
                }

                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser
                    {
                        FirstName = "Super",
                        LastName = "Admin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "01029780971",

                    };
                    await userManager.CreateAsync(MainAdmin, "P@ssw0rd");
                    await userManager.AddToRoleAsync(MainAdmin, "SuperAdmin");

                    var Admin = new ApplicationUser
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        UserName = "Admin",
                        Email = "Admin@gmail.com",
                        PhoneNumber = "01142133508",

                    };
                    await userManager.CreateAsync(Admin, "P@ssw0rd");
                    await userManager.AddToRoleAsync(Admin, "Admin");
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed : {ex}");
                return false;
            }
        }
    }
}
