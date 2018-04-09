using System;
using System.Data;
using keepr.Models;
using Dapper;

namespace keepr.Repositories
{
  public class UserRepository
  {
    private readonly IDbConnection _db;
    public UserRepository(IDbConnection db)
    {
      _db = db;
    }
    public UserReturnModel Register(UserCreateModel userData)
    {
      //Generate an ID
      Guid g;
      // Create and display the value  GUIDs.
      g = Guid.NewGuid();
      string id = g.ToString();
      string pass = BCrypt.Net.BCrypt.HashPassword(userData.Password);

      //construct a user
      User user = new User()
      {
        Id = id,
        Username = userData.Username,
        Email = userData.Email,
        Password = pass// lets encrypt userData.Password
      };

      // run a sql command
      var success = _db.Execute(@"
        INSERT INTO users(
          userId,
          username, 
          email,
          password
        ) VALUES(
          @Id,
          @Username,
          @Email,
          @Password
        )
      ", user);
      if (success < 1)
      {
        throw new Exception("EMAIL IN USE");
      }
      // create and return a user return model
      return new UserReturnModel()
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email
      };
    }

    public UserReturnModel Login(UserLoginModel userData)
    {
      // step1? Look up user by email
      User user = _db.QueryFirstOrDefault<User>(@"
        SELECT * FROM users WHERE email = @Email
      ", userData);

      // step2? Check password is valid
      Boolean valid = BCrypt.Net.BCrypt.Verify(userData.Password, user.Password);
      if (valid)
      {
        return new UserReturnModel()
        {
          Id = user.Id,
          Username = user.Username,
          Email = user.Email
        };
      }
      throw new Exception("Invalid Credentials");
    }

    public UserReturnModel GetUserById(string id)
    {
      User user = _db.QueryFirstOrDefault<User>(@"
        SELECT * FROM users WHERE userId = @Id
      ", new { Id = id });

      if (user == null) { throw new Exception("Oh Boy something very bad happened"); }

      return new UserReturnModel()
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email
      };
    }

    internal UserReturnModel UpdateAccount(UserReturnModel user, UserReturnModel userData)
    {
      var i = _db.Execute(@"
                UPDATE users SET
                    email = @Email,
                    username = @Username
                WHERE id = @Id
            ", userData);
      if (i > 0)
      {
        return userData;
      }
      return null;

    }

    internal string ChangeUserPassword(ChangeUserPasswordModel user)
    {
      User savedUser = _db.QueryFirstOrDefault<User>(@"
            SELECT * FROM users WHERE id = @id
            ", user);

      var valid = BCrypt.Net.BCrypt.Verify(user.OldPassword, savedUser.Password);
      if (valid)
      {
        user.NewPassword = BCrypt.Net.BCrypt.HashPassword(user.NewPassword);
        var i = _db.Execute(@"
                    UPDATE users SET
                        password = @NewPassword
                    WHERE id = @id
                ", user);
        return "Good Job";
      }
      return "Umm nope!";
    }
  }
}