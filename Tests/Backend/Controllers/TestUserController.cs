//using AutoMapper;
//using DataAccessLayer.Interfaces;
//using AutoMapper;
//using Azure;
//using DataAccessLayer.Interfaces;
//using DataAccessLayer.Model;
//using FakeItEasy;
//using FluentAssertions;
//using LoggerService;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using System.Net;
//using WebApi.Controllers;
//using WebApi.DTOs;
//using WebApi.MapperProfiles;


//namespace Tests.Backend.Controllers
//{
//    private IUserDataAccess _usersDataAcces;
//    private IMapper _mapper;
//    private UsersController _usersController;

//    public class TestUserController
//    {
//        _usersDataAcces = A.Fake<IUserDataAccess>();

//            //mapper config 
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new UserProfile());
//            });
//        _mapper = config.CreateMapper();

//            _usersController = new (_userDataAcces, _mapper);
//    }

//    [Fact]
//    public async Task GetByEmail_User_Success()
//    {
//        //ARRANGE
//        User user = new();
//        A.CallTo(() => _userDataAcces.GetByEmail("best.user@ucn.dk")).Returns(user);
//        //ACT
//        var request = await _usersController.Get("best.user@ucn.dk");
//        var result = request.Result as ObjectResult;
//        //ASSERT
//        result.Should().BeOfType<OkObjectResult>()
//        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
//    }

//    [Fact]
//    public async Task Post_User_Success()
//    {
//        //ARRANGE
//        var userDto = A.Fake<UserDto>();
//        A.CallTo(() => _userDataAcces.CreateAsync(new User())).Returns(3);
//        //ACT
//        var request = await _usersController.Post(userDto);
//        var result = request.Result as ObjectResult;
//        //ASSERT
//        result.Should().BeOfType<OkObjectResult>()
//        .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
//    }

//    [Fact]
//    public async Task Put_User_Success()
//    {
//        //ARRANGE
//        var userDto = A.Fake<UserDto>();
//        A.CallTo(() => _userDataAcces.UpdateAsync(new User())).Returns(true);
//        //ACT
//        var request = await _usersController.Put("best.user@ucn.dk", userDto);
//        //ASSERT
//        Assert.IsType<OkResult>(request);
//    }

//    [Fact]
//    public async Task Delete_User_Success()
//    {
//        //ARRANGE
//        string emailToDelete = "best.user@ucn.dk";
//        User user = new();
//        user.Email = emailToDelete;
//        A.CallTo(() => _userDataAcces.DeleteAsync(emailToDelete));
//        //ACT
//        var request = await _usersController.Delete(emailToDelete);
//        //ASSERT
//        Assert.IsType<OkResult>(request);

//    }

//    //[Fact]
//    //public async Task GetAll_Users_Success()
//    //{
//    //    //ARRANGE
//    //    var users = A.Fake<IEnumerable<User>>();
//    //    A.CallTo(() => _userDataAcces.GetAllAsync()).Returns(users);
//    //    //ACT
//    //    var request = await _usersController.Get();
//    //    var result = request.Result as ObjectResult;
//    //    //ASSERT
//    //    result.Should().BeOfType<OkObjectResult>()
//    //    .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
//    //}
//}
