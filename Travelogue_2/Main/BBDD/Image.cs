﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Travelogue_2.Main.BBDD
{
    [Table("Image")]
    public class Image
    {
        [PrimaryKey, AutoIncrement, Column("_id"), NotNull]
        public int Id { get; set; }

        [Column("Path")]
        public string Path { get; set; } = string.Empty;

        //[Column("Name")]
        //public string Name { get; set; } = string.Empty;

        [Column("Caption")]
        public string Caption { get; set; } = string.Empty;

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Journey")]
        public string Journey { get; set; }

        /** Constructor público sin parámetros necesario para la base de datos */
        public Image() { }

    }
}
