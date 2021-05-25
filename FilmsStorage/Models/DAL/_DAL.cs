
using FilmsStorage.Models.DB;
using FilmsStorage.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FilmsStorage.App_Start;
namespace FilmsStorage.Models.DAL
{
    public static class _DAL
    {
        private static IMapper mapper = AutoMapperConfig.Configure().CreateMapper();

        public static class Users
        {
            public static User Register(RegisterModel registerModel)
            {
                using(var db=new FilmsStorageEntities())
                {
                    User newUser = new User();
                    newUser.UserName = registerModel.UserName;
                    newUser.Login = registerModel.LoginName;
                    newUser.Password = registerModel.Password;
                    newUser.Registered = DateTime.Now;
                    db.User.Add(newUser);

                    db.SaveChanges();


                    return newUser;
                }
            }

            public static User ByLogin(string loginName)
            {
                using(var db=new FilmsStorageEntities())
                {
                    return db.User.Where(u => u.Login == loginName).First();
                }
            }
            public static User ByID(int userID)
            {
                using(var db=new FilmsStorageEntities())
                {
                    return db.User.Where(u => u.UserID == userID).First();
                }
            }
        }
        

        public static class Genres
        {
            public static List<Genre> All()
            {
                using (var db=new FilmsStorageEntities())
                {
                    return db.Genre.ToList();
                }
            }


        }

        public static class Films
        {
            public static Film Add(FilmAddModel filmToAdd)
            {
                using (FilmsStorageEntities db = new FilmsStorageEntities())
                {
                    Film newFilm = mapper.Map<Film>(filmToAdd);

                    db.Film.Add(newFilm);
                    db.SaveChanges();

                    return newFilm;

                }
            }
            public static List<v_Film> ByUserID(int UserID)
            {
                using (FilmsStorageEntities db = new FilmsStorageEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;

                    return db.v_Film.Where(f => f.UserID == UserID).ToList();
                }
            }
            public static List<v_Film> ByName(string fileName)
            {
                using (FilmsStorageEntities db = new FilmsStorageEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;

                    return db.v_Film.Where(f => f.FilmName.ToLower().Contains(fileName.ToLower())).ToList();
                }
            }
            public static List<v_Film> ByYearRange(int yearFrom, int yearTo)
            {
                using (var db = new FilmsStorageEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    return db.v_Film.Where(f => f.Release >= yearFrom && f.Release <= yearTo).ToList();
                }
            }
            public static List<v_Film> ByGenres(int genreId)
            {
                using(var db=new FilmsStorageEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    return db.v_Film.Where(f => f.GenreID == genreId).ToList();
                }
            }

            public static Film ByID(long filmID)
            {
                using(var db=new FilmsStorageEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;

                    try
                    {
                        return db.Film.Where(f => f.FilmId == filmID).First();
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            public static void RemoveById(int filmId)
            {
                using(var db=new FilmsStorageEntities())
                {
                    Film film = db.Film.Where(f => f.FilmId == filmId).First();
                    db.Film.Remove(film);
                    db.SaveChanges();
                }

                
            }
        }
    }
}
