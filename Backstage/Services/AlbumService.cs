using Backstage.Models;
using Backstage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Backstage.Repositories;

namespace Backstage.Services
{
    public class AlbumService
    {
        public List<AlbumViewModel> GetModalinform()
        {
            SoulfulBackStage context = new SoulfulBackStage();
            SoulfulRepository< Singer > SingerRepository = new SoulfulRepository<Singer>(context);
            SoulfulRepository<Album> AlbumRepository = new SoulfulRepository<Album>(context);

            var albumContext = from Album in AlbumRepository.GetAll()
                               join Singer in SingerRepository.GetAll()
                                on Album.Singer_id equals Singer.Singer_id
                               select new AlbumViewModel
                               {
                                   Album_Name = Album.Album_Name,
                                   About = Album.About,
                                   Pic = Album.Pic,
                                   Singer = Singer.Name,
                                   Singer_id = Singer.Singer_id,
                                   Company=Album.Company,
                                   Datetime=Album.Datetime,
                                   Album_id=Album.Album_id,
                                   Price=Album.Price,
                                   Hits=Album.Hits,
                                   WeekHits=Album.WeekHits,
                                   MonthHits=Album.MonthHits
                               };
            return albumContext.ToList();
        }

        public List<HitViewModel> GetHit()
        {
            SoulfulBackStage context = new SoulfulBackStage();
            SoulfulRepository<Album> AlbumRepository = new SoulfulRepository<Album>(context);

            var albumContext = from Album in AlbumRepository.GetAll()
                               select new HitViewModel
                               {
                                   Name = Album.Album_Name,
                                   Hits = Album.Hits
                               };
            return albumContext.ToList();
        }
    }
}