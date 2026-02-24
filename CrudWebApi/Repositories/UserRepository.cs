using System;
using CrudWebApi.Data;
using CrudWebApi.Models;


namespace CrudWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public User CreateUser(User user)
        {
            //ajouter le user à base de donnée
            _appDbContext.Add(user);
            // sauvegarder le user
            _appDbContext.SaveChanges();
            // retourner le user
            return user;

        }

        public void DeleteUser(int id)
        {
            //Trouver le user dans la liste 
            var findUser = _appDbContext.Users.FirstOrDefault((user) => user.Id == id);
             //Trouver le user dans la liste 
            if (findUser == null)
            {
                throw new Exception("Le user n'existe pas");
            }
            else
            {
                //sinon supprimer utilisateur
                _appDbContext.Remove(findUser);
                //puis enregister la modification
                _appDbContext.SaveChanges();
            }
        }

        public User GetUserById(int id)
        {
            //Trouver le user dans la liste 
            var user = _appDbContext.Users.FirstOrDefault(user => user.Id == id);
              // Si le user est égal à null retourner une expection
            if (user == null)
            {
                throw new Exception("L'utilisateur n'existe pas");
            }
            else
            {
                // retourne user
                return user;
            }
        }

        public List<User> GetUsers()
        {
          return  _appDbContext.Users.ToList();
        }

        public User UpdateUser(int id, User user)

        {
            //Trouver le user
            var findUser = _appDbContext.Users.FirstOrDefault(user => user.Id == id);
            // Modifier le user
            if (findUser != null)
            {
                findUser.FirstName = user.FirstName;
                findUser.LastName = user.LastName;
                findUser.Email = user.Email;

                 // sauvegarder le user
                 _appDbContext.SaveChanges();

                // retourner le user modifier
                return findUser;
            }
            else
            {
            throw new Exception("L'utilisateur n'existe pas");

            }
        }
    }
}
