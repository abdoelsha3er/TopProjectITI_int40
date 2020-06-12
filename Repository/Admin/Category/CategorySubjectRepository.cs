using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.Parent.Category
{
    public class CategorySubjectRepository : ICategorySubjectRepository
    {
        DBGProjectITI_Int40 _context;
        public CategorySubjectRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        //GetAll
        public IEnumerable<CategorySubject> GetCategorySubject()
        {
            return _context.CategorySubjects.Include(s => s.Subjects).ToList();
        }
        //GetAll By ID
        public CategorySubject GetCategorySubjectById(int id)
        {
            return _context.CategorySubjects.Find(id);
        }
        //Add
        public void AddCategorySubject(CategorySubject categorySubject)
        {
            _context.CategorySubjects.Add(categorySubject);
            _context.SaveChanges();
        }
        //Edit
        public void EditCategorySubject(CategorySubject newCategorySubject)
        {
            //oldCategorySubject.Name = newCategorySubject.Name;

            //_context.Entry(newCategorySubject).State = EntityState.Modified;
            //_context.CategorySubjects.Update(newCategorySubject);
            //_context.CategorySubjects.Update(newCategorySubject);
            _context.SaveChanges();
            
        }
        //Delete
        public void DeleteCategorySubject(int id)
        {
            CategorySubject categorySubject = _context.CategorySubjects.FirstOrDefault(a => a.CategorySubjectId == id);
            _context.Remove(categorySubject);
            _context.SaveChanges();
        }
        public CategorySubject GetDetailsCategorySubjectById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
