using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.ParentRepo.ParentRepositories
{
    public class ParentRepository : IParentRepository
    {
        DBGProjectITI_Int40 _context;
        public ParentRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        public async Task<Parent> GetParentById(int _id)  // get by id
        {
            return await _context.Parents.FirstOrDefaultAsync(p => p.ParentId == _id);
        }
        public async Task AddParent(Parent parent)   // fro resigster Parent
        {
            await _context.Parents.AddAsync(parent);
            await _context.SaveChangesAsync();
        }
        public async Task<Parent> CheckParentLogin(string userName, string password)   // for check login parent
        {
            return await _context.Parents.FirstOrDefaultAsync(p => p.UserName == userName && p.Password == password);
        }

        public async Task EditParent(Parent newParent, int parentId)  // for editing profile
        {

            Parent oldParent = await GetParentById(parentId);
            oldParent.FirstName = newParent.FirstName;
            oldParent.LastName = newParent.LastName;
            oldParent.Password = newParent.Password;
            oldParent.Phone = newParent.Phone;
            oldParent.Picture = newParent.Picture;
            oldParent.Street = newParent.Street;
            oldParent.UserName = newParent.UserName;
            oldParent.Email = newParent.Email;
            oldParent.CityId = newParent.CityId;
            _context.Parents.Update(oldParent);
            // _context.Entry(oldParent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteParent(int parentId)   // for Delete Parent
        {
            Parent parent = await GetParentById(parentId);
            _context.Remove(parent);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Parent>> GetParents()
        {
            throw new NotImplementedException();
        }
    }
}
