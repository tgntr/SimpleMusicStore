using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Models.ViewModels;
using SimpleMusicStore.Web.Services;

namespace SimpleMusicStore.Web.Controllers
{
    public class ViewController : Controller
    {
        private RecordService _recordService;
        private LabelService _labelService;
        private ArtistService _artistService;
        private IMapper _mapper;

        public ViewController(
           SimpleDbContext context,
           IMapper mapper)
        {
            _recordService = new RecordService(context, mapper);
            _labelService = new LabelService(context);
            _artistService = new ArtistService(context);
            _mapper = mapper;
        }

        public IActionResult Records(string orderBy = "")
        {
            List<RecordViewModel> records;
            if (orderBy == "newest")
            {
                records = _recordService.All().OrderByDescending(r => r.DateAdded).Select(_mapper.Map<RecordViewModel>).ToList();
            }
            else if (orderBy == "alphabetically")
            {
                records = _recordService.All().OrderBy(r => r.Title).Select(_mapper.Map<RecordViewModel>).ToList();
            }
            else if (orderBy == "popularity")
            {
                records = _recordService.All().OrderByDescending(r => r.WantedBy.Count() + r.Orders.Count()).Select(_mapper.Map<RecordViewModel>).ToList();
            }
            else
            {
                records = _recordService.All().Select(_mapper.Map<RecordViewModel>).ToList();
            }

            return View(records);
        }

        public IActionResult Records(int id)
        {
            var record = _recordService.GetRecord(id);

            if (record is null)
            {
                return RedirectToAction("Records");
            }

            var viewModel = _mapper.Map<RecordViewModel>(record);

            return View(viewModel);
        }






        public IActionResult Labels(string orderBy = "")
        {
            List<LabelViewModel> labels;
            if (orderBy == "alphabetically")
            {
                labels = _labelService.All().OrderBy(l => l.Name).Select(_mapper.Map<LabelViewModel>).ToList();
            }
            else if (orderBy == "popularity")
            {
                labels = _labelService.All().OrderByDescending(l => l.Followers.Count()).Select(_mapper.Map<LabelViewModel>).ToList();
            }
            else
            {
                labels = _labelService.All().Select(_mapper.Map<LabelViewModel>).ToList();
            }

            return View(labels);
        }

        public IActionResult Labels(int id)
        {
            var label = _labelService.GetLabel(id);

            if (label is null)
            {
                return RedirectToAction("Labels");
            }

            var viewModel = _mapper.Map<LabelViewModel>(label);

            return View(viewModel);
        }






        public IActionResult Artists(string orderBy = "")
        {
            List<ArtistViewModel> artists;
            if (orderBy == "alphabetically")
            {
                artists = _artistService.All().OrderBy(a => a.Name).Select(_mapper.Map<ArtistViewModel>).ToList();
            }
            else if (orderBy == "popularity")
            {
                artists = _artistService.All().OrderByDescending(a => a.Followers.Count()).Select(_mapper.Map<ArtistViewModel>).ToList();
            }
            else
            {
                artists = _artistService.All().Select(_mapper.Map<ArtistViewModel>).ToList();
            }

            return View(artists);
        }

        public IActionResult Artists(int id)
        {
            var artist = _artistService.GetArtist(id);

            if (artist is null)
            {
                return RedirectToAction("Artists");
            }

            var viewModel = _mapper.Map<ArtistViewModel>(artist);

            return View(viewModel);
        }





        public IActionResult Genres(string genre, string orderBy = "")
        {
            if (_recordService.IsValidGenre(genre))
            {
                return RedirectToAction("Records");
            }

            List<RecordViewModel> records;
            if (orderBy == "newest")
            {
                records = _recordService.All(genre).OrderByDescending(r => r.DateAdded).Select(_mapper.Map<RecordViewModel>).ToList();
            }
            else if (orderBy == "alphabetically")
            {
                records = _recordService.All(genre).OrderBy(r => r.Title).Select(_mapper.Map<RecordViewModel>).ToList();
            }
            else if (orderBy == "popularity")
            {
                records = _recordService.All(genre).OrderByDescending(r => r.WantedBy.Count() + r.Orders.Count()).Select(_mapper.Map<RecordViewModel>).ToList();
            }
            else
            {
                records = _recordService.All(genre).Select(_mapper.Map<RecordViewModel>).ToList();
            }



            return View(records);
        }
    }
}