using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    public class NoteController : ApiController
    {
        [HttpGet]
        //[Route("api/Note/?pageNo={pageNo}")]
        public IHttpActionResult GetAllNotes(int pageNum)
        {
            NoteService noteService = CreateNoteService();
            var notes = noteService.GetNotes();

            var totalNotes = notes.Count(); // ?
            int pageNo = pageNum;
            int pageSize = 3;
            var skip = pageSize * (pageNo - 1);
            var canPage = skip < totalNotes; // ?

            if (!canPage)
                return NotFound();

            return Ok(notes
                .Skip(skip)
                .Take(pageSize)
                .ToList());
        }

        [HttpGet]
        [Route("api/Note/{id}")]
        public IHttpActionResult GetNoteById(int id)
        {
            NoteService noteService = CreateNoteService();
            var note = noteService.GetNoteById(id);

            if (note == null)
                return NotFound();

            return Ok(note);
        }

        [HttpPost]
        public IHttpActionResult Post(NoteCreate note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateNoteService();

            if (!service.CreateNote(note))
                return InternalServerError();

            return Ok(note);
        }

        [HttpPut]
        public IHttpActionResult Put(NoteEdit note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateNoteService();

            if (!service.UpdateNote(note))
                return InternalServerError();

            return Ok();
        }

        [HttpPut, Route("api/Note/{id}/IsStarred")]
        public bool PutStarredNotes(int id) // Is it bool b/c we are asking it to return a bool due to IsStarred?
        {
            var service = CreateNoteService();

            var note = service.GetNoteById(id);

            var updatedNote = new NoteEdit()
            {
                NoteId = note.NoteId,
                Title = note.Title,
                Content = note.Content,
                IsStarred = !note.IsStarred
            };

            return service.UpdateNote(updatedNote);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateNoteService();

            if (!service.DeleteNote(id))
                return InternalServerError();

            return Ok();
        }

        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new NoteService(userId);
            return noteService;
        }
    }
}
