using Notlarim102.BusinessLayer;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Notlarim102WebApp.Controllers
{
    public class CommentController : Controller
    {

        NoteManager nm = new NoteManager();
        // GET: Comment
        public ActionResult ShowNoteComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = nm.Find(s => s.Id == id);

            if (note == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialComment", note.Comments);
        }
    }
}