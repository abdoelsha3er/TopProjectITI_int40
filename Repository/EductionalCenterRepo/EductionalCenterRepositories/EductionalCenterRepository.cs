﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.ViewModels;

namespace TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterRepositories
{
    public class EductionalCenterRepository : IEductionalCenterRepository
    {
        DBGProjectITI_Int40 _context;
        public EductionalCenterRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        
        }
        // Add new EductionalCenter  // Register
        public async Task AddEductionalCenter(EductionalCenter eductionalCenter)
        {
            await _context.EductionalCenters.AddAsync(eductionalCenter);
            await _context.SaveChangesAsync();
        }

        // Get All Eductional Centers
        public async Task<IEnumerable<EductionalCenter>> GetEductionalCenters()
        {
            return await _context.EductionalCenters.ToListAsync();
        }
        // get by EductionalCenter id   // will get from token when logined   // may use it in profile EductionalCenter>> id will from token
        public async Task<EductionalCenter> GetEductionalCenterById(int eductionalCenterId)
        {
            return await _context.EductionalCenters.FindAsync(eductionalCenterId);
        }
        public async Task<EductionalCenter> GetEductionalCenterByUserName(string userName)
        {
            return await _context.EductionalCenters.FirstOrDefaultAsync(a => a.UserName == userName);
        }
        // Edit profile of      // id gets from token when login, can't be able edit any profile 
        public async Task EditEductionalCenter(EductionalCenter newEductionalCenter, int eductionalCenterId, string file)
        {
            EductionalCenter oldEductionalCenter = await GetEductionalCenterById(eductionalCenterId);
            oldEductionalCenter.Name = newEductionalCenter.Name;
            oldEductionalCenter.UserName = newEductionalCenter.UserName;
            oldEductionalCenter.Email = newEductionalCenter.Email;
            oldEductionalCenter.Password = newEductionalCenter.Password;
            if (file != null)
            {
                oldEductionalCenter.Picture = file;
            }
            oldEductionalCenter.About = newEductionalCenter.About;
            oldEductionalCenter.CityId = newEductionalCenter.CityId;
            oldEductionalCenter.AddressDetails = newEductionalCenter.AddressDetails;
            _context.Entry(oldEductionalCenter).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        //Login
        public async Task<EductionalCenter> LoginEductionalCenter(LoginViewModel loginViewModel)
        {
            return await _context.EductionalCenters.SingleOrDefaultAsync(t => (t.UserName == loginViewModel.UserName) || t.Email == loginViewModel.UserName);
        }

    }
}
