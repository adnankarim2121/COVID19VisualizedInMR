using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluid : MonoBehaviour
{
    public int particlesWidth = 500; //Needs to be a multiple of the resolution value below.
    public int particlesHeight = 500; //This too.

    public int resolution = 10; //Width and height of each cell in the grid.

    public float penSize = 40; //Radius around the mouse cursor coordinates to reach when stirring

    public Color32 particlesColor = new Color32(0, 255, 255, 255);

    public float normalParticlesAlpha = 200;
    public float moveParticlesAlpha = 255;

    int num_cols;
    int num_rows;

    private ParticleSystem PS;

    private int particlesCount = 5000;
    private float particlesSize = 5;

    private bool mouseActive = false;
    private int mouseMouseState = 0;
    private float mouseX = 0;
    private float mouseY = 0;

    private float mousePX = 0;
    private float mousePY = 0;

    //FluidPoint[] points;
    private ParticlePoint[] particles;
    private FluidCellPoint[,] vec_cells;

    // Use this for initialization
    void Start()
    {
        PS = GetComponent<ParticleSystem>();

        ParticleSystem.MainModule _psmm = PS.main;
        particlesCount = _psmm.maxParticles;
        particlesSize = _psmm.startSize.constant;

        num_cols = particlesWidth / resolution; //This value is the number of columns in the grid.
        num_rows = particlesHeight / resolution; //This is number of rows.

        particles = new ParticlePoint[particlesCount];

        for (int i = 0; i < particlesCount; i++)
        {
            ParticlePoint p = new ParticlePoint();
            //p.x = Random.Range(-1 * canvasWidth / 2, canvasWidth / 2);
            //p.y = Random.Range(-1 * canvasHeight / 2, canvasHeight / 2);
            p.x = Random.Range(0, particlesWidth);
            p.y = Random.Range(0, particlesHeight);

            particles[i] = p;
        }

        vec_cells = new FluidCellPoint[num_cols, num_rows];

        //This loops through the count of columns.
        for (int col = 0; col < num_cols; col++)
        {
            //This loops through the count of rows.
            for (int row = 0; row < num_rows; row++)
            {

                /*
                This line calls the cell() function, which creates an individual grid cell
                and returns it as an object. The X and Y values are multiplied by the
                resolution so that when the loops are referring to "column 2, row 2", the
                width and height of "column 1, row 1" are counted in so that the top-left
                corner of the new grid cell is at the bottom right of the other cell.
                */
                FluidCellPoint cell_data = new FluidCellPoint(col * resolution, row * resolution, resolution);

                //This pushes the cell object into the grid array.
                vec_cells[col, row] = cell_data;

                /*
                These two lines set the object's column and row values so the object knows
                where in the grid it is positioned.                
                */
                vec_cells[col, row].col = col;
                vec_cells[col, row].row = row;

            }
        }

        /*
        These loops move through the rows and columns of the grid array again and set variables 
        in each cell object that will hold the directional references to neighboring cells. 
        For example, let's say the loop is currently on this cell:

        OOOOO
        OOOXO
        OOOOO

        These variables will hold the references to neighboring cells so you only need to
        use "up" to refer to the cell above the one you're currently on.
        */
        for (int col = 0; col < num_cols; col++)
        {

            for (int row = 0; row < num_rows; row++)
            {

                /*
                This variable holds the reference to the current cell in the grid. When you
                refer to an element in an array, it doesn't copy that value into the new
                variable; the variable stores a "link" or reference to that spot in the array.
                If the value in the array is changed, the value of this variable would change
                also, and vice-versa.
                */
                FluidCellPoint cell_data = vec_cells[col, row];

                /*
                Each of these lines has a ternary expression. A ternary expression is similar 
                to an if/then clause and is represented as an expression (e.g. row - 1 >= 0) 
                which is evaluated to either true or false. If it's true, the first value after
                the question mark is used, and if it's false, the second value is used instead.

                If you're on the first row and you move to the row above, this wraps the row 
                around to the last row. This is done so that momentum that is pushed to the edge 
                of the canvas is "wrapped" to the opposite side.
                */
                var row_up = (row - 1 >= 0) ? row - 1 : num_rows - 1;
                var col_left = (col - 1 >= 0) ? col - 1 : num_cols - 1;
                var col_right = (col + 1 < num_cols) ? col + 1 : 0;

                //Get the reference to the cell on the row above.
                FluidCellPoint up = vec_cells[col, row_up];
                FluidCellPoint left = vec_cells[col_left, row];
                FluidCellPoint up_left = vec_cells[col_left, row_up];
                FluidCellPoint up_right = vec_cells[col_right, row_up];

                /*
                Set the current cell's "up", "left", "up_left" and "up_right" attributes to the 
                respective neighboring cells.
                */
                cell_data.up = up;
                cell_data.left = left;
                cell_data.up_left = up_left;
                cell_data.up_right = up_right;

                /*
                Set the neighboring cell's opposite attributes to point to the current cell.
                */
                up.down = vec_cells[col, row];
                left.right = vec_cells[col, row];
                up_left.down_right = vec_cells[col, row];
                up_right.down_left = vec_cells[col, row];

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouse();

        float mouse_xv = mouseX - mousePX;
        float mouse_yv = mouseY - mousePY;

        //Loops through all of the columns
        for (int i = 0; i < num_cols; i++)
        {
            //Loops through all of the rows
            for (int j = 0; j < num_rows; j++)
            {

                //References the current cell
                FluidCellPoint cell_data = vec_cells[i, j];

                //If the mouse button is down, updates the cell velocity using the mouse velocity
                if (mouseActive)
                {
                    change_cell_velocity(cell_data, mouse_xv, mouse_yv, penSize);
                }

                //This updates the pressure values for the cell.
                update_pressure(cell_data);
            }
        }

        UpdateParticles();

        /*
        This calls the function to update the cell velocity for every cell by looping through
        all of the rows and columns.
        */
        for (int i = 0; i < num_cols; i++)
        {
            for (int j = 0; j < num_rows; j++)
            {
                var cell_data = vec_cells[i, j];

                update_velocity(cell_data);

            }
        }

        mousePX = mouseX;
        mousePY = mouseY;
    }

    void UpdateMouse()
    {
        if (Input.GetMouseButtonDown(0))
            mouseMouseState += 1;
        if (Input.GetMouseButtonUp(0))
            mouseMouseState -= 1;



        if (mouseMouseState == 1)
        {
            mouseActive = true;
            RaycastHit hitt = new RaycastHit();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out hitt);

            mouseX = hitt.point.x;
            mouseY = hitt.point.y;

        }
        else
            mouseActive = false;
    }

    void UpdateParticles()
    {
        ParticleSystem.Particle[] ps = new ParticleSystem.Particle[PS.particleCount];
        int pCount = PS.GetParticles(ps);
        float alpha = 255;

        for (int i = 0; i < particlesCount; i++)
        {
            ParticlePoint p = particles[i];

            if (p.x >= 0 && p.x < particlesWidth && p.y >= 0 && p.y < particlesHeight)
            {

                /*
                These lines divide the X and Y values by the size of each cell. This number is
                then parsed to a whole number to determine which grid cell the particle is above.
                */
                int col = (int)(p.x / resolution);
                int row = (int)(p.y / resolution);

                //Same as above, store reference to cell
                FluidCellPoint cell_data = vec_cells[col, row];

                /*
                These values are percentages. They represent the percentage of the distance across
                the cell (for each axis) that the particle is positioned. To give an example, if 
                the particle is directly in the center of the cell, these values would both be "0.5"

                The modulus operator (%) is used to get the remainder from dividing the particle's 
                coordinates by the resolution value. This number can only be smaller than the 
                resolution, so we divide it by the resolution to get the percentage.
                */
                float ax = (p.x % resolution) / resolution;
                float ay = (p.y % resolution) / resolution;

                /*
                These lines subtract the decimal from 1 to reverse it (e.g. 100% - 75% = 25%), multiply 
                that value by the cell's velocity, and then by 0.05 to greatly reduce the overall change in velocity 
                per frame (this slows down the movement). Then they add that value to the particle's velocity
                in each axis. This is done so that the change in velocity is incrementally made as the
                particle reaches the end of it's path across the cell.
                */
                p.xv += (1f - ax) * cell_data.xv * 0.05f;
                p.yv += (1f - ay) * cell_data.yv * 0.05f;

                /*
                These next four lines are are pretty much the same, except the neighboring cell's 
                velocities are being used to affect the particle's movement. If you were to comment
                them out, the particles would begin grouping at the boundary between cells because
                the neighboring cells wouldn't be able to pull the particle into their boundaries.
                */
                p.xv += ax * cell_data.right.xv * 0.05f;
                p.yv += ax * cell_data.right.yv * 0.05f;

                p.xv += ay * cell_data.down.xv * 0.05f;
                p.yv += ay * cell_data.down.yv * 0.05f;

                //This adds the calculated velocity to the position coordinates of the particle.
                p.x += p.xv;
                p.y += p.yv;

                //For each axis, this gets the distance between the old position of the particle and it's new position.
                float dx = p.px - p.x;
                float dy = p.py - p.y;

                //Using the Pythagorean theorum (A^2 + B^2 = C^2), this determines the distance the particle travelled.
                float dist = Mathf.Sqrt(dx * dx + dy * dy);

                //This line generates a random value between 0 and 0.5
                float limit = Random.Range(0, particlesSize / 2);// Math.random() * 0.5;

                //If the distance the particle has travelled this frame is greater than the random value...
                if (dist > limit)
                {
                    if (i < pCount)
                    {
                        ps[i].startSize = particlesSize;
                        alpha = moveParticlesAlpha;
                    }
                }
                else
                {
                    if (i < pCount)
                    {
                        ps[i].startSize = limit;
                        alpha = normalParticlesAlpha;
                    }
                }

                //This updates the previous X and Y coordinates of the particle to the new ones for the next loop.
                p.px = p.x;
                p.py = p.y;
            }
            else
            {
                //If the particle's X and Y coordinates are outside the bounds of the canvas...

                //Place the particle at a random location on the canvas
                //p.x = p.px = Random.Range(-1 * canvasWidth / 2, canvasWidth / 2);
                //p.y = p.py = Random.Range(-1 * canvasHeight / 2, canvasHeight / 2);
                p.x = p.px = Random.Range(0, particlesWidth);
                p.y = p.py = Random.Range(0, particlesHeight);

                //Set the particles velocity to zero.
                p.xv = 0;
                p.yv = 0;
            }

            //These lines divide the particle's velocity in half everytime it loops, slowing them over time.
            p.xv *= 0.5f;
            p.yv *= 0.5f;


            if (i < pCount)
            {
                ps[i].position = new Vector3(p.x, p.y);
                ps[i].startColor = new Color32(particlesColor.r, particlesColor.g, particlesColor.b, (byte)alpha);
            }
        }

        //Debug.Log(ps[0].position.ToString());

        PS.SetParticles(ps, pCount);
    }


    /*
    This function updates the pressure value for an individual cell using the 
    pressures of neighboring cells.
    */
    void update_pressure(FluidCellPoint cell_data)
    {

        //This calculates the collective pressure on the X axis by summing the surrounding velocities
        float pressure_x = (
            cell_data.up_left.xv * 0.5f //Divided in half because it's diagonal
            + cell_data.left.xv
            + cell_data.down_left.xv * 0.5f //Same
            - cell_data.up_right.xv * 0.5f //Same
            - cell_data.right.xv
            - cell_data.down_right.xv * 0.5f //Same
        );

        //This does the same for the Y axis.
        float pressure_y = (
            cell_data.up_left.yv * 0.5f
            + cell_data.up.yv
            + cell_data.up_right.yv * 0.5f
            - cell_data.down_left.yv * 0.5f
            - cell_data.down.yv
            - cell_data.down_right.yv * 0.5f
        );

        //This sets the cell pressure to one-fourth the sum of both axis pressure.
        cell_data.pressure = (pressure_x + pressure_y) * 0.25f;
    }

    /*
    This function changes the cell velocity of an individual cell by first determining whether the cell is 
    close enough to the mouse cursor to be affected, and then if it is, by calculating the effect that mouse velocity
    has on the cell's velocity.
    */
    void change_cell_velocity(FluidCellPoint cell_data, float mvelX, float mvelY, float pen_size)
    {
        //This gets the distance between the cell and the mouse cursor.
        float dx = cell_data.x - mouseX;
        float dy = cell_data.y - mouseY;
        float dist = Mathf.Sqrt(dy * dy + dx * dx);

        //If the distance is less than the radius...
        if (dist < pen_size)
        {

            //If the distance is very small, set it to the pen_size.
            if (dist < 4)
            {
                dist = pen_size;
            }

            //Calculate the magnitude of the mouse's effect (closer is stronger)
            var power = pen_size / dist;

            /*
            Apply the velocity to the cell by multiplying the power by the mouse velocity and adding it to the cell velocity
            */
            cell_data.xv += mvelX * power;
            cell_data.yv += mvelY * power;
        }
    }


    /*
    This function updates the velocity value for an individual cell using the 
    velocities of neighboring cells.
    */
    void update_velocity(FluidCellPoint cell_data)
    {

        /*
        This adds one-fourth of the collective pressure from surrounding cells to the 
        cell's X axis velocity.
        */
        cell_data.xv += (
            cell_data.up_left.pressure * 0.5f
            + cell_data.left.pressure
            + cell_data.down_left.pressure * 0.5f
            - cell_data.up_right.pressure * 0.5f
            - cell_data.right.pressure
            - cell_data.down_right.pressure * 0.5f
        ) * 0.25f;

        //This does the same for the Y axis.
        cell_data.yv += (
            cell_data.up_left.pressure * 0.5f
            + cell_data.up.pressure
            + cell_data.up_right.pressure * 0.5f
            - cell_data.down_left.pressure * 0.5f
            - cell_data.down.pressure
            - cell_data.down_right.pressure * 0.5f
        ) * 0.25f;

        /*
        This slowly decreases the cell's velocity over time so that the fluid stops
        if it's left alone.
        */
        cell_data.xv *= 0.99f;
        cell_data.yv *= 0.99f;
    }


    public void Reset()
    {
        //Loops through all of the columns
        for (int i = 0; i < num_cols; i++)
        {
            //Loops through all of the rows
            for (int j = 0; j < num_rows; j++)
            {
                FluidCellPoint cell_data = vec_cells[i, j];
                cell_data.Reset();
            }
        }

        for (int i = 0; i < particlesCount; i++)
        {
            ParticlePoint p = new ParticlePoint();
            //p.x = Random.Range(-1 * canvasWidth / 2, canvasWidth / 2);
            //p.y = Random.Range(-1 * canvasHeight / 2, canvasHeight / 2);
            p.x = Random.Range(0, particlesWidth);
            p.y = Random.Range(0, particlesHeight);

            particles[i] = p;
        }


    }
}
