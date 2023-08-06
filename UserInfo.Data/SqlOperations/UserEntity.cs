using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfo.Core;
using UserInfo.Data.Interface;

namespace UserInfo.Data.SqlOperations
{
    public class UserEntity : IDataHelper<User>
    {
        private DBContext _dbContext;

        public UserEntity()
        {
            _dbContext = new DBContext();
        }

        public async Task AddAsync(User table)
        {
            await _dbContext.Users.AddAsync(table);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var userExist = await FindAsync(id);
            if (userExist != null)
            {
                _dbContext.Users.Remove(userExist);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EditAsync(User table)
        {
            _dbContext = new DBContext();
            _dbContext.Users.Update(table);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> FindAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<List<User>> SearchAsync(string Item)
        {
            return await _dbContext.Users
                .Where(x => x.Id.ToString() == Item
                || x.FullName.Contains(Item)
                || x.Email.Contains(Item)
                || x.PhoneNumber.Contains(Item))
                .ToListAsync();
        }
    }
}
