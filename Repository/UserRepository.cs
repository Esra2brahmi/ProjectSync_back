using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectSync_back.data;
using projectSync_back.Dtos.User;
using projectSync_back.Interfaces;
using projectSync_back.Models;

namespace projectSync_back.Repository
{
     public class UserRepository:IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context){
            _context=context;
        }

         public async Task<List<User>> GetAllAsync(){
            return await  _context.Users.ToListAsync();
        }

        public async Task<User> CreateAsync(User userModel){
              await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> UpdateAsync(int id , UpdateUserRequestDto updateUser)
        {
            var existingUser=await _context.Users.FirstOrDefaultAsync(x=>x.Id==id);
            if(existingUser==null){
                return null;
            }
             existingUser.UserFirstName=updateUser.UserFirstName;
             existingUser.UserLastName=updateUser.UserLastName;
             existingUser.Department=updateUser.Department;
             existingUser.Email =updateUser.Email;
             existingUser.Classe=updateUser.Classe;
             existingUser.ProjectType=updateUser.ProjectType;
             existingUser.SupervisorFullName=updateUser.SupervisorFullName;

             await _context.SaveChangesAsync();

             return existingUser;
        }

        public async Task<User> DeleteAsync(int id){
            var userModel=await _context.Users.FirstOrDefaultAsync(x=>x.Id==id);
            if(userModel==null){
                return null;
            }
            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return userModel;


        }

}}