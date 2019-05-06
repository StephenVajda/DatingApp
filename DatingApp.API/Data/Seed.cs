using System.Collections.Generic;
using System.IO;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }
       
       public void SeedUsers(){
           try{
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users=JsonConvert.DeserializeObject<List<User>>(userData);

                foreach(var user in users){
                    byte[] passwordHash , passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);
                    user.PasswordHash=passwordHash;
                    user.PasswordSalt=passwordSalt;
                    user.Username=user.Username.ToLower();
                }
                _context.SaveChanges();
           }
            catch (DirectoryNotFoundException e)
        {
            System.Console.WriteLine($"The directory was not found: '{e}'");
        }
        catch (IOException e)
        {
            System.Console.WriteLine($"The file could not be opened: '{e}'");
        }
       
       }

         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
        
    }
}