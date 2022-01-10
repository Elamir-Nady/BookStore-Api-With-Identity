using BookStoreApi.Models;
using BookStoreApi.Repostory;
using BookStoreApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;

namespace BookStoreApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        public IUnitOfWork _UnitOfWork { get; }
        public IHostingEnvironment Hosting { get; }

        ResultDto result = new ResultDto();
        public BookController(IUnitOfWork unitOfWork, IHostingEnvironment hosting)
        {
            _UnitOfWork = unitOfWork;
            Hosting = hosting;
        }
        // GET: api/<BookController>
        [HttpGet]
        public ResultDto Get()
        {
            result.Message = "All Books";
            result.Data = _UnitOfWork.GetBookRepo().Get();
            if (result.Data == null)
            {
                result.Message = "Not Found any Books ";
                result.ISuccessed = false;

            }
            return result;
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<ResultDto> Get(int id)
        {
           
            result.Message = "Book Where ID is: " + id;
            result.Data =await _UnitOfWork.GetBookRepo().GetByID(id); ;
            if (result.Data==null)
            {
                result.Message = "Not Found Book Use Id " + id;
                result.ISuccessed = false;

            }
            return result;
        }
        [HttpGet("Auther/{id}")]
        public ResultDto GetByAuther(int id)
        {
            result.Message = "All Books For AutherID: "+id;
            result.Data = _UnitOfWork.GetBookRepo().Get().Where(b=>b.AutherID==id);
            if (result.Data == null)
            {
                result.Message = "Not Found any Books For This Auther";
                result.ISuccessed = false;

            }
            return result;
        }
        // POST api/<BookController>
        [HttpPost]
        public async Task<ResultDto> Post(Book book)
        {
            if (book != null)
            {
               await _UnitOfWork.GetBookRepo().Add(book);
                await _UnitOfWork.Save();
                result.Message = "Added new book";
                result.Data = book;
            }
            else
            {
                result.Message = "Not Added";
                result.ISuccessed = false;

            }
            return result;

        }

        // PUT api/<BookController>/5
        [HttpPut]
        public async Task<ResultDto> Put(Book book)
        {
            if (book != null)
            {
                _UnitOfWork.GetBookRepo().Update(book);
               await _UnitOfWork.Save();
                result.Message = "Update book Done";
                result.Data = book;
            }
            else
            {
                result.Message = "Not Updeted";
                result.ISuccessed = false;

            }
                return result;

        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<ResultDto> Delete(int id)
        {
            if (id != 0)
            { 
                result.Data =await _UnitOfWork.GetBookRepo().GetByID(id);
                await _UnitOfWork.GetBookRepo().Remove(id);
                await _UnitOfWork.Save();
                result.Message = "Removed Book Done";
            }
            else
            {
                result.Message = "Not Removed";
                result.ISuccessed = false;

            }

            return result;

        }

        [HttpGet("search/{text}")]
        public ResultDto Search(string text)
        {
            result.Message = "All Books";
            result.Data = _UnitOfWork.GetBookRepo().Get().Where(b=>b.Title.Contains(text));
            if (result.Data == null)
            {
                result.Message = "Not Found any Books ";
                result.ISuccessed = false;

            }
            return result;
        }
        [HttpPost("addImage")]
        public ResultDto addImage([FromForm]  IFormFile model)
        {
            string filename = string.Empty;
            if (model != null)
            {
                string notRepeat = DateTime.Now.ToString();
                string uplodes = Path.Combine(Hosting.WebRootPath, "images");
                filename = notRepeat+ model.FileName;
                //filename = model.File.FileName + (DateTime.Now.ToString());

                string Fullpath = Path.Combine(uplodes, filename);
                model.CopyTo(new FileStream(Fullpath, FileMode.Create));
                result.Message = "Uplode";
                result.Data = filename;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = "Not Uplode";
                
            }
          
            return result;
        }

        [HttpPut("updatebook")]
        public async Task<ResultDto> updatebook(AddBookDto model)
        {
            string filename = string.Empty;
            if (model.File != null)
            {
                string uplodes = Path.Combine(Hosting.WebRootPath, "Uplodes");
                filename = model.File.FileName;
                //filenam e = model.File.FileName + (DateTime.Now.ToString());
                string oldFilename = model.ImageURL;
                string fullOldPath = Path.Combine(uplodes, oldFilename);
                string Fullpath = Path.Combine(uplodes, filename);

                if (fullOldPath != Fullpath)
                {
                    System.IO.File.Delete(fullOldPath);
                    model.File.CopyTo(new FileStream(Fullpath, FileMode.Create));
                }

            }
            Book book = new Book
            {
                ID = model.BookID,
                Title = model.Title,
                Descryption = model.Descryption,
                Auther = await _UnitOfWork.GeAutherRepo().GetByID(model.AutherID),
                AutherID = model.AutherID,
                ImageURL= filename

             };
            _UnitOfWork.GetBookRepo().Update(book);
            await _UnitOfWork.Save();
            result.Data= book;

            return result;
        }




    }
}
