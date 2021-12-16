﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class Actor
    {
        public int Id { get; set; }

        public string ActorName { get; set; }

        public string ActorSurname { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }

        public int? GenderId { get; set; }
        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int ActorRoleId { get; set; }
        public ActorRole ActorRole { get; set; }


    }
}
