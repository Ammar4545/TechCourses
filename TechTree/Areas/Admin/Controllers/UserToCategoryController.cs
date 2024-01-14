using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTree.Areas.Admin.Models;
using TechTree.Data;
using TechTree.Entities;

namespace TechTree.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class UserToCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserToCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }
        //this get method will return all user saved to the system and all user saved against sepecific categoetId
        [HttpGet]
        public async Task<IActionResult> GetUserForCategory(int categoryId)
        {
            UserCategoryListModel userCategoryListModel = new UserCategoryListModel();
            var allUser = await GetAllUsers();

            var selectedUserForCategory = await GetSavedSelectedUsersForCategory(categoryId);

            userCategoryListModel.Users = allUser;
            userCategoryListModel.SelectedUser = selectedUserForCategory;

            return PartialView("_UsersListViewPartial", userCategoryListModel);

        }

        [HttpPost]
        public async Task<IActionResult> SaveSelectedUser([Bind("CategoryId,SelectedUser")] UserCategoryListModel userCategoryListModel)
        {
            List<UserCategory> usersSelecetForCategoryToAdd = null;

            if (userCategoryListModel.SelectedUser != null)
            {
                usersSelecetForCategoryToAdd = await GetUserForCategoryToAdd(userCategoryListModel);
            }

            //var usersSelecetForCategoryToAdd = await GetUserForCategoryToAdd(userCategoryListModel);
            var usersSelecetForCategoryTodelete = await GetUserForCategoryToDelete(userCategoryListModel.CategoryId);

            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    _context.RemoveRange(usersSelecetForCategoryTodelete);
                    await _context.SaveChangesAsync();
                    if (usersSelecetForCategoryToAdd != null)
                    {
                        _context.AddRange(usersSelecetForCategoryToAdd);
                        await _context.SaveChangesAsync();
                    }
                    await dbContextTransaction.CommitAsync();

                }
                catch (Exception ex)
                {

                    await dbContextTransaction.DisposeAsync();
                }
            }

            userCategoryListModel.Users=await GetAllUsers();

            return PartialView("_UsersListViewPartial", userCategoryListModel);

        }

        private async Task<List<UserModel>> GetAllUsers()
        {
            var allUser = await (from user in _context.Users
                                 select new UserModel
                                 {
                                     Id = user.Id,
                                     UserName = user.UserName,
                                     FirstName = user.FirstName,
                                     LastName = user.LastName
                                 }).ToListAsync() ;
            return allUser;
        }

        private async Task<List<UserCategory>> GetUserForCategoryToAdd(UserCategoryListModel userCategoryListModel)
        {
            var usersForCategoryToAdd = (from userCat in userCategoryListModel.SelectedUser
                                         select new UserCategory
                                         {
                                             CategoryId = userCategoryListModel.CategoryId,
                                             UserId = userCat.Id
                                         }).ToList();
            return await Task.FromResult(usersForCategoryToAdd);
            
        }

        private async Task<List<UserCategory>> GetUserForCategoryToDelete(int categoryId)
        {
            var usersForCategoryToDelete = (from userCat in _context.UserCategory
                                            where userCat.CategoryId == categoryId
                                            select new UserCategory
                                            {
                                                CategoryId = categoryId,
                                                Id = userCat.Id,
                                                UserId = userCat.UserId
                                            }
                                          ).ToList();
            return await Task.FromResult(usersForCategoryToDelete);
        }

        private async Task<List<UserModel>> GetSavedSelectedUsersForCategory(int categoryId)
        {
            var savedSelectedUsersForCategory = await (from usersToCat in _context.UserCategory
                                                       where usersToCat.CategoryId == categoryId
                                                       select new UserModel
                                                       {
                                                           Id = usersToCat.UserId
                                                       }).ToListAsync();
            return savedSelectedUsersForCategory;
        }


    }
}
