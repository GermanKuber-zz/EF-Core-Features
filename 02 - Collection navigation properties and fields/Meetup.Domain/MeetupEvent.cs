﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetup.Domain
{
    public class MeetupEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Group Group { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Location { get; set; }

        //TODO: 01 - Declaro una propiedad privada con la colección a encapsular
        private readonly List<UserMeetup> _assistants = new List<UserMeetup>();
        public IEnumerable<UserMeetup> Assistants => _assistants;

        //TODO: 02 - Encapsulo en un metodo la capacidad de invitar Usuarios
        public void AddAssistant(User user) =>
            _assistants.Add(new UserMeetup { Meetup = this, User = user });

    }

}
