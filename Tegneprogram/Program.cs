using Tegneprogram;

class OLCircles
{
    //Circles are colored left - right given these colors
    static private string[] circle_colors = new string[] {
        "blue","yellow","black", "green", "red"
    };

    public Cirkel[] cirkel_arr = new Cirkel[5];


    public OLCircles(int width)
    {
        //Simple OL circles relative proportions by eye measure (could be improved)
        int vertical_increments = width / 10;
        int horizontal_increments = width / 8;
        int cirkel_diameter = vertical_increments * 2;
        
        //Instantiate Cirkel-objs 
        for(int i = 0; i < circle_colors.Length; i++)
        {
            Cirkel temp_cirkel = new(circle_colors[i]);
            temp_cirkel.Diameter = cirkel_diameter;
            //Move Cirkel two increments from the left and then add an increment for each Cirkel instantiated
            temp_cirkel.X = horizontal_increments * 2 + horizontal_increments * i;
            //Move Cirkel two increments down and then take turns moving Cirkel on increments up/down
            temp_cirkel.Y = vertical_increments * 2 + horizontal_increments * (i % 2);
            cirkel_arr[i] = temp_cirkel;
        }
    }
}


class OLSquare
{
    static private string[] square_colors = new string[] { "black" };

    public Firkant[] firkant_arr = new Firkant[1];
    

    public OLSquare(int width)
    {
        //OL square is programmed static given some width for this implementation
        Firkant f = new("black");
        f.Hojde = width / 2;
        f.Bredde = width;
        f.X = 0;
        f.Y = 0;

        firkant_arr[0] = f;
    }
}


class OLLogo
{
    public Cirkel[] cirkel_arr;
    public Firkant[] firkant_arr;

    public OLLogo(int width)
    {
        cirkel_arr = new OLCircles(width).cirkel_arr;
        firkant_arr = new OLSquare(width).firkant_arr;
    }
}

class program
{
    static void Main()
    {
        OLLogo logo = new(500);
        Tegning tegner = new Tegning();
        tegner.Tegn(logo.firkant_arr, logo.cirkel_arr);
    }


}