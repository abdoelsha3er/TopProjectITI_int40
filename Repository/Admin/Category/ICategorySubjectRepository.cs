using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.Parent.Category
{
    public interface ICategorySubjectRepository
    {
        IEnumerable<CategorySubject> GetCategorySubject();
        CategorySubject GetCategorySubjectById(int id);
        void AddCategorySubject(CategorySubject categorySubject);
        void EditCategorySubject(CategorySubject naeCategorySubject);
        void DeleteCategorySubject(int id);
        CategorySubject GetDetailsCategorySubjectById(int id);
    }
}
