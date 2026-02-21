using System.Linq.Expressions;
using CrudWebApi.Models;
using CrudWebApi.Services;

public class UserService : IUserService
{
    // je crée une nouvelle instance de list 
    private List<User> users = new List<User>();
    public User CreateUser(User user)
    {
        // Ajouter un user à la liste
        var userCount = users.Count();
        user.Id = userCount +1;
        users.Add(user);
        // retourner le user
        return user;
    }

    public void DeleteUser(int id)

    {
        //Trouver le user dans la liste 
        var user = users.Find((user) => user.Id == id);
        // si user n'existe pas, envoyer une exception
        if(user == null)
        {
            throw new Exception("L'utilisateur n'existe pas");
        } else
        //sinon supprimer utilisateur
        {
            users.Remove(user);
        }
        
    }

    public User GetUserById(int id)

    {
        //Trouver le user dans la liste 
        var user = this.users.Find((user) => user.Id == id);
        // Si le user est égal à null retourner une expection
        if (user == null)
        {
              throw new Exception("L'utilisateur n'existe pas");
        } else
        {
            // sinon je retourne le user
            return user;
        }  
       
    }

    public List<User> GetUsers()
    {
        // retourner la liste des users
        return users;
    }

    public User UpdateUser(int id, User user)
    {
          //Trouver le user dans la liste 
         var findUser = this.users.Find((user) => user.Id == id);
        //Si le user existe
        if (findUser != null)
        {
            // Modifier le user
            findUser.FirstName = user.FirstName;
            findUser.LastName = user.LastName;
            findUser.Email = user.Email;

            //retourner le user modifer

            return findUser;

        }
        // sinon renvoyer une exception
        else
        {
            throw new Exception("L'utilisateur n'existe pas");
        }
    }
}