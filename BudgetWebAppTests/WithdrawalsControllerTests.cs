using Xunit;
using BudgetWepApp.Controllers;
using BudgetWepApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BudgetWebAppTests
{
    public class WithdrawalsControllerTests
    {
        private Mock<IUserContext> mockContext;
        private Mock<UserManager<User>> mockUserManager;

        public WithdrawalsControllerTests()
        {
            mockUserManager = MockUserManager();
            mockContext = new Mock<IUserContext>();

            static Mock<UserManager<User>> MockUserManager()
            {
                var userStore = new Mock<IUserStore<User>>();
                return new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            }
        }
        private Mock<DbSet<T>> CreateMockDbSet<T>(List<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return mockSet;
        }
        [Fact]
        public void Withdrawals_AddPayment_AddsPaymentAndTransaction()
        {
            var userId = "testUserId";
            var user = new User { Id = userId, UserName = "TestUser" , CurrentBalance = 1000 };
            
            var users = CreateMockDbSet(new List<User> { user });
            var recurringPayments = CreateMockDbSet(new List<RecurringPayment>());
            var transactions = CreateMockDbSet(new List<Transaction>());

            mockUserManager.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            mockContext.Setup(c => c.Users).Returns(users.Object);
            mockContext.Setup(c => c.recurringPayments).Returns(recurringPayments.Object);
            mockContext.Setup(c => c.Transactions).Returns(transactions.Object);
            mockContext.Setup(c => c.SaveChanges()).Returns(1);

            var ctrl = new WithdrawalsController(mockContext.Object, mockUserManager.Object);
            ctrl.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }))
                }
            };

            var model = new NewTransationViewModel
            {
                Amount = 200,
                FrequencyInDays = 30,
                UpdateTransactions = true
            };
            var res = ctrl.AddPayment(model);

            var redirectResult = Assert.IsType<RedirectToActionResult>(res);
            Assert.Equal("WithdrawalsPage", redirectResult.ActionName);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
        [Fact]
        public void Withdrawals_RemovePayment_RemovesAndRedirects()
        {
            var userId = "testUserId";
            var payment = new RecurringPayment { RecurringPaymentId = 1, userId = userId, PaymentAmount = 100 };

            var recurringPayments = CreateMockDbSet(new List<RecurringPayment> { payment });

            mockContext.Setup(c => c.recurringPayments).Returns(recurringPayments.Object);
            mockContext.Setup(c => c.SaveChanges()).Returns(1);

            var ctrl = new WithdrawalsController(mockContext.Object, mockUserManager.Object);

            var res = ctrl.RemovePayment(payment.RecurringPaymentId);

            var redirectResult = Assert.IsType<RedirectToActionResult>(res);
            Assert.Equal("WithdrawalsPage", redirectResult.ActionName);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
