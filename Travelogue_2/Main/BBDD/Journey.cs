using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Travelogue_2.Main.Models;

namespace Travelogue_2.Main.BBDD
{
    [Table("Journey")]
    public class Journey
    {
        [PrimaryKey, AutoIncrement, Column("_id"), NotNull]
        public int Id { get; set; } = 0;

        [Column("State"), NotNull]
        public State JourneyState { get; set; } = State.CREATED;

        [Column("Name"), MaxLength(40), NotNull]
        public string Name { get; set; } = string.Empty;

        [Column("IniDate"), NotNull]
        public DateTime IniDate { get; set; } = DateTime.Today;

        [Column("EndDate"), NotNull]
        public DateTime EndDate { get; set; } = DateTime.Today;

        [ManyToMany(typeof(JourneyDestiny), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<Destiny> Destinies { get; set; } = new List<Destiny>();

        // TODO Son nuevos, probar
        //[ForeignKey(typeof(Journal))]
        //public int journalId { get; set; }

        //[OneToOne]
        //public Journal Journal { get; set; }
        //public Journal Journal { get; set; }

        [ForeignKey(typeof(Image))]
        public int CoverId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Image Cover { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeDelete | CascadeOperation.CascadeRead)]
        public List<Day> Days { get; set; } = new List<Day>();

        public Journey() { }

        /** Constructor público con tres parámetros:
         *  Name - Nombre del viaje
         *  Ini - Fecha de inicio del viaje
         *  End - Fecha de fin del viaje */
        public Journey(string name, DateTime ini, DateTime end)
        {
            Name = name;
            IniDate = ini;
            EndDate = end;

            CreateDays();
        }

        /** Crea la cantidad de días que dura el viaje */
        private void CreateDays()
        {
            int total_days = Duration();
            Days = Enumerable.Range(0, total_days).
                Select(i => new Day(IniDate.AddDays(i))).
                ToList();
        }

        /** Indica la cantidad de días totales que dura el viaje */
        public int Duration()
        {
            int value = (int)(this.EndDate - this.IniDate).TotalDays + 1;
            if (value > 0) return value;
            else
            {
                //TODO- lanzar exception?
                return 0;
            }
        }

        /** Retorna un booleando indicando si el viaje está abierto */
        public bool HasStarted() { return this.JourneyState.Equals(State.OPEN); }
        /** Retorna un booleando indicando si el viaje está cerrado */
        public bool HasFinished() { return this.JourneyState.Equals(State.CLOSED); }
        /** Retorna un booleando indicando si la fecha de inicio coincide con el día de consulta */
        public bool StartsToday() { return DateTime.Compare(IniDate, DateTime.Today) == 0; }
        /** Retorna un booleando indicando si la fecha de fin coincide con el día de consulta */
        public bool FinishesToday() { return DateTime.Compare(EndDate, DateTime.Today) == 0; }
        /** Retorna un booleando indicando si la fecha de inicio es anterior con el día de consulta */
        public bool StartedAlready() { return DateTime.Compare(IniDate, DateTime.Today) < 0; }
        /** Retorna un booleano indicando si la fecha de fin es anterior con el día de consulta */
        public bool FinishedAlready() { return DateTime.Compare(EndDate, DateTime.Today) < 0; }

        public bool Collision(DateTime dateIni, DateTime dateEnd)
        {
            /** Retorna un booleano indicando si la fecha que se pasa colisiona con la fecha del viaje */
            bool colisionDate = Days.Exists(x => x.Date.CompareTo(dateIni) == 0) ||
               Days.Exists(x => x.Date.CompareTo(dateEnd) == 0);
            /** o si el periodo de fechas colisiona con la fecha del viaje */
            bool colisionPeriod = (IniDate.Date.CompareTo(dateIni.Date) >= 0 &&
                IniDate.Date.CompareTo(dateEnd.Date) <= 0);

            return colisionDate || colisionPeriod;
        }

        public void StartJourney()
        {
            this.JourneyState = State.OPEN;
        }

        public void FinishJourney()
        {
            this.JourneyState = State.CLOSED;
        }

    }
}
