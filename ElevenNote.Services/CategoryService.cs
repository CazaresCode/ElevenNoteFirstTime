using ElevenNote.Data;
using ElevenNote.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category()
            {
                OwnerId = _userId,
                CategoryName = model.CategoryName
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Categories
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CategoryListItem
                                {
                                    CategoryId = e.CateogryId,
                                    CategoryName = e.CategoryName
                                }
                                );

                return query.ToArray();
            }
        }

        public CategoryDetail GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CateogryId == id && e.OwnerId == _userId);
                return
                    new CategoryDetail
                    {
                        CategoryId = entity.CateogryId,
                        CategoryName = entity.CategoryName
                    };
            }
        }

        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CateogryId == model.CategoryId && e.OwnerId == _userId);

                entity.CategoryName = model.CategoryName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Categories
                    .Single(e => e.CateogryId == id && e.OwnerId == _userId);

                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
