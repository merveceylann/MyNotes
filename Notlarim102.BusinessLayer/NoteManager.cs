﻿using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
     public class NoteManager
    {
        private Repostory<Note> rnote = new Repostory<Note>();

        public List<Note> GetAllNotes()
        {
            return rnote.List();
        }

        public IQueryable<Note> GetAllNoteQueryable()
        {
            return rnote.listQueryable();
        }
    }
}
