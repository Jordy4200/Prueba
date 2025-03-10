﻿namespace Dogs.Models
{
    public class Dog
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Hypoallergenic { get; set; }
        public int LifeSpanMin { get; set; }
        public int LifeSpanMax { get; set; }
        public int MaleWeightMin { get; set; }
        public int MaleWeightMax { get; set; }
        public int FemaleWeightMin { get; set; }
        public int FemaleWeightMax { get; set; }
    }
}
