using AuthService.Domain;
using AuthService.Infrastructure.Contexts;

namespace AuthService.Infrastructure.Managers;

// Реализация интерфейса IAuthManager

public class AuthManager : IAuthManager
{
    private readonly UserContext _UserContext;

    public AuthManager(UserContext context) { _UserContext = context;}

    // Получение всех пользователей
    public List<User> GetAll(){ return _UserContext.Users.ToList();}
    public User? GetById(long id){ return _UserContext.Users.FirstOrDefault(x => x.Id == id); } // Получение данных пользователя по id

    //Добавление пользователя
    public User CreateUser(User user){
        var UserData = _UserContext.Add(user);
        _UserContext.SaveChanges();
        return UserData.Entity;
    }

    // Обновление данных пользователя
    public User? UpdateUser(User user){
        var existingUser = _UserContext.Users.FirstOrDefault(x => x.Id == user.Id);

        if(existingUser is null) {return null;}

        existingUser.Login = user.Login;
        existingUser.Password = user.Password;
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.Email = user.Email;
        existingUser.BirthdayDate = user.BirthdayDate;

        var UserData = _UserContext.Update(user);
        _UserContext.SaveChanges();
        return UserData.Entity;

    }

    // Удаление пользователя

    public User? DeleteUser(long id){
        var existingUser = _UserContext.Users.FirstOrDefault(x => x.Id == id);

        if(existingUser is null){return null;}

        var UserData = _UserContext.Remove(existingUser);
        _UserContext.SaveChanges();
        return UserData.Entity;
    }
}