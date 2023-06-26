using AuthService.Domain;

namespace AuthService.Host.Routes;

public static class UserRouter{
    public static WebApplication AddUserRouter(this WebApplication application){
        var userGroup = application.MapGroup("/api/users");

        userGroup.MapGet(pattern: "/", handler: GetAllUsers);
        userGroup.MapGet(pattern: "/{id:long}", handler: GetUserById);
        userGroup.MapPost(pattern: "/", handler: CreateUser);
        userGroup.MapPut(pattern: "/", handler: UpdateUser);
        userGroup.MapDelete(pattern: "/{id:long}", handler: DeleteUser);

        return application;
    }
    // Получаем всех пользователей
    private static IResult GetAllUsers(IAuthManager authManager){
        var users = authManager.GetAll();
        return Results.Ok(users); // Список пользователей
    }
    

    // ПОлучить пользователь по id
    private static IResult GetUserById(long id, IAuthManager authManager){
        var user = authManager.GetById(id);
        return user is null ? Results.NotFound() : Results.Ok(user); // Данные пользователя
    }


    //Добавление пользователя
    private static IResult CreateUser(User user, IAuthManager authManager){
        var createUser = authManager.CreateUser(user);
        return Results.Ok(createUser); //Данные пользователя
    }


    //Обновление данных пользователя

    private static IResult UpdateUser(User user, IAuthManager authManager){
        var updateUser = authManager.UpdateUser(user);
        return updateUser is null ? Results.NotFound() : Results.Ok(updateUser);
    }

    //Удаление пользователя

    private static IResult DeleteUser(long id, IAuthManager authManager){
        var deletedUser = authManager.DeleteUser(id);
        return deletedUser is null ? Results.NotFound() : Results.Ok(deletedUser);
    }

}