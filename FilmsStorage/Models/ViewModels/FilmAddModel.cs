using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FilmsStorage.Models.DB;

namespace FilmsStorage.Models.ViewModels
{
    public class FilmAddModel
    {
        [Required]
        public  string FilmName { get; set; }
        [Required]
        public int Release { get; set; }
        [Required]
        public int GenreID { get; set; }

        public string fileDescription { get; set; }

        public int UserID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public List<Genre> Genres { get; set; }
    }
}