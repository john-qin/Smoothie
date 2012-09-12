
namespace Smoothie.Domain.Dto
{
    public class SmoothieIngredientsDto
    {
        public int SmoothieId { get; set; }
        public int Quantity { get; set; }
        public string NDB_No { get; set; }


        public double Calories { get; set; }
        public double TotalFat { get; set; }
        public double Saturated { get; set; }
        public double Polyunsaturated { get; set; }
        public double Monounsaturated { get; set; }
        public double Cholesterol { get; set; }
        public double TotalCarbs { get; set; }
        public double DietaryFiber { get; set; }
        public double Sugars { get; set; }
        public double Protein { get; set; }

        public double Calcium { get; set; }
        public double Iron { get; set; }
        public double Magnesium { get; set; }
        public double Phosphorus { get; set; }
        public double Potassium { get; set; }
        public double Sodium { get; set; }
        public double Zinc { get; set; }


        public double VitaminA { get; set; }
        public double VitaminC { get; set; }
        public double VitaminD { get; set; }
        public double VitaminE { get; set; }
        public double Thiamin { get; set; }
        public double Riboflavin { get; set; }
        public double Niacin { get; set; }
        public double VitaminB6 { get; set; }
        public double Folate { get; set; }
        public double VitaminB12 { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }

        public double GmWt_3 { get; set; }
        public string GmWt_Desc3 { get; set; }

    }
}
