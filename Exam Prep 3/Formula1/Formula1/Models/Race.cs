using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Formula1.Models
{
    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private ICollection<IPilot> pilots;

        private Race()
        {
            this.pilots = new List<IPilot>();
        }

        public Race(string raceName, int numberOfLaps)
            : this()
        {
            this.RaceName = raceName;
            this.NumberOfLaps = numberOfLaps;
        }

        public string RaceName
        {
            get { return this.raceName; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidRaceName, value));
                }

                this.raceName = value;
            }
        }

        public int NumberOfLaps
        {
            get { return this.numberOfLaps; }
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidLapNumbers, value));
                }

                this.numberOfLaps = value;
            }
        }

        public bool TookPlace { get; set; } = false;

        public ICollection<IPilot> Pilots => this.pilots;

        public void AddPilot(IPilot pilot)
        {
            this.pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            int participants = 0;
            foreach (var pilot in this.pilots)
            {
                if (pilot.CanRace)
                {
                    participants++;
                }
            }

            return $"The {this.RaceName} race has:" + Environment.NewLine + $"Participants: {participants}" + Environment.NewLine + $"Number of laps: {this.NumberOfLaps}" + Environment.NewLine + $"Took place: {(this.TookPlace == true ? "Yes" : "No")}";
        }
    }
}
