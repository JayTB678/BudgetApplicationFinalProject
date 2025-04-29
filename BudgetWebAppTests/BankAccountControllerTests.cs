using BudgetWepApp.Controllers;
using BudgetWepApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BudgetWebAppTests
{
    public class BankAccountControllerTests
    {
        private Mock<UserManager<User>> mockUserManager;
        private Mock<IUserContext> mockContext;
        public BankAccountControllerTests() 
        {
            var userStore = new Mock<IUserStore<User>>();
            mockUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            mockContext = new Mock<IUserContext>();
        }
        [Fact]
        public void BankAccountInfo_UserNotFound_ReturnsNotFoundResult()
        {
            var userId = "testUserId";
            mockUserManager.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            mockContext.Setup(c => c.Users).Returns(MockDbSet(new List<User>().AsQueryable()));

            var ctrl = new BankAccountController(mockContext.Object, mockUserManager.Object);

            var res = ctrl.BankAccountInfo(new UserViewModel());

            Assert.IsType<NotFoundObjectResult>(res);
        }
        [Fact]
        public void BankAccountInfo_AddIncome_AddsIncomeAndRedirects()
        {
            var userId = "testUserId";
            var user = new User { Id = userId, CurrentBalance = 0};

            mockUserManager.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            mockContext.Setup(c => c.Users).Returns(MockDbSet(new List<User> { user }.AsQueryable()));
            mockContext.Setup(c => c.Incomes).Returns(MockDbSet(new List<Income>().AsQueryable()));
            mockContext.Setup(c => c.Transactions).Returns(MockDbSet(new List<Transaction>().AsQueryable()));

            var ctrl = new BankAccountController(mockContext.Object, mockUserManager.Object);

            var model = new NewTransationViewModel
            {
                Amount = 100.0,
                FrequencyInDays = 10,
                UpdateTransactions = true
            };

            var res = ctrl.AddIncome(model) as RedirectToActionResult;

            Assert.NotNull(res);
            Assert.Equal("BankAccountInfo", res.ActionName);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
        [Fact]
        public void RemoveIncome_ValidIncome_RemovesIncome()
        {
            var userId = "testUserId";
            var incomeId = 1;
            var user = new User { Id = userId };
            var income = new Income { IncomeID = incomeId, userId = userId, IncomeAmmount = 100.00 };
            mockUserManager.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            mockContext.Setup(c => c.Users).Returns(MockDbSet(new List<User> { user }.AsQueryable()));
            mockContext.Setup(c => c.Incomes).Returns(MockDbSet(new List<Income> { income }.AsQueryable()));

            var ctrl = new BankAccountController(mockContext.Object, mockUserManager.Object);

            var res = ctrl.RemoveIncome(incomeId) as RedirectToActionResult;

            Assert.NotNull(res);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        private static DbSet<T> MockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }
    }
}
