using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FilmsStorage.Models.DB;
using FilmsStorage.Models.ViewModels;
using FilmsStorage.Models.DAL;
using static FilmsStorage.SL._SL.Files;
using System.IO;
using FilmsStorage.SL;

namespace FilmsStorage.Controllers
{
    [Authorize]
    public class FilmsController : BaseController
    {
        // показати форму додавання фільма
        public ViewResult Add()
        {
            FilmAddModel model = new FilmAddModel();
            model.Genres = _DAL.Genres.All();
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(FilmAddModel newFilmModel)
        {
            HttpPostedFileBase postedFile = Request.Files[0];
            if (ModelState.IsValid)
            {
                if (postedFile.ContentLength>0)
                {

                    FilmFileSaveResult filmFileSaveResult = SaveFilm(postedFile, base.User.UserID);
                    if (filmFileSaveResult.IsSaved)
                    {
                        newFilmModel.FileName = filmFileSaveResult.FileName;
                        newFilmModel.FilePath = filmFileSaveResult.FilePath;
                        newFilmModel.UserID = base.User.UserID;
                        _DAL.Films.Add(newFilmModel);
                    }
                }
                else
                {
                    ViewBag.ErrorMsg = "Please select a file to upload";
                }
            }
           
            newFilmModel.Genres = _DAL.Genres.All();
            return View(newFilmModel);
        }

        public PartialViewResult ByUser()
        {
            List<v_Film> userFilms = _DAL.Films.ByUserID(base.User.UserID);

            return PartialView(userFilms);
        }

        public ViewResult Search()
        {
            FilmAddModel model = new FilmAddModel();
            model.Genres = _DAL.Genres.All();

            return View(model);
        }

        public PartialViewResult ByName(string filmName)
        {
            List<v_Film> films = _DAL.Films.ByName(filmName);

            return PartialView("SearchResults",films);
        }
        
        public  PartialViewResult ByYears(int yearFrom, int yearTo)
        {
            List<v_Film> films = _DAL.Films.ByYearRange(yearFrom, yearTo);
            return PartialView("SearchResults", films);
        }
        public PartialViewResult ByGenres(FilmAddModel model)
        {
            List<v_Film> films = _DAL.Films.ByGenres(model.GenreID);
            return PartialView("SearchResults", films);
        }
        public ActionResult Download(long id)
        {
            Film film = _DAL.Films.ByID(id);
            if (film != null)
            {
                string downloadPath = Path.Combine(film.filePath, film.fileName);
                return File(downloadPath,_SL.Tools.MimeTypeByFileName(film.fileName),film.fileName+Path.GetExtension(film.FilmName));
            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NoContent);
            }
            
        }


        public ActionResult Delete(int id)
        {
            _DAL.Films.RemoveById(id);
            return null;
        }
       
    }
}