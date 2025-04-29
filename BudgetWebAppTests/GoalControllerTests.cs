using Xunit;
using BudgetWepApp.Controllers;
using BudgetWepApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BudgetWebAppTests
{
    public class GoalControllerTests
    {
        private Mock<UserManager<User>> mockUserManager;
        private Mock<IUserContext> mockContext;
        
        public GoalControllerTests()
        {
            var userStore = new Mock<IUserStore<User>>();
            mockUserManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            mockContext = new Mock<IUserContext>();
        }

        [Fact]
        public void Goals_ReturnGoalsForUser()
        {
            var userId = "testUserId";
            mockUserManager.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);

            var goals = new List<Goal>
            {
                new Goal { GoalID = 1, userId = userId, Name = "Test Goal 1" }
            };
            mockContext.Setup(c => c.Goals).Returns(MockDbSet(goals.AsQueryable()));

            var ctrl = new GoalController(mockContext.Object, mockUserManager.Object);

            var res = ctrl.Goals() as ViewResult;
            var model = res?.Model as List<Goal>;

            Assert.NotNull(res);
            Assert.Single(model);
            Assert.Equal("Test Goal 1", model[0].Name);

        }
        [Fact]
        public void CreateGoal_ValidGoal_RedirectsToGoals()
        {
            var userId = "testUserId";
            var user = new User { Id = userId };
            mockUserManager.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            mockContext.Setup(c => c.Users).Returns(MockDbSet(new List<User> { user }.AsQueryable()));

            var goals = new List<Goal>();
            mockContext.Setup(c => c.Goals).Returns(MockDbSet(goals.AsQueryable()));
            mockContext.Setup(c => c.SaveChanges()).Returns(1);

            var goal = new Goal { Name = "Test Goal", Description = "Desc Test" };
            var ctrl = new GoalController(mockContext.Object, mockUserManager.Object);

            var res = ctrl.CreateGoal(goal) as RedirectToActionResult;

            Assert.NotNull(res);
            Assert.Equal("Goals", res.ActionName);
        }
        [Fact]
        public void EditGoal_NonexistentGoal_ReturnsNotFound()
        {
            var goals = new List<Goal>
            {
                new Goal { GoalID = 1, userId = "testUserId", Name = "Test Goal 1" }
            };
            mockContext.Setup(c => c.Goals).Returns(MockDbSet(goals.AsQueryable()));

            var ctrl = new GoalController(mockContext.Object, mockUserManager.Object);

            var res = ctrl.EditGoal(999);
            Assert.IsType<NotFoundObjectResult>(res);
        }

        private static Microsoft.EntityFrameworkCore.DbSet<T> MockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }
    }
}