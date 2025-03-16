using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Dough
    {
        private string doughType;
        private string bakingTechnique;
        private double grams;
        private const double WhiteDough = 1.5;
        private const double WholegrainDough = 1;
        private const double Crispy = 0.9;
        private const double Chewy = 1.1;
        private const double Homemade = 1;

        public Dough(string doughType, string bakingTechnique, double grams)
        {
            this.DoughType = doughType;
            this.BakingTechnique = bakingTechnique;
            this.Grams = grams;
        }

        private string DoughType
        {
            get { return this.doughType; }
            set
            {
                if (value.ToLower() != "white" && value.ToLower() != "wholegrain")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                this.doughType = value.ToLower();
            }
        }
        private string BakingTechnique
        {
            get { return this.bakingTechnique; }
            set
            {
                if (value.ToLower() != "crispy" && value.ToLower() != "chewy" && value.ToLower() != "homemade")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                this.bakingTechnique = value.ToLower();
            }
        }
        private double Grams
        {
            get { return this.grams; }
            set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                this.grams = value;
            }
        }
        public double Calories
        {
            get
            {
                if (this.DoughType == "white")
                {
                    if (this.BakingTechnique == "crispy")
                    {
                        return 2 * this.Grams * WhiteDough * Crispy;
                    }
                    else if (this.BakingTechnique == "chewy")
                    {
                        return 2 * this.Grams * WhiteDough * Chewy;
                    }
                    else if (this.BakingTechnique == "homemade")
                    {
                        return 2 * this.Grams * WhiteDough * Homemade;
                    }
                }
                else if (this.DoughType == "wholegrain")
                {
                    if (this.BakingTechnique == "crispy")
                    {
                        return 2 * this.Grams * WholegrainDough * Crispy;
                    }
                    else if (this.BakingTechnique == "chewy")
                    {
                        return 2 * this.Grams * WholegrainDough * Chewy;
                    }
                    else if (this.BakingTechnique == "homemade")
                    {
                        return 2 * this.Grams * WholegrainDough * Homemade;
                    }
                }

                return 2 * this.Grams;
            }
        }
        public override string ToString()
        {
            return $"{this.Calories:f2}";
        }
    }
}
